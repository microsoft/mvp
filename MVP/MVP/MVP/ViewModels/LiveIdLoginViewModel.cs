using MVPUI.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MVP.ViewModels
{
    public class LiveIdLoginViewModel
    {
        #region Singleton pattern and constructors
        private static LiveIdLoginViewModel _instance = null;
        private static readonly object _synObject = new object();
        public static LiveIdLoginViewModel Instance
        {
            get
            {
                // Double-Checked Locking
                if (null == _instance)
                {
                    lock (_synObject)
                    {
                        if (null == _instance)
                        {
                            _instance = new LiveIdLoginViewModel();
                        }
                    }
                }
                return _instance;
            }
        }

        public LiveIdLoginViewModel()
        {

        }

        #endregion

        #region Private Members 
        string _liveIdLoginUrl =string.Format(CommonConstants.liveIdLoginUrlFormatString, CommonConstants._client_id, CommonConstants._scope);
        string _liveIdSignOutUrl = string.Format(CommonConstants.liveIdSignOutUrlFormatString, CommonConstants._client_id);
        string _accessTokenUrl = string.Format(CommonConstants.accessTokenUrlFormatString, CommonConstants._client_id, CommonConstants._client_secret);

        #endregion

        #region Public Members      

        public string LiveIdLoginUrl
        {
            get
            {
                return _liveIdLoginUrl;
            }

            set
            {
                _liveIdLoginUrl = value;
            }
        }

        #endregion

        #region Public Methods

        public async Task GetAccessToken()
        {
            if (Application.Current.Properties.ContainsKey(CommonConstants.AuthCodeKey))
            {
                await makeAccessTokenRequest(_accessTokenUrl + Application.Current.Properties[CommonConstants.AuthCodeKey]);
            }
        }

        public void SignOut()
        {
            Application.Current.Properties.Clear();
            LoginViewModel.StoredToken = string.Empty;
            MyProfileViewModel.Instance.StoreImageBase64Str = null;
            MyProfileViewModel.Instance.List = new System.Collections.ObjectModel.ObservableCollection<Models.ContributionModel>();

        }

        public async Task makeAccessTokenRequest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;

                string responseTxt = String.Empty;
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    responseTxt = reader.ReadToEnd();

                    var tokenData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseTxt);
                    if (tokenData.ContainsKey(CommonConstants.AccessTokenKey))
                    {
                        Application.Current.Properties.Add(CommonConstants.AccessTokenKey, tokenData[CommonConstants.AccessTokenKey]);
                        LoginViewModel.StoredToken = tokenData[CommonConstants.AccessTokenKey];

                        // refresh token
                        Application.Current.Properties.Add(CommonConstants.RefreshTokenKey, tokenData[CommonConstants.RefreshTokenKey]);

                        Application.Current.Properties[CommonConstants.TokenKey] = string.Format("{0},{1}", LoginViewModel.StoredToken, DateTime.Now);

                    }
                }
            }
            catch (Exception ex)
            {
                MyProfileViewModel.Instance.ErrorMessage = ex.Message;
            }
        }

        // TODO: this is example on how code is executed.  Should be refactored to be called when
        // we get a Forbidden response from API
        public async Task<string> GetNewAccessToken()
        {
            string refreshToken = Application.Current.Properties[CommonConstants.RefreshTokenKey].ToString();
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = false,
            };

            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("client_id", CommonConstants._client_id));
            postData.Add(new KeyValuePair<string, string>("client_secret", CommonConstants._client_secret));
            postData.Add(new KeyValuePair<string, string>(CommonConstants.RefreshTokenKey, refreshToken));
            postData.Add(new KeyValuePair<string, string>("redirect_uri", "https://login.live.com/oauth20_desktop.srf"));
            postData.Add(new KeyValuePair<string, string>("grant_type", CommonConstants.RefreshTokenKey));

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

                        if (tokenData.ContainsKey(CommonConstants.AccessTokenKey))
                        {
                            Application.Current.Properties.Remove(CommonConstants.AccessTokenKey);
                            Application.Current.Properties.Add(CommonConstants.AccessTokenKey, tokenData[CommonConstants.AccessTokenKey]);
                            // refresh token
                            Application.Current.Properties.Remove(CommonConstants.RefreshTokenKey);
                            Application.Current.Properties.Add(CommonConstants.RefreshTokenKey, tokenData[CommonConstants.RefreshTokenKey]);

                        }
                        return tokenData[CommonConstants.AccessTokenKey];
                    }

                }

                return string.Empty;
            }
        }
        #endregion

    }
}
