

using Microsoft.Mvp.ViewModels;
using System;
using Xamarin.Forms;
using Microsoft.Mvp.Interfaces;
using Microsoft.Mvp.Helpers;
using Plugin.Connectivity;
using Acr.UserDialogs;
using System.Diagnostics;

namespace Microsoft.Mvpui
{
    public partial class LogOn : ContentPage
    {

        #region Constructor

        public LogOn()
        {
            InitializeComponent();

			ToolbarClose.Command = new Command(async () => await Navigation.PopModalAsync());

			if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WinPhone)
				ToolbarClose.Icon = "Assets\\toolbar_close.png";


			BindingContext = LogOnViewModel.Instance;
        }
		#endregion

		#region Private and Protected Methods
		
        protected override async void OnAppearing()
        {
            base.OnAppearing();

			if (IsBusy)
				return;
			

			if (Settings.GetSetting(CommonConstants.TokenKey) != string.Empty)
			{
				IProgressDialog progress = null;
				try
				{
					IsBusy = true;
					progress = UserDialogs.Instance.Loading(TranslateServices.GetResourceString(CommonConstants.RefreshTokenKey));
					await LiveIdLogOnViewModel.GetNewAccessToken();
					
				}
				catch (Exception ex)
				{
					Debug.WriteLine(string.Format(TranslateServices.GetResourceString(CommonConstants.RefreshTokenExceptionTip), ex.Message));
				}
				finally
				{
					progress?.Hide();
					IsBusy = false;
				}
			}

            if (LogOnViewModel.Instance.IsLoggedIn)
            {
                App.GoHome();
            }
            else
            {
                await logo.FadeTo(1, 500, Easing.CubicOut);
                await title.FadeTo(1, 1000, Easing.CubicOut);
            }
        }

        private async void ButtonSignIn_Clicked(object sender, EventArgs e)
        {
			if (CommonConstants.ClientId == "LIVE_ID")
			{
				App.GoHome();
				return;
			}


            if (LogOnViewModel.Instance.IsLoggedIn)
            {
                App.GoHome();
            }
            else if(!CrossConnectivity.Current.IsConnected)
			{
				await DisplayAlert(TranslateServices.GetResourceString(CommonConstants.DialogTitleForCheckNetwork),
					TranslateServices.GetResourceString(CommonConstants.DialogDescriptionForCheckNetwork),
					TranslateServices.GetResourceString(CommonConstants.DialogOK));
			}
			else
            {
                await Navigation.PushModalAsync(new NavigationPage(new LiveIdLogOn()));
            }
        }

        private async void ButtonLearnMore_Clicked(object sender, EventArgs e)
        {
            try
            {
				await Plugin.Share.CrossShare.Current.OpenBrowser(CommonConstants.LearnMoreUrl, new Plugin.Share.Abstractions.BrowserOptions
				{
					ChromeShowTitle = true,
					ChromeToolbarColor = new Plugin.Share.Abstractions.ShareColor { R = 3, G = 169, B = 244, A = 255 },
					SafariBarTintColor = new Plugin.Share.Abstractions.ShareColor { R = 3, G = 169, B = 244, A = 255 },
					UseSafariWebViewController = true,
					SafariControlTintColor = new Plugin.Share.Abstractions.ShareColor { R = 255, G = 255, B = 255, A = 255 },
					UseSafariReaderMode = false
				});
            }
            catch (Exception ex)
            {
                await DisplayAlert(TranslateServices.GetResourceString(CommonConstants.DialogTitleForError), TranslateServices.GetResourceString(CommonConstants.DialogDescriptionForError), TranslateServices.GetResourceString(CommonConstants.DialogOK));
            }
        }

        #endregion
    }
}
