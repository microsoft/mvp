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
                // Double-Checked Locking
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
        
        private string _strAvatarBackground = "mvplogo.png";
        private string _logOnImage = "LoginBtn.png";
        private string _settingIcon = "Settings.png";
        private string _searchIcon = "searchIcon.png";
        private static string _storedToken;
       
        #endregion

        #region Public Members
        public bool IsLoggedIn
        {
            get
            {

                string tokenAndExpiredTime = null;
                if (Application.Current.Properties.ContainsKey(CommonConstants.TokenKey))
                {
                    tokenAndExpiredTime = Application.Current.Properties[CommonConstants.TokenKey].ToString();
                }

                if (!string.IsNullOrEmpty(tokenAndExpiredTime))
                {
                    var index = tokenAndExpiredTime.LastIndexOf(",",StringComparison.Ordinal);

                    var expiredtime = Convert.ToDateTime(tokenAndExpiredTime.Substring(index + 1), System.Globalization.CultureInfo.CurrentCulture);
                    var token = tokenAndExpiredTime.Substring(0, index);
                    if (DateTime.Now.AddHours(-7) < expiredtime)
                    {
                        LogOnViewModel.StoredToken = token;
                    }
                    else
                    {
                        LogOnViewModel.StoredToken = "";
                    }
                }

                return !string.IsNullOrEmpty(LogOnViewModel.StoredToken);
            }
        }
        public string MvpBackgroundLogo
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}{1}", CommonConstants.BaseResourcePath, _strAvatarBackground);
            }
            set
            {
                _strAvatarBackground = value;
                OnPropertyChanged("MvpBackgroundLogo");
            }
        }

        public string LogOnImage
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}{1}", CommonConstants.BaseResourcePath, _logOnImage);
            }
            set
            {
                _logOnImage = value;
                OnPropertyChanged("LogOnImage");
            }
        }
        public string SettingIcon
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}{1}", CommonConstants.BaseResourcePath, _settingIcon);
            }
            set
            {
                _logOnImage = value;
                OnPropertyChanged("SettingIcon");
            }
        }
        public string SearchIcon
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}{1}", CommonConstants.BaseResourcePath, _searchIcon);
            }
            set
            {
                _logOnImage = value;
                OnPropertyChanged("SearchIcon");
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
        #endregion

    }
}
