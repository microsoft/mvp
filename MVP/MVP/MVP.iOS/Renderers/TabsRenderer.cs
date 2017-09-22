using Microsoft.Mvp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabsRenderer))]
namespace Microsoft.Mvp.iOS.Renderers
{
    public class TabsRenderer : TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            UITabBar.Appearance.TintColor = ((Color)Xamarin.Forms.Application.Current.Resources["TabTintColor"]).ToUIColor();
        }

        public override void ViewWillAppear(bool animated)
        {
            if (TabBar?.Items == null)
                return;

            // Go through our elements and change the icons
            var tabs = Element as TabbedPage;
            if (tabs != null)
            {
                for (int i = 0; i < TabBar.Items.Length; i++)
                    UpdateTabBarItem(TabBar.Items[i], tabs.Children[i].Icon);
            }

            base.ViewWillAppear(animated);
        }

        private void UpdateTabBarItem(UITabBarItem item, string icon)
        {
            if (item == null || icon == null)
                return;

            // Set our different selected icons
            icon = icon.Replace(".png", "_selected.png");

            if (item?.SelectedImage?.AccessibilityIdentifier == icon)
                return;

            item.SelectedImage = UIImage.FromBundle(icon).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            item.SelectedImage.AccessibilityIdentifier = icon;
        }
    }
}
