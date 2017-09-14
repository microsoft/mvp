using MVPUI.Helpers;
using System;
using Xamarin.Forms;

namespace MVP.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        #region Singleton pattern and constructors
        private static LoginViewModel _instance = null;
        private static readonly object _synObject = new object();
        public static LoginViewModel Instance
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
                            _instance = new LoginViewModel();
                        }
                    }
                }
                return _instance;
            }
        }

        public LoginViewModel()
        {
            _strWPResourcePath = (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows) ? CommonConstants.ImageFolderForWP : string.Empty;
        }

        #endregion

        #region Private Members

        private string _strWPResourcePath;
        private string _strAvatarBackground = "mvplogo.png";
        private string _loginImage = "LoginBtn.png";
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
                    var index = tokenAndExpiredTime.LastIndexOf(",");

                    var expiredtime = Convert.ToDateTime(tokenAndExpiredTime.Substring(index + 1));
                    var token = tokenAndExpiredTime.Substring(0, index);
                    if (DateTime.Now.AddHours(-7) < expiredtime)
                    {
                        LoginViewModel.StoredToken = token;
                    }
                    else
                    {
                        LoginViewModel.StoredToken = "";
                    }
                }

                return !string.IsNullOrEmpty(LoginViewModel.StoredToken);
            }
        }
        public string MvpBackgroundLogo
        {
            get
            {
                return string.Format("{0}{1}", _strWPResourcePath, _strAvatarBackground);
            }
            set
            {
                _strAvatarBackground = value;
                OnPropertyChanged("MvpBackgroundLogo");
            }
        }

        public string LoginImage
        {
            get
            {
                return string.Format("{0}{1}", _strWPResourcePath, _loginImage);
            }
            set
            {
                _loginImage = value;
                OnPropertyChanged("LoginImage");
            }
        }
        public string SettingIcon
        {
            get
            {
                return string.Format("{0}{1}", _strWPResourcePath, _settingIcon);
            }
            set
            {
                _loginImage = value;
                OnPropertyChanged("SettingIcon");
            }
        }
        public string SearchIcon
        {
            get
            {
                return string.Format("{0}{1}", _strWPResourcePath, _searchIcon);
            }
            set
            {
                _loginImage = value;
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
