using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Core;
using Windows.Media.Capture;
using Windows.System;
using System.Diagnostics;

namespace XboxSamples.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

           // stateSystem.Text = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            Shell.HamburgerMenu.IsOpen = !Shell.HamburgerMenu.IsOpen;
        }

        public void showMenu()
        {
            Shell.HamburgerMenu.IsOpen = !Shell.HamburgerMenu.IsOpen;
            Shell.HamburgerMenu.Focus(FocusState.Keyboard);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //SystemNavigationManager.GetForCurrentView().BackRequested -= MainPage_BackRequested;
            try
            {
                //BootStrapper.Current.NavigationService.FrameFacade.BackRequested -= FrameFacade_BackRequested;
                //BootStrapper.BackRequested -= BootStrapper_BackRequested;

            }
            catch { }

        }


        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {

            var state = sender.GetKeyState(Windows.System.VirtualKey.Control);
            Debug.WriteLine(args.VirtualKey);
            //if (state == CoreVirtualKeyStates.Down)
            {
                switch (args.VirtualKey)
                {
                    //case VirtualKey.GamepadMenu: showMenu();  break;
                    case VirtualKey.GamepadB: directoryChoose(); break;
                    case VirtualKey.GamepadY: takePhoto(); break;
                }
            }
            // else
            {
                switch (args.VirtualKey)
                {
                    case VirtualKey.F5: showMenu(); break;


                }
            }
        }

        public async void fileChoose()
        {
            FolderPicker fp = new FolderPicker();
            fp.FileTypeFilter.Add(".png");
            fp.FileTypeFilter.Add(".jpeg");
            fp.FileTypeFilter.Add(".jpg");
            StorageFolder folder = await fp.PickSingleFolderAsync();
        }

        public async void directoryChoose()
        {
            FileOpenPicker fop = new FileOpenPicker();
            fop.FileTypeFilter.Add(".png");
            fop.FileTypeFilter.Add(".jpeg");
            fop.FileTypeFilter.Add(".jpg");

            // Interazione con l'utente
            StorageFile sf = await fop.PickSingleFileAsync();
            // controllo se il file è stato selezionato
            //if (sf != null)
        }

        public async void takePhoto()
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            //captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            takePhoto();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            fileChoose();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            directoryChoose();
        }
    }
}

