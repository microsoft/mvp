using Microsoft.Mvp.Helpers;
using System;
using Xamarin.Forms;

namespace Microsoft.Mvp.ViewModels
{
	public class LogOnViewModel : ViewModelBase
	{

		#region Singleton pattern and constructors

		private static LogOnViewModel _instance = null;
		private static readonly object _synObject = new object();

		public static LogOnViewModel Instance
		{
			get
			{
				if (null == _instance)
				{
					lock (_synObject)
					{
						if (null == _instance)
						{
							_instance = new LogOnViewModel();
						}
					}
				}
				return _instance;
			}
		}

		#endregion

		#region Private Members

		private static string _storedToken;

		#endregion

		#region Public Members

		public bool IsLoggedIn
		{
			get
			{

				string tokenAndExpiredTime = null;
				if (Settings.GetSetting(CommonConstants.TokenKey) != string.Empty)
				{
					tokenAndExpiredTime = Settings.GetSetting(CommonConstants.TokenKey);
				}

				if (!string.IsNullOrEmpty(tokenAndExpiredTime))
				{
					var index = tokenAndExpiredTime.LastIndexOf(",", StringComparison.Ordinal);

					var expiredtime = Convert.ToDateTime(tokenAndExpiredTime.Substring(index + 1), System.Globalization.CultureInfo.InvariantCulture);
					var token = tokenAndExpiredTime.Substring(0, index);
					if (DateTime.Now.AddHours(-7) < expiredtime)
					{
						StoredToken = token;
					}
					else
					{
						StoredToken = "";
					}
				}

				return !string.IsNullOrEmpty(LogOnViewModel.StoredToken);
			}
		}

		public static string StoredToken
		{
			get
			{
				return _storedToken;
			}

			set
			{
				_storedToken = value;

			}

		}

		public string LearnMore
		{
			get {
				return TranslateServices.GetResourceString(CommonConstants.LearnMore);
			} 
		}

		public string SigninTo {
			get
			{
				return TranslateServices.GetResourceString(CommonConstants.SigninTo);
			}
		}

		public string Welcome
		{
			get
			{
				return TranslateServices.GetResourceString(CommonConstants.Welcome);
			}
		}

		public string PageTitleForLogOn
		{
			get
			{
				return TranslateServices.GetResourceString(CommonConstants.PageTitleForLogOn);
			}
		}

		public string CancelButton { get; } = TranslateServices.GetResourceString(CommonConstants.CancelButton);




		#endregion

	}
}
