using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Xamarin.Forms;
using Microsoft.Mvp.Interfaces;

namespace Microsoft.Mvp.Helpers
{
	public class TranslateExtensionConverter : IValueConverter
	{
	

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{ 
			string tempText = string.Empty;
			if (value is null)
			{
				return string.Empty;
			}
			else
			{
				tempText = value.ToString();
			}
	 
			
			if (tempText == null)
				return "";

			
			var translation = TranslateServices.GetResourceString(tempText);

			return translation;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
