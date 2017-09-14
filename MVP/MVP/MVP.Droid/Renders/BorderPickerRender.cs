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
using Android.Widget;

[assembly: ExportRenderer(typeof(BorderPicker), typeof(BorderPickerRender))]
namespace Microsoft.Mvp.Droid
{
    public class BorderPickerRender : ViewRenderer<BorderPicker, Spinner>
    {
        BorderPicker picker;
        bool isFirstLoaded = true; 
        int initSelectedIndex = -1;

        protected override void OnElementChanged(ElementChangedEventArgs<BorderPicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
            {
                return;
            }

            picker = e.NewElement;
            if (Control == null)
            {
                IList<string> scaleNames = e.NewElement.Items;
                ConvertToNative(scaleNames, picker);
            }

        }

        private void ConvertToNative(IList<string> scaleNames, BorderPicker view)
        {
            Spinner spinner = new Spinner(this.Context);
            spinner.ItemSelected -= new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var scaleAdapter = new ArrayAdapter<string>(this.Context, Microsoft.Mvp.Droid.Resource.Layout.simple_spinner_item, scaleNames);
            scaleAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = scaleAdapter;
            base.SetNativeControl(spinner);

            var nativeEditText = (global::Android.Widget.Spinner)Control;
            var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
            shape.Paint.Color = view.BorderColor.ToAndroid();
            shape.Paint.StrokeWidth = 10;
            shape.Paint.SetStyle(Android.Graphics.Paint.Style.Stroke);
            nativeEditText.Background = shape;

        }


        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (BorderPicker)Element;

            if (e.PropertyName == "SelectedIndex" && isFirstLoaded)
            {
                int selectIndex = picker.SelectedIndex;
                picker = sender as BorderPicker;
                IList<string> scaleNames = picker.Items;
                ConvertToNative(scaleNames,picker);
                isFirstLoaded = false;
                if (selectIndex > 0)
                {
                    initSelectedIndex = selectIndex;
                }
            }

            if (e.PropertyName.Equals("Width") || e.PropertyName.Equals("Height"))
            {
                var nativeEditText = (global::Android.Widget.Spinner)Control;
                var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
                shape.Paint.Color = view.BorderColor.ToAndroid();
                shape.Paint.StrokeWidth = 10;
                shape.Paint.SetStyle(Android.Graphics.Paint.Style.Stroke);
                nativeEditText.Background = shape;
            }
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (initSelectedIndex > 0)
            {
                (sender as Spinner).SetSelection(initSelectedIndex);
                initSelectedIndex = -1;
            }
            else
            {
                picker.SelectedIndex = (e.Position);

            }
        }

        private void DrawBorder(BorderPicker view)
        {

            GradientDrawable gd = new GradientDrawable();
            gd.SetStroke((int)view.BorderWidth * 2, view.BorderColor.ToAndroid());
            gd.SetCornerRadius((float)view.BorderRadius);
            Control.SetBackground(gd);
        }
    }
}
