using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Microsoft.Mvpui.Converters
{
    // TODO: I think this doesn't get used, can be removed?

    public class UriToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Uri input = value as Uri;
            return input == null ?
                String.Empty : input.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string input = value as string;
            return String.IsNullOrEmpty(input) ?
                null : new Uri(input, UriKind.Absolute);
        }
    }
}
