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

[assembly: ExportRenderer(typeof(BorderEntry), typeof(BorderEntryRender))]
namespace Microsoft.Mvp.WinPhone
{
    public class BorderEntryRender : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var view = (Element as BorderEntry);
                if (view != null)
                {
                    Control.Style = (Windows.UI.Xaml.Style)App.Current.Resources["CustomTextBox"];
                    DrawBorder(view);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (BorderEntry)Element;

            if (e.PropertyName.Equals(view.BorderColor))
                DrawBorder(view);
        }

        private void DrawBorder(BorderEntry view)
        {
            Control.BorderThickness = new Windows.UI.Xaml.Thickness(1.0, 1.0, 1.0, 1.0);
            Control.BorderBrush =new SolidColorBrush(Colors.Black);
        }
    }
}
