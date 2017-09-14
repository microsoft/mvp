using Microsoft.Mvp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Microsoft.Mvpui
{
    public partial class Settings : ContentPage
    {

        #region Constructor
        public Settings()
        {
            InitializeComponent();

            string StrWPResourcePath = (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows) ? Helpers.CommonConstants.ImageFolderForWP : string.Empty;
            btnCancel.Source = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", StrWPResourcePath, "Cancel.png");

            btnSignOut.Clicked += BtnSignOut_Clicked;
            btnAbout.Clicked += BtnAbout_Clicked;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnCloseClicked;
            btnCancel.GestureRecognizers.Add(tapGestureRecognizer);

        }

        #endregion

        #region Private and Protected Methods

        protected override bool OnBackButtonPressed()
        {
            //Navigation.PushAsync(new MyProfile());
            Navigation.PopModalAsync();
            return true;
        }

        private async void BtnAbout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new About());
        }

        private async void BtnSignOut_Clicked(object sender, EventArgs e)
        {
            App.CookieHelper.ClearCookie();
            LiveIdLogOnViewModel.Instance.SignOut();
            await Navigation.PushModalAsync(new LogOn());
        }

        private async void OnCloseClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
            //await Navigation.PushModalAsync(new MyProfile());
        }

        #endregion

    }
}
