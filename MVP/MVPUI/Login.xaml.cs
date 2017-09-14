using MVP.ViewModels;
using MVPUI.Helpers;
using System;
using Xamarin.Forms;

namespace MVPUI
{
    public partial class Login : ContentPage
    {

        #region Constructor

        public Login()
        {
            InitializeComponent();
            BindingContext = LoginViewModel.Instance;
        }
        #endregion

        #region Private and Protected Methods

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string platformName = Device.OS.ToString();

            if (LoginViewModel.Instance.IsLoggedIn)
            {
                try
                {
                    if (string.IsNullOrEmpty(MyProfileViewModel.Instance.StoreImageBase64Str))
                    {
                        MyProfileViewModel.Instance.StoreImageBase64Str = await MvpService.GetPhoto(LoginViewModel.StoredToken);
                    }
                }
                catch (Exception ex)
                {
                    MyProfileViewModel.Instance.StoreImageBase64Str = CommonConstants.DefaultPhoto;
                    MyProfileViewModel.Instance.ErrorMessage = ex.Message;
                }

                await Navigation.PushModalAsync(new MyProfile());
            }
            else
            {
                var tabGesture = new TapGestureRecognizer();
                tabGesture.Tapped += TabGesture_Tapped;
                //Content.FindByName<Label>("loginButton" + platformName).GestureRecognizers.Add(tabGesture);
                Content.FindByName<Label>("loginBtn").GestureRecognizers.Add(tabGesture);
                
            }

        }

        private async void TabGesture_Tapped(object sender, EventArgs e)
        {

            if (LoginViewModel.Instance.IsLoggedIn)
            {
                try
                {
                    var profile = await MvpService.GetProfile(LoginViewModel.StoredToken);
                    MvpHelper.SetDataToProfileViewModel(profile);
                    if (string.IsNullOrEmpty(MyProfileViewModel.Instance.StoreImageBase64Str))
                    {
                        MyProfileViewModel.Instance.StoreImageBase64Str = await MvpService.GetPhoto(LoginViewModel.StoredToken);
                    }
                }
                catch (Exception ex)
                {
                    MyProfileViewModel.Instance.StoreImageBase64Str = CommonConstants.DefaultPhoto;
                    MyProfileViewModel.Instance.ErrorMessage = ex.Message;
                }

                await Navigation.PushModalAsync(new MyProfile());

            }
            else
            {
                await Navigation.PushModalAsync(new LiveIdLogin());
            }

        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LiveIdLogin());
        }

        #endregion

    }
}
