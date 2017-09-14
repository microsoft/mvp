using Microsoft.Mvp.iOS;
using Microsoft.Mvpui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;

[assembly: ExportRenderer(typeof(BorderEntry), typeof(BorderEntryRender))]
namespace Microsoft.Mvp.iOS
{
    public class BorderEntryRender : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Layer.BorderColor = Color.FromHex("8D8787").ToCGColor();
                Control.Layer.BorderWidth = 1.5f;
                Control.BorderStyle = UITextBorderStyle.None;
                Control.LeftView = new UIView(new CGRect(0, 0, 5, 0));
                Control.LeftViewMode = UITextFieldViewMode.Always;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}
