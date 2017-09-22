using System;
using Microsoft.Mvp.iOS.Renderers;
using Microsoft.Mvpui.CustomControls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ModalPage), typeof(ContentPageRenderer))]
namespace Microsoft.Mvp.iOS.Renderers
{
    public class ContentPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ModalPresentationCapturesStatusBarAppearance = false;

            // Set the status bar to light.
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, false);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            // Set the status bar to light.
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
        }
    }
}
