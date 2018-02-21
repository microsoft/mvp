using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.ViewModels;
using System;
using Xamarin.Forms;

namespace Microsoft.Mvpui
{
    public partial class LiveIdLogOn : ContentPage
    {

        #region Constructor

        public LiveIdLogOn()
		{
			Logger.Log("Page-LiveIdLogOn");
			InitializeComponent();
            this.BindingContext = LiveIdLogOnViewModel.Instance;

            //TODO: Perhaps remove toolbaritems on Android?
        }

        #endregion

        #region Private and Protected Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();

            WebView browserInstance = Content.FindByName<WebView>("browser");
            browserInstance.Navigating -= Browser_Navigating;
            browserInstance.Navigating += Browser_Navigating;
            browserInstance.Source = new UrlWebViewSource() { Url = LiveIdLogOnViewModel.Instance.LiveIdLogOnUrl };
            browserInstance.GoForward();
        }

        private async void ButtonClose_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

        private async void Browser_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Uri liveUrl = new Uri(e.Url, UriKind.Absolute);
            if (liveUrl.AbsoluteUri.Contains("code="))
            {
                if (Settings.GetSetting(CommonConstants.AuthCodeKey) != string.Empty)
                {
                    MvpHelper.RemoveProperties();
                }

                string auth_code = System.Text.RegularExpressions.Regex.Split(liveUrl.AbsoluteUri, "code=")[1];
				Settings.SetSetting(CommonConstants.AuthCodeKey, auth_code);
                await LiveIdLogOnViewModel.Instance.GetAccessToken();

                var profileTest = await MvpHelper.MvpService.GetProfile(LogOnViewModel.StoredToken);
                if (profileTest == null || profileTest.MvpId == 0)
                {
					Logger.Log("Login-Invalid");
                    await DisplayAlert(string.Empty, TranslateServices.GetResourceString(CommonConstants.DialogDescriptionForInvalidCredentials), TranslateServices.GetResourceString(CommonConstants.DialogOK));
                    App.CookieHelper.ClearCookie();
                    LiveIdLogOnViewModel.Instance.SignOut();
                    await Navigation.PopModalAsync(true);
                }
                else
				{
					Logger.Log("Login-Valid");

					switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            Application.Current.MainPage = new MainTabPageiOS();
                            break;
                        default:
                            Application.Current.MainPage = new MVPNavigationPage(new MainTabPage())
                            {
                                Title = "Microsoft MVP"
                            };
                            break;
                    }
                }

            }
        }

        #endregion

    }
}
