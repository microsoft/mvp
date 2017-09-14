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
     
     //   private readonly string _liveIdLoginUrl = "https://login.live.com/oauth20_authorize.srf?client_id=0000000048193351&scope=wl.basic%20wl.emails&response_type=code&redirect_uri=https://login.live.com/oauth20_desktop.srf";

       
        static string scope = "wl.emails%20wl.basic%20wl.offline_access%20wl.signin";
        static string client_id = "000000004818061B"; //<- this is my (Micah) client key ==> yours[0000000048193351]
        static string _liveIdLoginUrl =
        String.Format(@"https://login.live.com/oauth20_authorize.srf?client_id={0}&redirect_uri=https://login.live.com/oauth20_desktop.srf&response_type=code&scope={1}", client_id, scope);

        static string client_secret = "XQNsbHbSnd17CNcLYdZcm6i8gz79HA4u";  // <- my (Micah) secret ), put yours in here
        static string accessTokenUrl = String.Format(@"https://login.live.com/oauth20_token.srf?client_id={0}&client_secret={1}&redirect_uri=https://login.live.com/oauth20_desktop.srf&grant_type=authorization_code&code=", client_id, client_secret);


        public LiveIdLogin()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            WebView browser = Content.FindByName<WebView>("browser");
            browser.Navigating -= Browser_Navigating;
            browser.Navigating += Browser_Navigating;
            browser.Source = new UrlWebViewSource() { Url = _liveIdLoginUrl };
            browser.GoForward();

        }

        private async void Browser_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Uri liveUrl = new Uri(e.Url, UriKind.Absolute);
            if (liveUrl.AbsoluteUri.Contains("code="))
            {
                if (App.Current.Properties.ContainsKey("auth_code"))
                {
                    App.Current.Properties.Clear();
                }

                string auth_code = System.Text.RegularExpressions.Regex.Split(liveUrl.AbsoluteUri, "code=")[1];
                App.Current.Properties.Add("auth_code", auth_code);

                getAccessToken();
            }
        }

        private void getAccessToken()
        {
            if (App.Current.Properties.ContainsKey("auth_code"))
            {
                makeAccessTokenRequest(accessTokenUrl + App.Current.Properties["auth_code"]);
            }
        }

        private async void makeAccessTokenRequest(string requestUrl)
        {
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;

            string responseTxt = String.Empty;
            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                responseTxt = reader.ReadToEnd();

                var tokenData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseTxt);
                if (tokenData.ContainsKey("access_token"))
                {
                    App.Current.Properties.Add("access_token", tokenData["access_token"]);
                    LoginViewModel.StoredToken = tokenData["access_token"];

                    // refresh token
                    App.Current.Properties.Add("refresh_token", tokenData["refresh_token"]);

                    // testing refresh token
                   // var ret = await GetNewAccessToken();

                    var profile = await MvpService.GetProfile(LoginViewModel.StoredToken);
                    MvpHelper.SetDataToProfileViewModel(profile);
                    var photobase64Str = await MvpService.GetPhoto(LoginViewModel.StoredToken);

                    LoginViewModel.StoreImageBase64Str = photobase64Str;
                    var myprofileView = new MyProfile();
                    myprofileView.SetPhoto(photobase64Str);
                    await Navigation.PushModalAsync(myprofileView);
                    Application.Current.Properties[CommonConstants.TokenKey] = string.Format("{0},{1}", LoginViewModel.StoredToken, DateTime.Now);

                }
            }
        }

        // TODO: this is example on how code is executed.  Should be refactored to be called when
        // we get a Forbidden response from API
        private async Task<string> GetNewAccessToken()
        {
            string refreshToken = App.Current.Properties["refresh_token"].ToString();
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = false,
            };

            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("client_id", client_id));
            postData.Add(new KeyValuePair<string, string>("client_secret", client_secret));
            postData.Add(new KeyValuePair<string, string>("refresh_token", refreshToken));
            postData.Add(new KeyValuePair<string, string>("redirect_uri", "https://login.live.com/oauth20_desktop.srf"));
            postData.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));

            using (var client = new HttpClient(handler) { Timeout = new System.TimeSpan(0, 0, 15) })
            {
                using (var content = new FormUrlEncodedContent(postData))
                {

                    HttpResponseMessage response = null;
                    response = await client.PostAsync("https://login.live.com/oauth20_token.srf", content);

                    // Parse response
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var tokenData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

                        if (tokenData.ContainsKey("access_token"))
                        {
                            App.Current.Properties.Remove("access_token");
                            App.Current.Properties.Add("access_token", tokenData["access_token"]);
                            // refresh token
                            App.Current.Properties.Remove("refresh_token");
                            App.Current.Properties.Add("refresh_token", tokenData["refresh_token"]);

                        }
                        return tokenData["access_token"];
                    }

                }

                return string.Empty;
            }
        }

        //private string GetTokenFromUrl(string url)
        //{
        //    string urlReturnInfo = url.Substring(url.IndexOf("?") + 1);

        //    var infos = urlReturnInfo.Split('&').ToList();
        //    Dictionary<string, string> infoListDic = new Dictionary<string, string>();
        //    foreach (var s in infos)
        //    {
        //        var index = s.IndexOf('=');

        //        var key = s.Substring(0, index);
        //        var value = s.Substring(index + 1);



        //        infoListDic.Add(key, value);
        //    }

        //    if (infoListDic.ContainsKey("status") && infoListDic["status"] == "True")
        //    {
        //        return infoListDic["msg"];
        //    }

        //    return "";
        //}


        private async void TestService(string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create("https://mvpstaging.com/api/contributions/test") as HttpWebRequest;

                request.Method = "GET";
                request.Headers["Authorization"] = token;
                request.Headers["Accepts"] = "application/json";

                string responseTxt = String.Empty;
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    responseTxt = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                string s = "error";
            }
        }

        private async Task<ProfileModel> GetProfile(string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create("https://mvpstaging.com/api/profile/5001044") as HttpWebRequest;

                request.Method = "GET";
                request.Headers["Authorization"] = token;
                request.Headers["Accepts"] = "application/json";
                request.Headers["ACCESS_TYPE"] = "web";

                string responseTxt = String.Empty;
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    responseTxt = reader.ReadToEnd();

                    var profile = JsonConvert.DeserializeObject<ProfileModel>(responseTxt);
                    return profile;


                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
