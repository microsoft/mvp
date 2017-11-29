using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.Interfaces;
using Microsoft.Mvp.ViewModels;
using Xamarin.Forms;
using System.Reflection;
using Device = Xamarin.Forms.Device;

namespace Microsoft.Mvpui
{
	public partial class App : Application
	{



		#region Private Fields

		private static ICookieHelper _cookieHelper;
		private static ISignOutHelper _SignOutHelper;
		private static ILocationHelper _locationHelper;

		#endregion

		#region Public Fields

		public static ICookieHelper CookieHelper
		{
			get
			{
				if (_cookieHelper == null)
				{
					_cookieHelper = DependencyService.Get<ICookieHelper>();
				}
				return _cookieHelper;
			}
			set
			{
				_cookieHelper = value;
			}
		}


		public static ISignOutHelper SignOutHelper
		{
			get
			{
				if (_SignOutHelper == null)
				{
					_SignOutHelper = DependencyService.Get<ISignOutHelper>();
				}
				return _SignOutHelper;
			}
			set
			{
				_SignOutHelper = value;
			}
		}

		public static ILocationHelper LocationHelper
		{
			get
			{
				if (_locationHelper == null)
				{
					_locationHelper = DependencyService.Get<ILocationHelper>();
				}
				return _locationHelper;
			}
			set
			{
				_locationHelper = value;
			}
		}
		#endregion

		#region Constructor

		public App()
		{
			InitializeComponent();

#if DEBUG
			// force a specific culture, useful for quick testing 
			System.Diagnostics.Debug.WriteLine("====== resource debug info =========");
			var assembly = typeof(App).GetTypeInfo().Assembly;
			foreach (var res in assembly.GetManifestResourceNames())
				System.Diagnostics.Debug.WriteLine("found resource: " + res);
			System.Diagnostics.Debug.WriteLine("====================================");
#endif


			// This lookup NOT required for Windows platforms - the Culture will be automatically set
			if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.UWP)
			{
				// determine the correct, supported .NET culture
				var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
				Microsoft.Mvp.Resources.AppResources.Culture = ci; // set the RESX for resource localization
				DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
			}

			//Microsoft.Mvp.Resources.AppResources.Culture = new System.Globalization.CultureInfo("en");

#if !DEBUG
			if ((Device.RuntimePlatform == Device.Android && CommonConstants.MobileCenterAndroid != "MC_ANDROID") ||
				(Device.RuntimePlatform == Device.iOS && CommonConstants.MobileCenteriOS != "MC_IOS") ||
				(Device.RuntimePlatform == Device.UWP && CommonConstants.MobileCenterUWP != "MC_UWP"))
			{
				MobileCenter.Start($"android={CommonConstants.MobileCenterAndroid};" +
				   $"uwp={CommonConstants.MobileCenterUWP};" +
				   $"ios={CommonConstants.MobileCenteriOS}",
				   typeof(Analytics), typeof(Crashes));
			}

			if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
			{
				var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
				Mvp.Resources.AppResources.Culture = ci; // set the RESX for resource localization
				DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
			}
#endif

#if DEBUG
			//if design register the actual service, else mock
			if (CommonConstants.ClientId == "LIVE_ID")
				DependencyService.Register<IMvpService, DesignMvpService>();
			else
				DependencyService.Register<IMvpService, LiveMvpService>();
#else
			if (CommonConstants.ClientId == "LIVE_ID")
				throw new System.InvalidOperationException("Invalid configuration, please fill in proper Ids.");

			DependencyService.Register<IMvpService, LiveMvpService>();
#endif

			if (LogOnViewModel.Instance.IsLoggedIn)
			{
				GoHome();
			}
			else
			{
				MainPage = new LogOn();
			}
		}

		public static void GoHome()
		{
			// The root page of your application
			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					Current.MainPage = new MainTabPageiOS();
					break;
				default:
					Current.MainPage = new MVPNavigationPage(new MainTabPage())
					{
						Title = "Microsoft MVP"
					};
					break;
			}
		}

#endregion

#region Private and Protected Methods


		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

#endregion

	}
}
