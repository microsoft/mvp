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
            BarTextColor = Color.White;
            BarBackgroundColor = (Color)Application.Current.Resources["Primary"];
        }
    }
}
