using Microsoft.Mvp.ViewModels;
using Microsoft.Mvpui.Helpers;
using System;
using Xamarin.Forms;

namespace Microsoft.Mvpui
{
    public partial class LogOn : ContentPage
    {

        #region Constructor

        public LogOn()
        {
            InitializeComponent();
            BindingContext = LogOnViewModel.Instance;
        }
        #endregion

        #region Private and Protected Methods

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var tabGesture = new TapGestureRecognizer();
            tabGesture.Tapped += TabGesture_Tapped;
            Content.FindByName<Label>("logOnBtn").GestureRecognizers.Add(tabGesture);

            if (LogOnViewModel.Instance.IsLoggedIn)
            {
                await Navigation.PushModalAsync(new MyProfile());
            }
        }

        private async void TabGesture_Tapped(object sender, EventArgs e)
        {

            var imageButton = sender as Label;

            if (imageButton != null)
            {
                imageButton.Opacity = 0.5;
                await imageButton.FadeTo(1);
            }

            if (LogOnViewModel.Instance.IsLoggedIn)
            {
                await Navigation.PushModalAsync(new MyProfile());
            }
            else
            {
                await Navigation.PushModalAsync(new LiveIdLogOn());
            }

        }

        private void OnLogOnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LiveIdLogOn());
        }

        #endregion

    }
}
