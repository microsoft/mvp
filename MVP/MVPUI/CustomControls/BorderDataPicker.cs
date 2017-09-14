using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Microsoft.Mvpui
{
    public class BorderDatePicker : DatePicker
    {
        public static BindableProperty BorderWidthProperty = BindableProperty.Create(
                                                                         "BorderWidth",
                                                                         typeof(double),
                                                                         typeof(BorderDatePicker),
                                                                         2.0);

        public double BorderWidth
        {
            get
            {
                return (double)GetValue(BorderWidthProperty);
            }
            set
            {
                SetValue(BorderWidthProperty, value);
            }
        }

        public static BindableProperty BorderColorProperty = BindableProperty.Create(
                                                                          "BorderColor",
                                                                          typeof(Color),
                                                                          typeof(BorderDatePicker),
                                                                          Color.FromHex("#807A79"));

        public Color BorderColor
        {
            get
            {
                return (Color)GetValue(BorderColorProperty);
            }
            set
            {
                SetValue(BorderColorProperty, value);
            }
        }

        public static BindableProperty BorderRadiusProperty = BindableProperty.Create(
                                                                          "BorderRadius",
                                                                          typeof(double),
                                                                          typeof(BorderDatePicker),
                                                                          0.0);

        public double BorderRadius
        {
            get
            {
                return (double)GetValue(BorderRadiusProperty);
            }
            set
            {
                SetValue(BorderRadiusProperty, value);
            }
        }
    }
}
