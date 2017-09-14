using Microsoft.Mvp.iOS.CustomControls;
using Microsoft.Mvpui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderEditor), typeof(BorderEditorRender))]
namespace Microsoft.Mvp.iOS.CustomControls
{
    public class BorderEditorRender :EditorRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Layer.BorderColor = Color.FromHex("8D8787").ToCGColor();
                Control.Layer.BorderWidth = 1.5f;
            }
        }
    }
}