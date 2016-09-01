using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using XboxHelpers.Common;

namespace XboxSamples.Views
{
    public sealed partial class SettingsPage : Page
    {
        Template10.Services.SerializationService.ISerializationService _SerializationService;


        ApplicationDataContainer settings = Windows.Storage.ApplicationData.Current.LocalSettings;
        bool init = false;
        public SettingsPage()
        {
            InitializeComponent();
            _SerializationService = Template10.Services.SerializationService.SerializationService.Json;

            version.Text = Version;
            logo.Source = new BitmapImage(Logo);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            init = true;
            var index = int.Parse(_SerializationService.Deserialize(e.Parameter?.ToString()).ToString());
            MyPivot.SelectedIndex = index;
            UseMouseMode.IsOn = Utility.isMouseMode(Application.Current);

            Object rsa = settings.Values["RemoveSafeArea"];
            if (rsa != null && (bool)rsa == true)
                RemoveSafeArea.IsOn = true;
            init = false;

        }

        public Uri Logo => Windows.ApplicationModel.Package.Current.Logo;

        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

        public string Publisher => Windows.ApplicationModel.Package.Current.PublisherDisplayName;

        public string Version
        {
            get
            {
                var v = Windows.ApplicationModel.Package.Current.Id.Version;
                return $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
        }

        private void RemoveSafeArea_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (init)
                return;
            settings.Values["RemoveSafeArea"] = RemoveSafeArea.IsOn;
            CloseApp();
        }

        private void UseMouseMode_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (init)
                return;
            settings.Values["UseMouseMode"] = UseMouseMode.IsOn;
            CloseApp();
            
        }

        public async void CloseApp()
        {
            var dialog = new Windows.UI.Popups.MessageDialog(
"Close app for apply this changes?",
"Question");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                // Adding a 3rd command will crash the app when running on Mobile !!!
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Maybe later") { Id = 2 });
            }

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();
            if((int)result.Id==0)
                Application.Current.Exit();
        }
    }
}
