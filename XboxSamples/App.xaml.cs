using Windows.UI.Xaml;
using System.Threading.Tasks;
using XboxSamples.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using XboxSamples.Views;
using Windows.Storage;
using XboxHelpers.Common;

namespace XboxSamples
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {
        ApplicationDataContainer settings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public App()
        {
            InitializeComponent();

            Object UseMouseMode = settings.Values["UseMouseMode"];

           

            // Disable mouse control
            if(UseMouseMode!=null && (bool)UseMouseMode==true)
            Utility.setMouseMode(Application.Current);
            else
                Utility.setXYMode(Application.Current);

            //this.RequiresPointerMode = Windows.UI.Xaml.ApplicationRequiresPointerMode.WhenRequested;
            SplashFactory = (e) => new Views.Splash(e);

            #region App settings

            var _settings = SettingsService.Instance;
            RequestedTheme = _settings.AppTheme;
            CacheMaxDuration = _settings.CacheMaxDuration;
            ShowShellBackButton = _settings.UseShellBackButton;

            #endregion
        }

        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            if (Window.Current.Content as ModalDialog == null)
            {
                // create a new frame 
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);

                // create modal root
                Window.Current.Content = new ModalDialog
                {
                    DisableBackButtonWhenModal = true,
                    Content = new Views.Shell(nav),
                    ModalContent = new Views.Busy(),
                };
            }
            await Task.CompletedTask;
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            //Remove safe area
            // 

            Object RemoveSafeArea = settings.Values["RemoveSafeArea"];



            // Disable mouse control
            if (RemoveSafeArea != null && (bool)RemoveSafeArea == true)
                Utility.removeSafeArea();
            

            // long-running startup tasks go here
            await Task.Delay(5000);

            NavigationService.Navigate(typeof(Views.MainPage));
            await Task.CompletedTask;
        }
    }
}

