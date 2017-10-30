using System;
using System.Threading.Tasks;
using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.Models;
using MvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;
using System.Linq;
using Acr.UserDialogs;
using System.Collections.Generic;

namespace Microsoft.Mvp.ViewModels
{
    public class MyProfileViewModel : ViewModelBase
    {
        #region Singleton pattern and constructors

        private static MyProfileViewModel _instance = null;
        private static readonly object _synObject = new object();
        public static MyProfileViewModel Instance
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
                            _instance = new MyProfileViewModel();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Private Members

        private string _personName = string.Empty;
        private string _mvpNumber = string.Empty;
        private string _awardCategoriesValue = string.Empty;
        private string _firstAwardValue = string.Empty;
        private string _awardsCountValue = string.Empty;
        private string _description = string.Empty;
        private ObservableRangeCollection<ContributionModel> _list = new ObservableRangeCollection<ContributionModel>();
        private int _totalOfData = 100;
        private string _storeImageBase64Str;

        private string _ErrorMessage = string.Empty;
		#endregion

		#region Public Members

		public string PersonName
		{
			get => _personName;
			set => SetProperty<string>(ref _personName, value);
		}

        public string MvpNumber
        {
            get => _mvpNumber;
            set => SetProperty<string>(ref _mvpNumber, value);
        }

		public string AwardCategoriesValue
		{
			get => _awardCategoriesValue;
			set => SetProperty<string>(ref _awardCategoriesValue, value);
		}

		public string FirstAwardValue
		{
			get => _firstAwardValue;
			set => SetProperty<string>(ref _firstAwardValue, value);
		}

		public string AwardsCountValue
		{
			get => _awardsCountValue;
			set => SetProperty<string>(ref _awardsCountValue, value);
		}

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

		public ObservableRangeCollection<ContributionModel> List
		{
			get => _list;
			set
			{
				SetProperty(ref _list, value);
				CanLoadMore = HasMoreItems;
			}
		}

        public bool HasMoreItems => List.Count < TotalOfData;

		public int TotalOfData
		{
			get => _totalOfData;
			set
			{
				_totalOfData = value;
				CanLoadMore = HasMoreItems;
			}
		}

		public string StoreImageBase64Str
		{
			get => _storeImageBase64Str;
			set
            {
                SetProperty<string>(ref _storeImageBase64Str, value);
                OnPropertyChanged(nameof(ProfilePhoto));
            }
		}

        public ImageSource ProfilePhoto
        {
            get
            {
                ImageSource retSource = null;
                bool useDefault = false;
                if (StoreImageBase64Str != null)
                {
                    if (MvpHelper.MvpService is DesignMvpService)
                    {
                        retSource = ImageSource.FromUri(new Uri(StoreImageBase64Str));
                        return retSource;
                    }

                    try
                    {
                        var bytes2 = Convert.FromBase64String(StoreImageBase64Str);
                        retSource = ImageSource.FromStream(() => new System.IO.MemoryStream(bytes2));
                    }
                    catch
                    {
                        useDefault = true;
                    }
                }
                else
                {
                    useDefault = true;
                }

                if (useDefault)
                {
                    var bytes = Convert.FromBase64String(CommonConstants.DefaultPhoto);
                    retSource = ImageSource.FromStream(() => new System.IO.MemoryStream(bytes));
                }
                return retSource;
            }
        }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public string ErrorMessage
        {
            get => _ErrorMessage;
            set
            {
                _ErrorMessage = value;
                OnPropertyChanged("ErrorMessage");
                OnPropertyChanged("HasError");
            }
        }

		Command<ContributionModel> deleteCommand;
		public ICommand DeleteCommand => deleteCommand ?? (deleteCommand = new Command<ContributionModel>(async (c) => await ExecuteDeleteCommand(c)));

		async Task ExecuteDeleteCommand(ContributionModel contribution)
		{
			var remove = await Application.Current.MainPage.DisplayAlert("Delete Activity?", "Are you sure you want to delete this activity?", "Yes, Delete", "Cancel");
			if (!remove)
				return;

			string result = await MvpHelper.MvpService.DeleteContributionModel(Convert.ToInt32(contribution.ContributionId, System.Globalization.CultureInfo.InvariantCulture), LogOnViewModel.StoredToken);
			if (result == CommonConstants.OkResult)
			{
				var modelToDelete = List.Where(item => item.ContributionId == contribution.ContributionId).FirstOrDefault();
				List.Remove(modelToDelete);
			}
		}

		Command loadProfileCommand;
		public ICommand LoadProfileCommand => loadProfileCommand ?? (loadProfileCommand = new Command(async () => await ExecuteLoadProfileCommand()));

