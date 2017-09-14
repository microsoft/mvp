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

[assembly: ExportRenderer(typeof(BorderDatePicker), typeof(BorderDatePickerRender))]
namespace Microsoft.Mvp.Droid
{
    public class BorderDatePickerRender : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var view = (Element as BorderDatePicker);
                if (view != null)
                {
                    DrawBorder(view);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (BorderDatePicker)Element;

            if (e.PropertyName.Equals("Width") || e.PropertyName.Equals("Height"))
            {
                DrawBorder(view);
            }

        }

        private void DrawBorder(BorderDatePicker view)
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
