using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Device = Xamarin.Forms.Device;
using Microsoft.Azure.Mobile.Analytics;

namespace Microsoft.Mvp.Helpers
{
	public static class Logger
	{
		static bool IsEnabled => ((Device.RuntimePlatform == Device.Android && CommonConstants.MobileCenterAndroid != "MC_ANDROID") ||
				(Device.RuntimePlatform == Device.iOS && CommonConstants.MobileCenteriOS != "MC_IOS") ||
				(Device.RuntimePlatform == Device.UWP && CommonConstants.MobileCenterUWP != "MC_UWP"));

		public static void Log(string name, string extra)
		{
			if (!IsEnabled)
				return;

			Analytics.TrackEvent(name, new Dictionary<string, string> { ["extra"] = extra });
		}

		public static void Log(string name)
		{
			if (!IsEnabled)
				return;

			Analytics.TrackEvent(name);
		}
			
	}
}