		bool loadedProfile;
		async Task ExecuteLoadProfileCommand()
		{
			if (MyProfileViewModel.Instance.IsBusy || loadedProfile)
				return;


			ErrorMessage = "";
			IsBusy = true;
			IProgressDialog progress = null;
			try
			{
				progress = UserDialogs.Instance.Loading("Loading profile...", maskType: MaskType.Clear);
				progress.Show();

				await GetPhoto();
				await GetProfile();

				if (string.Compare(CommonConstants.DefaultNetworkErrorString, ErrorMessage, StringComparison.OrdinalIgnoreCase) == 0)
				{
					StoreImageBase64Str = CommonConstants.DefaultPhoto;
					ErrorMessage = CommonConstants.DefaultNetworkErrorString;
				}
			}
			catch (Exception ex)
			{
				progress?.Hide();
				await UserDialogs.Instance.AlertAsync("Looks like something went wrong. Please check your connection.. Error: " + ex.Message, "Unable to load", "OK");

			}
			finally
			{
				progress?.Hide();
				IsBusy = false;
			}
		}


		Dictionary<string, object> cache = new Dictionary<string, object>();
		string currentUserIdKey = string.Empty;
		Dictionary<string, object> cacheItem = new Dictionary<string, object>();

		private async Task GetProfile()
		{
			
			if (string.IsNullOrEmpty(MyProfileViewModel.Instance.FirstAwardValue))
			{
				
				ProfileModel profile = null;

				CheckCache();

				CheckCacheItem();

				if (cacheItem.ContainsKey(CommonConstants.ProfileCacheKey))
				{
					var cachedDate = DateTime.Parse(cacheItem[CommonConstants.ProfileCacheDateKey].ToString());
					var ExpiredDate = cachedDate.AddHours(24);
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
					FirstAwardValue = profile.FirstAwardYear.ToString(System.Globalization.CultureInfo.CurrentCulture);
					PersonName = profile.DisplayName;
					MvpNumber = $"MVP {profile.MvpId}";
					AwardCategoriesValue = profile.AwardCategoryDisplay.Replace(",", Environment.NewLine);
					Description = profile.Biography;
					AwardsCountValue = profile.YearsAsMVP.ToString(System.Globalization.CultureInfo.CurrentCulture);
				}
				
			}

		}


		private async Task GetPhoto()
		{

			if (string.IsNullOrEmpty(StoreImageBase64Str))
			{
				try
				{
					CheckCache();

					CheckCacheItem();

					if (cacheItem.ContainsKey(CommonConstants.ProfilePhotoCacheKey))
					{
						var cachedDate = DateTime.Parse(cacheItem[CommonConstants.ProfilePhotoCacheDateKey].ToString());
						var ExpiredDate = cachedDate.AddHours(24);
						if (DateTime.Compare(ExpiredDate, DateTime.Now) > 0) //Valid data.
						{
							StoreImageBase64Str = cacheItem[CommonConstants.ProfilePhotoCacheKey].ToString();
						}
						else
						{
							StoreImageBase64Str = await MvpHelper.MvpService.GetPhoto(LogOnViewModel.StoredToken);
							cacheItem[CommonConstants.ProfilePhotoCacheKey] = MyProfileViewModel.Instance.StoreImageBase64Str;
							cacheItem[CommonConstants.ProfilePhotoCacheDateKey] = DateTime.Now;
							cache[currentUserIdKey] = cacheItem;
						}
					}
					else
					{
						StoreImageBase64Str = await MvpHelper.MvpService.GetPhoto(LogOnViewModel.StoredToken);
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
			if (Settings.GetSetting(CommonConstants.CurrentUserIdKey) == string.Empty)
				return;

			currentUserIdKey = Settings.GetSetting(CommonConstants.CurrentUserIdKey);
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

		Command refreshCommand;
		public ICommand RefreshCommand => refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand()));

		public async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;

			List.Clear();
			OnPropertyChanged("IsBusy");
			CanLoadMore = true;
			await ExecuteLoadMoreCommand();
		}
		
		public async Task ExecuteLoadMoreCommand()
		{
			if (!CanLoadMore || IsBusy)
				return;


			IProgressDialog progress = null;
			IsBusy = true;
			var index = List.Count;


			try
			{
				progress = UserDialogs.Instance.Loading("Loading Activities...", maskType: MaskType.Clear);
				progress?.Show();

				var contributions = await MvpHelper.MvpService.GetContributions(index, 50, LogOnViewModel.StoredToken);
				
				if (contributions.Contributions.Count != 0)
				{
					List.AddRange(contributions.Contributions);
				}
				CanLoadMore = contributions.Contributions.Count == 50;				
			}
			catch (Exception ex)
			{
			}
			finally
			{
				progress?.Hide();
				IsBusy = false;
			}
		}

		#endregion
	}
}
