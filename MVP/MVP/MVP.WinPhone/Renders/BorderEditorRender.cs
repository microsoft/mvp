using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms;
using System.ComponentModel;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Microsoft.Mvp.WinPhone;
using Microsoft.Mvpui;

[assembly: ExportRenderer(typeof(BorderEditor), typeof(BorderEditorRender))]
namespace Microsoft.Mvp.WinPhone
{
    public class BorderEditorRender : EditorRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var view = (Element as BorderEditor);
                if (view != null)
                {                  
                    Control.Style = (Windows.UI.Xaml.Style)App.Current.Resources["CustomTextEditor"];
                    DrawBorder(view);
                }
            }
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (BorderEditor)Element;

            if (e.PropertyName.Equals(view.BorderColor))
                DrawBorder(view);
        }

        private void DrawBorder(BorderEditor view)
        {
            Control.BorderThickness = new Windows.UI.Xaml.Thickness(1.0, 1.0, 1.0, 1.0);
            Control.BorderBrush = new SolidColorBrush(Colors.Black);
        }
    }
}