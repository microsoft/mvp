using Android.Graphics.Drawables;
using Microsoft.Mvp.Droid;
using Microsoft.Mvpui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(BorderEditor), typeof(BorderEditorRender))]
namespace Microsoft.Mvp.Droid
{
    public class BorderEditorRender : EditorRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (BorderEditor)Element;
            if (e.PropertyName.Equals("Width") || e.PropertyName.Equals("Height"))
            {
                var nativeEditText = (global::Android.Widget.EditText)Control;
                var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
                shape.Paint.Color = view.BorderColor.ToAndroid();
                shape.Paint.StrokeWidth = 10;
                shape.Paint.SetStyle(Android.Graphics.Paint.Style.Stroke);
                nativeEditText.Background = shape;
            }
        }
    }

    
}
