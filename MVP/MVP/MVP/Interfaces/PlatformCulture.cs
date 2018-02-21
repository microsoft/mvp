using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Mvp.Interfaces
{
	public class PlatformCulture
	{
		public PlatformCulture(string platformCultureString)
		{
			if (String.IsNullOrEmpty(platformCultureString))
			{
				throw new ArgumentException(Helpers.TranslateServices.GetResourceString(Helpers.CommonConstants.ExpectedCultureIdentifier), "platformCultureString"); // in C# 6 use nameof(platformCultureString)
			}
			PlatformString = platformCultureString.Replace("_", "-"); // .NET expects dash, not underscore
			var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);
			if (dashIndex > 0)
			{
				var parts = PlatformString.Split('-');
				LanguageCode = parts[0];
				LocaleCode = parts[1];
			}
			else
			{
				LanguageCode = PlatformString;
				LocaleCode = "";
			}
		}
		public string PlatformString { get; private set; }
		public string LanguageCode { get; private set; }
		public string LocaleCode { get; private set; }
		public override string ToString()
		{
			return PlatformString;
		}
	}
}
