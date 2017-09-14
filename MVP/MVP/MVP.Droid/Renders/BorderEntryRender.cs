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

[assembly: ExportRenderer(typeof(BorderEntry), typeof(BorderEntryRender))]
namespace Microsoft.Mvp.Droid
{
    public class BorderEntryRender : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

           
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);


            var view = (BorderEntry)Element; 
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

        private void DrawBorder(BorderEntry view)
        {
            GradientDrawable gd = new GradientDrawable();

            gd.SetStroke((int)view.BorderWidth * 2, view.BorderColor.ToAndroid());
            gd.SetCornerRadius((float)view.BorderRadius);
            Control.SetBackground(gd);
        }
    }
}
