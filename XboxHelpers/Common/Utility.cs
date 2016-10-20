using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace XboxHelpers.Common
{
   public class Utility
    {
        public static bool setXYMode(Application app)
        {
            try
            {
                app.RequiresPointerMode = Windows.UI.Xaml.ApplicationRequiresPointerMode.WhenRequested;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool setMouseMode(Application app)
        {
            try
            {
                app.RequiresPointerMode = ApplicationRequiresPointerMode.Auto;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool isMouseMode(Application app)
        {
            
               return app.RequiresPointerMode == Windows.UI.Xaml.ApplicationRequiresPointerMode.Auto;
               
            
        }



        public static bool isXbox(Application app)
        {

            return Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily =="Windows.Xbox";


        }

       public static void RemoveSafeArea()
        {
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
        }

    }
}
