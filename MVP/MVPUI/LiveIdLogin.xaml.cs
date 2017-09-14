using MVP.Models;
using MVP.ViewModels;
using MVPUI.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MVPUI
{
    public partial class LiveIdLogin : ContentPage
    {

        #region Constructor

        public LiveIdLogin()
        {
            InitializeComponent();
            this.BindingContext = LiveIdLoginViewModel.Instance;
        }

        #endregion

        #region Private and Protected Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();

            WebView browser = Content.FindByName<WebView>("browser");
            browser.Navigating -= Browser_Navigating;
            browser.Navigating += Browser_Navigating;
            browser.Source = new UrlWebViewSource() { Url = LiveIdLoginViewModel.Instance.LiveIdLoginUrl };
            browser.GoForward();

        }

        private async void Browser_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Uri liveUrl = new Uri(e.Url, UriKind.Absolute);
            if (liveUrl.AbsoluteUri.Contains("code="))
            {
                if (Application.Current.Properties.ContainsKey(CommonConstants.AuthCodeKey))
                {
                    Application.Current.Properties.Clear();
                }

                string auth_code = System.Text.RegularExpressions.Regex.Split(liveUrl.AbsoluteUri, "code=")[1];
                Application.Current.Properties.Add(CommonConstants.AuthCodeKey, auth_code);
                await LiveIdLoginViewModel.Instance.GetAccessToken();
                await Navigation.PushModalAsync(new MyProfile());
            }
        }

        #endregion

    }
}
