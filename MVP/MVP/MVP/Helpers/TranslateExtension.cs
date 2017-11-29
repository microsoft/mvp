using Microsoft.Mvp.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Microsoft.Mvp.Helpers
{
	//	// You exclude the 'Extension' suffix when using in Xaml markup
	//	[ContentProperty("Text")] 
	//	public class TranslateExtension : IMarkupExtension
	//	{
	//		readonly CultureInfo ci;
	//		const string ResourceId = "Microsoft.Mvp.Resources.AppResources";

	//		private static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId
	//																												  , typeof(TranslateExtension).GetTypeInfo().Assembly));

	//		public TranslateExtension()
	//		{
	//			if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
	//			{
	//				ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
	//			}
	//		}

	//		public string Text { get; set; }

	//		public string StringFormat { get; set; }

	//		public object ProvideValue(IServiceProvider serviceProvider)
	//		{
	//			if (Text == null)
	//				return "";

	//			var translation1 = ResMgr.Value.GetString(Text, ci);
	//			ResourceManager temp = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

	//			var translation = temp.GetString(Text, ci);

	//			if (translation == null)
	//			{
	//#if DEBUG
	//				throw new ArgumentException(
	//					String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),
	//					"Text");
	//#else
	//				translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
	//#endif
	//			}
	//			else
	//			{
	//				if (!string.IsNullOrEmpty(StringFormat))
	//				{
	//					translation = string.Format(StringFormat, translation);
	//				}
	//			}
	//			return translation;
	//		}
	//	}


	
}

