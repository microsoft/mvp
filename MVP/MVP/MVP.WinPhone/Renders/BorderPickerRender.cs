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

[assembly: ExportRenderer(typeof(BorderPicker), typeof(BorderPickerRender))]
namespace Microsoft.Mvp.WinPhone
{
    public class BorderPickerRender : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {           
                var view = (Element as BorderPicker);
                if (view != null)
                { 
                    Control.Style = (Windows.UI.Xaml.Style)App.Current.Resources["ComboBoxStyle1"]; 
                    DrawBorder(view);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (BorderPicker)Element; 

            if (e.PropertyName.Equals("SelectedIndex"))
            {
                    
            }
            if (e.PropertyName.Equals(view.BorderColor))
                DrawBorder(view); 
        }

        private void DrawBorder(BorderPicker view)
        {
            Control.BorderThickness = new Windows.UI.Xaml.Thickness(1.0, 1.0, 1.0, 1.0);
            Control.BorderBrush =new SolidColorBrush(Colors.Black);
        } 
    }
}
