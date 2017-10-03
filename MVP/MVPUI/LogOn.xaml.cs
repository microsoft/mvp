#define SKIP_LOGIN

using Microsoft.Mvp.ViewModels;
using System;
using Xamarin.Forms;
using Microsoft.Mvp.Interfaces;

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
        }

        private async void ButtonSignIn_Clicked(object sender, EventArgs e)
        {
#if SKIP_LOGIN
            App.GoHome();
            return;
#endif

            if (LogOnViewModel.Instance.IsLoggedIn)
            {
                App.GoHome();
            }
            else
            {
                await Navigation.PushModalAsync(new NavigationPage(new LiveIdLogOn()));
            }
        }

        #endregion
    }
}
