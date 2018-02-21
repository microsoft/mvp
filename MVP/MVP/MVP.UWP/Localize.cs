using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using Microsoft.Mvp.Interfaces;
using System.Globalization;
using System.Threading;
using Windows.Globalization.DateTimeFormatting;

[assembly: Xamarin.Forms.Dependency(typeof(Microsoft.Mvp.UWP.Helpers.Localize))]

namespace Microsoft.Mvp.UWP.Helpers
{
	public class Localize : ILocalize
	{
		public void SetLocale(CultureInfo ci)
		{
			//System.Threading.Thread.CurrentThread.CurrentCulture = ci;
			//System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
		}
		public CultureInfo GetCurrentCultureInfo()
		{
			System.Globalization.CultureInfo ci = null; 

			try
			{
				//ci = CultureInfo.DefaultThreadCurrentUICulture;
				var cultureName = new DateTimeFormatter("longdate", new[] { "US" }).ResolvedLanguage;

				return new CultureInfo(cultureName);
			}
			catch (CultureNotFoundException e1)
			{
				ci = new System.Globalization.CultureInfo("en");
			}
			return ci;
		}
	}
}
