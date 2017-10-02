using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.Models;
using Microsoft.Mvp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Microsoft.Mvpui
{
    public partial class MyProfile : ContentPage
    {

        #region Constructor

        public MyProfile()
        {
            InitializeComponent();
            this.BindingContext = MyProfileViewModel.Instance;

            if (Device.RuntimePlatform == Device.Windows || Device.RuntimePlatform == Device.WinPhone)
                ToolBarSettings.Icon = "Assets\\toolbar_settings.png";

            ToolBarSettings.Command = new Command(async () =>
            {
                await Navigation.PushModalAsync(new MVPNavigationPage(new SettingsPage()));
            });
        }

        #endregion

        #region Private and Protected Methods

        protected override bool OnBackButtonPressed()
        {
            App.SignOutHelper.CloseApp();
            return true;
        }

        Dictionary<string, object> cache = new Dictionary<string, object>();
        string currentUserIdKey = string.Empty;
        Dictionary<string, object> cacheItem = new Dictionary<string, object>();

        private async void GetProfile()
        {
            if (string.IsNullOrEmpty(MyProfileViewModel.Instance.FirstAwardValue))
            {
                try
                {
                    ProfileModel profile = null;

                    CheckCache();

                    CheckCacheItem();

                    if (cacheItem.ContainsKey(CommonConstants.ProfileCacheKey))
                    {
                        DateTime cachedDate = DateTime.Parse(cacheItem[CommonConstants.ProfileCacheDateKey].ToString());
                        DateTime ExpiredDate = cachedDate.AddHours(24);
                        if (DateTime.Compare(ExpiredDate, DateTime.Now) > 0) //Valid data.
                        {
                            string profileString = cacheItem[CommonConstants.ProfileCacheKey].ToString();
                            profile = Newtonsoft.Json.JsonConvert.DeserializeObject<ProfileModel>(profileString);
                        }
                        else
                        {
                            profile = await MvpHelper.MvpService.GetProfile(LogOnViewModel.StoredToken);

                            cacheItem[CommonConstants.ProfileCacheKey] = Newtonsoft.Json.JsonConvert.SerializeObject(profile);
                            cacheItem[CommonConstants.ProfileCacheDateKey] = DateTime.Now;

                            cache[currentUserIdKey] = cacheItem;
                        }
                    }
                    else
                    {
                        profile = await MvpHelper.MvpService.GetProfile(LogOnViewModel.StoredToken);

                        cacheItem.Add(CommonConstants.ProfileCacheKey, Newtonsoft.Json.JsonConvert.SerializeObject(profile));
                        cacheItem.Add(CommonConstants.ProfileCacheDateKey, DateTime.Now);

                        cache[currentUserIdKey] = cacheItem;
                    }

                    Application.Current.Properties[CommonConstants.ProfileCacheListKey] = cache;

                    if (profile != null)
                    {
                        MyProfileViewModel.Instance.FirstAwardValue = profile.FirstAwardYear.ToString(System.Globalization.CultureInfo.CurrentCulture);
                        MyProfileViewModel.Instance.PersonName = profile.DisplayName;
                        MyProfileViewModel.Instance.AwardCategoriesValue = profile.AwardCategoryDisplay.Replace(",", Environment.NewLine);
                        MyProfileViewModel.Instance.Description = profile.Biography;
                        MyProfileViewModel.Instance.AwardsCountValue = profile.YearsAsMVP.ToString(System.Globalization.CultureInfo.CurrentCulture);
                    }
                }
                finally
                {

                }
            }

        }

        private async void GetPhoto()
        {
            if (string.IsNullOrEmpty(MyProfileViewModel.Instance.StoreImageBase64Str))
            {
                try
                {
                    CheckCache();

                    CheckCacheItem();

                    if (cacheItem.ContainsKey(CommonConstants.ProfilePhotoCacheKey))
                    {
                        DateTime cachedDate = DateTime.Parse(cacheItem[CommonConstants.ProfilePhotoCacheDateKey].ToString());
                        DateTime ExpiredDate = cachedDate.AddHours(24);
                        if (DateTime.Compare(ExpiredDate, DateTime.Now) > 0) //Valid data.
                        {
                            MyProfileViewModel.Instance.StoreImageBase64Str = cacheItem[CommonConstants.ProfilePhotoCacheKey].ToString();
                        }
                        else
                        {
                            MyProfileViewModel.Instance.StoreImageBase64Str = await MvpHelper.MvpService.GetPhoto(LogOnViewModel.StoredToken);
                            cacheItem[CommonConstants.ProfilePhotoCacheKey] = MyProfileViewModel.Instance.StoreImageBase64Str;
                            cacheItem[CommonConstants.ProfilePhotoCacheDateKey] = DateTime.Now;
                            cache[currentUserIdKey] = cacheItem;
                        }
                    }
                    else
                    {
                        MyProfileViewModel.Instance.StoreImageBase64Str = await MvpHelper.MvpService.GetPhoto(LogOnViewModel.StoredToken);
                        cacheItem.Add(CommonConstants.ProfilePhotoCacheKey, MyProfileViewModel.Instance.StoreImageBase64Str);
                        cacheItem.Add(CommonConstants.ProfilePhotoCacheDateKey, DateTime.Now);
                        cache[currentUserIdKey] = cacheItem;
                    }

                    Application.Current.Properties[CommonConstants.ProfileCacheListKey] = cache;
                }
                finally
                {

                }
            }
        }

        private void CheckCache()
        {
            if (Application.Current.Properties.ContainsKey(CommonConstants.ProfileCacheListKey))
            {
                cache = (Dictionary<string, object>)Application.Current.Properties[CommonConstants.ProfileCacheListKey];
            }
            else
            {
                Application.Current.Properties.Add(CommonConstants.ProfileCacheListKey, cache);
            }
        }

        private void CheckCacheItem()
        {
            if (!Application.Current.Properties.ContainsKey(CommonConstants.CurrentUserIdKey))
                return;

            currentUserIdKey = Application.Current.Properties[CommonConstants.CurrentUserIdKey].ToString();
            if (cache.ContainsKey(currentUserIdKey))
            {
                cacheItem = (Dictionary<string, object>)cache[currentUserIdKey];
            }
            else
            {
                cache.Add(currentUserIdKey, cacheItem);
                Application.Current.Properties[CommonConstants.ProfileCacheListKey] = cache;
            }
        }

        protected async override void OnAppearing()
        {
            MyProfileViewModel.Instance.ErrorMessage = "";
            MyProfileViewModel.Instance.IsBusy = true;

            base.OnAppearing();
            GetPhoto();
            GetProfile();
            if (MyProfileViewModel.Instance.List == null || MyProfileViewModel.Instance.List.Count == 0)
            {
                var contributions = await MvpHelper.MvpService.GetContributions(-5, 10, LogOnViewModel.StoredToken);
                MvpHelper.SetContributionInfoToProfileViewModel(contributions);
            }

            if (string.Compare(CommonConstants.DefaultNetworkErrorString, MyProfileViewModel.Instance.ErrorMessage, StringComparison.OrdinalIgnoreCase) == 0)
            {
                MyProfileViewModel.Instance.StoreImageBase64Str = CommonConstants.DefaultPhoto;
                MyProfileViewModel.Instance.ErrorMessage = CommonConstants.DefaultNetworkErrorString;
            }

            MyProfileViewModel.Instance.IsBusy = false;
        }

        private async void OnLoadMoreClicked(object sender, EventArgs e)
        {
            int start = MyProfileViewModel.Instance.List.Count;

            var contributionInfo = await MvpHelper.MvpService.GetContributions(start, 5, LogOnViewModel.StoredToken);

            if (contributionInfo != null && contributionInfo.Contributions != null && contributionInfo.Contributions.Count > 0)
            {
                var contributors = contributionInfo.Contributions.Select(c =>
                {
                    MvpHelper.SetLabelTextOfContribution(c);
                    return c;
                });
                MyProfileViewModel.Instance.List.AddRange(contributors);

                MyProfileViewModel.Instance.TotalOfData = contributionInfo.TotalContributions;

            }
        }

        #endregion

    }
}
