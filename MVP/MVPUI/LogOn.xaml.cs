

using Microsoft.Mvp.ViewModels;
using System;
using Xamarin.Forms;
using Microsoft.Mvp.Interfaces;
using Microsoft.Mvp.Helpers;

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
                await DisplayAlert("Error", "Failed to open your browser", "OK");
            }
        }

        #endregion
    }
}
