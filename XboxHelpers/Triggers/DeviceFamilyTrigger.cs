using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace XboxHelpers.Triggers
{
    public class DeviceFamilyTrigger : StateTriggerBase

    {

        public string DeviceFamily

        {

            get { return (string)GetValue(DeviceFamilyProperty); }

            set { SetValue(DeviceFamilyProperty, value); }

        }



        public static readonly DependencyProperty DeviceFamilyProperty =

            DependencyProperty.Register("DeviceFamily", typeof(string), typeof(DeviceFamilyTrigger),

                new PropertyMetadata(string.Empty, DeviceFamilyChanged));



        private static void DeviceFamilyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)

        {

            var @this = (DeviceFamilyTrigger)dependencyObject;

            var queriedFamily = (string)dependencyPropertyChangedEventArgs.NewValue;

            var currentFamily = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

            @this.SetActive(queriedFamily == currentFamily);

        }
    }
}
