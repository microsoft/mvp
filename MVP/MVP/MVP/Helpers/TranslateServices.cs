using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Resources;
using Microsoft.Mvp.Interfaces;
using Xamarin.Forms;

namespace Microsoft.Mvp.Helpers
{
	public class TranslateServices
	{
		static CultureInfo ci = new CultureInfo("en");
		const string resourceId = "Microsoft.Mvp.Resources.AppResources";

		private static readonly Lazy<ResourceManager> resMgr = new Lazy<ResourceManager>(
			() => new ResourceManager(resourceId, typeof(TranslateServices).GetTypeInfo().Assembly));

		public static string GetResourceString(string resourceKey)
		{
			string translation = string.Empty;
			if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.UWP)
			{
				ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
			}

			translation = resMgr.Value.GetString(resourceKey, ci);

			if (translation == null)
			{
#if DEBUG
				throw new ArgumentException(
					String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", resourceKey, resourceId, ci.Name),
					"Text");
#else
				translation = resourceKey; // returns the key, which GETS DISPLAYED TO THE USER
#endif
			}
			return translation;
		}
	}
}
