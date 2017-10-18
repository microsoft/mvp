using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Mvp.Helpers;
using MvvmHelpers;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Microsoft.Mvp.ViewModels
{
    public class LiveIdLogOnViewModel
    {
        #region Singleton pattern and constructors

        private static LiveIdLogOnViewModel _instance = null;
        private static readonly object _synObject = new object();

        public static LiveIdLogOnViewModel Instance
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
                            _instance = new LiveIdLogOnViewModel();
                        }
                    }
                }
                return _instance;
            }
        }

        public LiveIdLogOnViewModel()
        {

        }

        #endregion

        #region Private Members 

        string _liveIdLogOnUrl = string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.LiveIdLogOnUrlFormatString, CommonConstants.ClientId, CommonConstants.Scope);
        string _liveIdSignOutUrl = string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.LiveIdSignOutUrlFormatString, CommonConstants.ClientId);
        string _accessTokenUrl = string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.AccessTokenUrlFormatString, CommonConstants.ClientId, CommonConstants.ClientSecret);

        #endregion

        #region Public Members      

        public string LiveIdLogOnUrl
        {
            get
            {
                return _liveIdLogOnUrl;
            }

            set
            {
                _liveIdLogOnUrl = value;
            }
        }

        #endregion

        #region Public Methods

        public async Task GetAccessToken()
        {
            if (Settings.GetSetting(CommonConstants.AuthCodeKey) != string.Empty)
            {
                await MakeAccessTokenRequest(_accessTokenUrl + Settings.GetSetting(CommonConstants.AuthCodeKey));
            }
        }

        public void SignOut()
        {
            MvpHelper.RemoveProperties();

			if (Application.Current.Properties.ContainsKey(CommonConstants.ProfileCacheListKey))
				Application.Current.Properties.Remove(CommonConstants.ProfileCacheListKey);

			LogOnViewModel.StoredToken = string.Empty;
            MyProfileViewModel.Instance.FirstAwardValue = string.Empty;
            MyProfileViewModel.Instance.PersonName = string.Empty;
            MyProfileViewModel.Instance.AwardCategoriesValue = string.Empty;
            MyProfileViewModel.Instance.Description = string.Empty;
            MyProfileViewModel.Instance.AwardsCountValue = string.Empty;

            MyProfileViewModel.Instance.StoreImageBase64Str = null;
            MyProfileViewModel.Instance.List = new ObservableRangeCollection<Models.ContributionModel>();

        }

        public async Task MakeAccessTokenRequest(Uri requestUri)
        {
            await MakeAccessTokenRequest(requestUri.AbsoluteUri);
        }

        public async Task MakeAccessTokenRequest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;

                string responseTxt = String.Empty;
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseTxt = reader.ReadToEnd();

                        var tokenData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseTxt);
                        if (tokenData.ContainsKey(CommonConstants.AccessTokenKey))
                        {
							Settings.SetSetting(CommonConstants.AccessTokenKey, tokenData[CommonConstants.AccessTokenKey]);
                            LogOnViewModel.StoredToken = tokenData[CommonConstants.AccessTokenKey];

							// refresh token
							Settings.SetSetting(CommonConstants.RefreshTokenKey, tokenData[CommonConstants.RefreshTokenKey]);

							Settings.SetSetting(CommonConstants.TokenKey, string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0},{1}", LogOnViewModel.StoredToken, DateTime.Now));

                        }

                        if (tokenData.ContainsKey(CommonConstants.CurrentUserIdKey))
                        {
							Settings.SetSetting(CommonConstants.CurrentUserIdKey, tokenData[CommonConstants.CurrentUserIdKey]);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                MyProfileViewModel.Instance.ErrorMessage = ex.Message;
            }
            catch (HttpRequestException ex)
            {
                MyProfileViewModel.Instance.ErrorMessage = ex.Message;
            }
        }

        // TODO: this is example on how code is executed.  Should be refactored to be called when
        // we get a Forbidden response from API
        public static async Task<string> GetNewAccessToken()
        {
            string refreshToken = Settings.GetSetting(CommonConstants.RefreshTokenKey);
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = false,
            };

            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("client_id", CommonConstants.ClientId));
            postData.Add(new KeyValuePair<string, string>("client_secret", CommonConstants.ClientSecret));
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
							Settings.SetSetting(CommonConstants.AccessTokenKey, tokenData[CommonConstants.AccessTokenKey]);

							// refresh token
							Settings.SetSetting(CommonConstants.RefreshTokenKey, tokenData[CommonConstants.RefreshTokenKey]);

							LogOnViewModel.StoredToken = Settings.GetSetting(CommonConstants.AccessTokenKey);

							Settings.SetSetting(CommonConstants.TokenKey, string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0},{1}", LogOnViewModel.StoredToken, DateTime.Now));

						}

						if (tokenData.ContainsKey(CommonConstants.CurrentUserIdKey))
						{
							Settings.SetSetting(CommonConstants.CurrentUserIdKey, tokenData[CommonConstants.CurrentUserIdKey]);
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
