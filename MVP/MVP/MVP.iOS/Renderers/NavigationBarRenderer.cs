using System;
using Microsoft.Mvp.iOS.Renderers;
using Microsoft.Mvpui;
using Microsoft.Mvp.iOS.Helpers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavigationBarRenderer))]
namespace Microsoft.Mvp.iOS.Renderers
{
    public class NavigationBarRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            // Some basic navigationbar styling.
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes()
            {
                ForegroundColor = ((Color)App.Instance.Resources["NavigationTitleColor"]).ToUIColor(),
                Font = UIFont.BoldSystemFontOfSize(16)
            };

            UINavigationBar.Appearance.TintColor = ((Color)App.Instance.Resources["NavigationTintColor"]).ToUIColor();
            UINavigationBar.Appearance.ShadowImage = ((Color)App.Instance.Resources["NavigationShadowColor"]).ToUIColor().ToUIImage(1, 1);
            UINavigationBar.Appearance.BarTintColor = Color.White.ToUIColor();
        }
    }
}
