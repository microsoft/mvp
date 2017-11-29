using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.ViewModels;
using System;

using Xamarin.Forms;

namespace Microsoft.Mvpui
{
    public partial class SettingsPage : ContentPage
    {
		
		#region Constructor
		public SettingsPage()
        {
            InitializeComponent();
			Logger.Log("Page-Settings");

			ToolbarClose.Command = new Command(async () => await Navigation.PopModalAsync());

            if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WinPhone)
                ToolbarClose.Icon = "Assets\\toolbar_close.png";

			//Don't need toolbar on UWP/Android as we pushed.
			if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.Android)
				ToolbarItems.Clear();

            BindingContext = new SettingsViewModel();
        }

        #endregion

        #region Private and Protected Methods


        private async void ButtonSignoutClicked(object sender, EventArgs e)
        {
			var confirm = await DisplayAlert(TranslateServices.GetResourceString(CommonConstants.DialogTitleForSignout), TranslateServices.GetResourceString(CommonConstants.DialogForSignoutConfirmTipText), TranslateServices.GetResourceString(CommonConstants.DialogForSignoutOKText), TranslateServices.GetResourceString(CommonConstants.DialogCancel));
			if (!confirm)
				return;


			Logger.Log("Logout");

			App.CookieHelper.ClearCookie();
            LiveIdLogOnViewModel.Instance.SignOut();

			await Navigation.PopToRootAsync();
            App.Current.MainPage = new LogOn();
        }

        #endregion

    }
}
