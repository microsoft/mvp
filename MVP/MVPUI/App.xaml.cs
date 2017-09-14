using Microsoft.Mvp.ViewModels;
using Xamarin.Forms;

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

            if (LogOnViewModel.Instance.IsLoggedIn)
            {
                MainPage = new MyProfile();
            }
            else
            {
                MainPage = new LogOn();
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
