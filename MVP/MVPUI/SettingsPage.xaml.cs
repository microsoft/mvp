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

            ToolbarClose.Command = new Command(async () => await Navigation.PopModalAsync());

            if (Device.RuntimePlatform == Device.Windows || Device.RuntimePlatform == Device.WinPhone)
                ToolbarClose.Icon = "Assets\\toolbar_close.png";

            BindingContext = new SettingsViewModel();
        }

        #endregion

        #region Private and Protected Methods


        private async void ButtonSignoutClicked(object sender, EventArgs e)
        {
            App.CookieHelper.ClearCookie();
            LiveIdLogOnViewModel.Instance.SignOut();
            await Navigation.PushModalAsync(new LogOn());
        }

        #endregion

    }
}
