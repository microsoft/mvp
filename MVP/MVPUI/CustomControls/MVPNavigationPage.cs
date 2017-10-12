using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Microsoft.Mvpui
{
    class MVPNavigationPage : NavigationPage
    {
        public MVPNavigationPage(Page root) : base(root)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {

                BarTextColor = (Color)Application.Current.Resources["PrimaryDark"]; ;
                BarBackgroundColor = (Color)Application.Current.Resources["BackgroundColoriOS"];
            }
            else
            {
                BarTextColor = Color.White;
                BarBackgroundColor = (Color)Application.Current.Resources["Primary"];
            }
        }
    }
}
