using Acr.UserDialogs;
using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.Models;
using Microsoft.Mvp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Microsoft.Mvpui
{
    public partial class MyProfile : ContentPage
    {

        #region Constructor

        public MyProfile()
		{
			Logger.Log("Page-Profile");
			InitializeComponent();
            this.BindingContext = MyProfileViewModel.Instance;

            if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WinPhone)
                ToolBarSettings.Icon = "Assets\\toolbar_settings.png";

            ToolBarSettings.Command = new Command(async () =>
            {
				if (Device.RuntimePlatform == Device.iOS)
					await Navigation.PushModalAsync(new MVPNavigationPage(new SettingsPage()));
				else
					await Navigation.PushAsync(new SettingsPage());
            });
        }

        #endregion

        #region Private and Protected Methods

		
        protected async override void OnAppearing()
        {
            base.OnAppearing();

			MyProfileViewModel.Instance.LoadProfileCommand.Execute(null);
        }

        #endregion

    }
}
