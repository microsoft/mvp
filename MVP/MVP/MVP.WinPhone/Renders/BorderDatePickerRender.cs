using Microsoft.Mvp.WinPhone;
using Microsoft.Mvpui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(BorderDatePicker), typeof(BorderDatePickerRender))]
namespace Microsoft.Mvp.WinPhone
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
                    Control.Template = (Windows.UI.Xaml.Controls.ControlTemplate)App.Current.Resources["MyDatePickerTemplate"]; 
                    DrawBorder(view);
                }

                Windows.UI.Color wpColor = Windows.UI.Color.FromArgb(
            (byte)(view.BackgroundColor.A * 255),
            (byte)(view.BackgroundColor.R * 255),
            (byte)(view.BackgroundColor.G * 255),
            (byte)(view.BackgroundColor.B * 255));

                Brush brush = new SolidColorBrush(wpColor);
                Control.Background = brush;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (BorderDatePicker)Element;

            if (e.PropertyName.Equals(view.BorderColor))
                DrawBorder(view);
        }

        private void DrawBorder(BorderDatePicker view)
        { 
            Control.Foreground = new SolidColorBrush(Windows.UI.Colors.Black); 
        }
    }
}
