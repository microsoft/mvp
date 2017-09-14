using Microsoft.Mvp.Models;
using Microsoft.Mvp.ViewModels;
using Microsoft.Mvpui.Helpers;
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
            InitializeComponent();
            this.BindingContext = MyProfileViewModel.Instance;
            listView.ItemAppearing += ListView_ItemAppearing;
            listView.ItemDisappearing += ListView_ItemDisappearing;


            var settingsGesture = new TapGestureRecognizer();
            settingsGesture.Tapped += SettingsGesture_Tapped;
            imageSettings.GestureRecognizers.Add(settingsGesture);

            if (Device.OS == TargetPlatform.iOS)
            {
                btnAdd.WidthRequest = 130;
                btnLoadMore.HeightRequest = 25;
            }
            else
            {
                btnAdd.WidthRequest = 170;
                btnLoadMore.HeightRequest = 35;
            }

        }

        #endregion

        #region Private and Protected Methods

        private async void SettingsGesture_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Settings());
        }

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
                            profile = await MvpService.GetProfile(LogOnViewModel.StoredToken);

                            cacheItem[CommonConstants.ProfileCacheKey] = Newtonsoft.Json.JsonConvert.SerializeObject(profile);
                            cacheItem[CommonConstants.ProfileCacheDateKey] = DateTime.Now;

                            cache[currentUserIdKey] = cacheItem;
                        }
                    }
                    else
                    {
                        profile = await MvpService.GetProfile(LogOnViewModel.StoredToken);

                        cacheItem.Add(CommonConstants.ProfileCacheKey, Newtonsoft.Json.JsonConvert.SerializeObject(profile));
                        cacheItem.Add(CommonConstants.ProfileCacheDateKey, DateTime.Now);

                        cache[currentUserIdKey] = cacheItem;
                    }

                    Application.Current.Properties[CommonConstants.ProfileCacheListKey] = cache;

                    if (profile != null)
                    {
                        MyProfileViewModel.Instance.FirstAwardValue = profile.FirstAwardYear.ToString(System.Globalization.CultureInfo.CurrentCulture);
                        MyProfileViewModel.Instance.PersonName = profile.DisplayName;
                        MyProfileViewModel.Instance.AwardCategoriesValue = profile.AwardCategoryDisplay;
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
                            MyProfileViewModel.Instance.StoreImageBase64Str = await MvpService.GetPhoto(LogOnViewModel.StoredToken);
                            cacheItem[CommonConstants.ProfilePhotoCacheKey] = MyProfileViewModel.Instance.StoreImageBase64Str;
                            cacheItem[CommonConstants.ProfilePhotoCacheDateKey] = DateTime.Now;
                            cache[currentUserIdKey] = cacheItem;
                        }
                    }
                    else
                    {
                        MyProfileViewModel.Instance.StoreImageBase64Str = await MvpService.GetPhoto(LogOnViewModel.StoredToken);
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
            listView.SelectedItem = null;
            
            MyProfileViewModel.Instance.ErrorMessage = "";
            stkOveryLay.IsVisible = true;

            base.OnAppearing();
            GetPhoto();
            GetProfile();
            if (MyProfileViewModel.Instance.List == null || MyProfileViewModel.Instance.List.Count == 0)
            {
                var contributions = await MvpService.GetContributions(-5, 10, LogOnViewModel.StoredToken);
                MvpHelper.SetContributionInfoToProfileViewModel(contributions);
                listView.HeightRequest = MyProfileViewModel.Instance.List.Count * 50;
            }

            if (string.Compare(CommonConstants.DefaultNetworkErrorString, MyProfileViewModel.Instance.ErrorMessage, StringComparison.OrdinalIgnoreCase) == 0)
            {
                MyProfileViewModel.Instance.StoreImageBase64Str = CommonConstants.DefaultPhoto;
                MyProfileViewModel.Instance.ErrorMessage = CommonConstants.DefaultNetworkErrorString;
            }

            stkOveryLay.IsVisible = false;
        }

        private void ListView_ItemDisappearing(object sender, ItemVisibilityEventArgs e)
        {
            //listView.HeightRequest = MyProfileViewModel.Instance.List.Count * 50;
        }

        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            listView.HeightRequest = MyProfileViewModel.Instance.List.Count * 50;
            MyProfileViewModel.Instance.OnPropertyChanged("HasMoreItems");
        }

        private async void OnAddContributionClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ContributionDetail());
        }

        private async void OnLoadMoreClicked(object sender, EventArgs e)
        {
            int start = MyProfileViewModel.Instance.List.Count;

            var contributionInfo = await Helpers.MvpService.GetContributions(start, 5, LogOnViewModel.StoredToken);

            if (contributionInfo != null && contributionInfo.Contributions != null && contributionInfo.Contributions.Count > 0)
            {
                foreach (var item in contributionInfo.Contributions)
                {
                    Helpers.MvpHelper.SetIconAndLabelTextOfContribution(item);
                    MyProfileViewModel.Instance.List.Add(item);
                }

                MyProfileViewModel.Instance.TotalOfData = contributionInfo.TotalContributions;

                listView.HeightRequest = MyProfileViewModel.Instance.List.Count * 50;
            }

        }

        #endregion

        #region Public Methods
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem!=null)
            {
                ContributionViewModel.Instance.MyContribution = e.SelectedItem as ContributionModel;
                await Navigation.PushModalAsync(
                    new ContributionDetail()
                    {
                        BindingContext = ContributionViewModel.Instance
                    });
            }          
        }

        public async void OnEdit(object sender, EventArgs eventArgs)
        {
            var mi = ((MenuItem)sender);
            ContributionViewModel.Instance.MyContribution = mi.CommandParameter as ContributionModel;
            await Navigation.PushModalAsync(
                new ContributionDetail()
                {
                    BindingContext = ContributionViewModel.Instance
                });
        }

        public async void OnDelete(object sender, EventArgs eventArgs)
        {
            var mi = ((MenuItem)sender);
            ContributionModel contribution = mi.CommandParameter as ContributionModel;

            string result = await MvpService.DeleteContributionModel(Convert.ToInt32(contribution.ContributionId, System.Globalization.CultureInfo.InvariantCulture), LogOnViewModel.StoredToken);
            if (result == CommonConstants.OkResult)
            {
                var modelToDelete = MyProfileViewModel.Instance.List.Where(item => item.ContributionId == contribution.ContributionId).FirstOrDefault();
                MyProfileViewModel.Instance.List.Remove(modelToDelete);

                listView.HeightRequest = MyProfileViewModel.Instance.List.Count * 50;
            }
        }
        #endregion

    }
}
