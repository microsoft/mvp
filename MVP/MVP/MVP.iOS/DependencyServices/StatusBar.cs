using System;
using Microsoft.Mvp.Interfaces;
using Microsoft.Mvp.iOS.DependencyServices;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBar))]
namespace Microsoft.Mvp.iOS.DependencyServices
{
    public class StatusBar : IStatusBar
    {
        public void SetLightStatusBar(bool animated = false)
        {
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, animated);
        }

        public void SetDarkStatusBar(bool animated = false)
        {

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, animated);
        }
    }
}
