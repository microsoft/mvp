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
		private string _tabTitleForContributions = TranslateServices.GetResourceString(CommonConstants.TabTitleForContributions);
		private string _tabTitleForProfile = TranslateServices.GetResourceString(CommonConstants.TabTitleForProfile);
		private ObservableRangeCollection<ContributionModel> _list = new ObservableRangeCollection<ContributionModel>();

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
				byte[] bytes = default(byte[]);

				if (StoreImageBase64Str != null)
				{
					try
					{
						bytes = Convert.FromBase64String(StoreImageBase64Str);
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
					bytes = Convert.FromBase64String(CommonConstants.DefaultPhoto);
				}

				retSource = ImageSource.FromStream(() => new System.IO.MemoryStream(bytes));
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
			var remove = await Application.Current.MainPage.DisplayAlert(TranslateServices.GetResourceString(CommonConstants.DialogTitleForDelete), TranslateServices.GetResourceString(CommonConstants.DialogDescriptionForDelete), TranslateServices.GetResourceString(CommonConstants.DialogConfirmTextForDelete), TranslateServices.GetResourceString(CommonConstants.DialogCancel));
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
				progress = UserDialogs.Instance.Loading(TranslateServices.GetResourceString(CommonConstants.DialogTitleForLoadingProfile), maskType: MaskType.Clear);
				progress.Show();

				await GetPhoto();
				await GetProfile();

				if (string.Compare(TranslateServices.GetResourceString(CommonConstants.DefaultNetworkErrorString), ErrorMessage, StringComparison.OrdinalIgnoreCase) == 0)
				{
					StoreImageBase64Str = CommonConstants.DefaultPhoto;
					ErrorMessage = TranslateServices.GetResourceString(CommonConstants.DefaultNetworkErrorString);
				}
			}
			catch (Exception ex)
			{
				progress?.Hide();
				await UserDialogs.Instance.AlertAsync(TranslateServices.GetResourceString(CommonConstants.DialogDescriptionForCheckNetworkFormat), TranslateServices.GetResourceString(CommonConstants.DialogOK));

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

		public string TabTitleForContributions
		{
			get => _tabTitleForContributions;
			set => _tabTitleForContributions = value;
		}
		public string TabTitleForProfile
		{
			get => _tabTitleForProfile;
			set => _tabTitleForProfile = value;
		}
		public string LabelForAwardCategories { get; } = TranslateServices.GetResourceString(CommonConstants.LabelForAwardCategories);
		public string LabelForFirstAward { get; } = TranslateServices.GetResourceString(CommonConstants.LabelForFirstAward);
		public string LabelForNumberOfAward { get; } = TranslateServices.GetResourceString(CommonConstants.LabelForNumberOfAward);

		public string AddButton { get; } = TranslateServices.GetResourceString(CommonConstants.AddButton);
		public string DeleteButton { get; } = TranslateServices.GetResourceString(CommonConstants.DeleteButton);

		public string SettingsButton { get; } = TranslateServices.GetResourceString(CommonConstants.SettingsButton);

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
			var index = List.Count;

			try
			{
				progress = UserDialogs.Instance.Loading(TranslateServices.GetResourceString(CommonConstants.DialogTitleForLoadingContribution), maskType: MaskType.Clear);
				progress?.Show();

				var contributions = await MvpHelper.MvpService.GetContributions(index, 20, LogOnViewModel.StoredToken);

				if (contributions.Contributions.Count != 0)
				{
					var finalList = contributions.Contributions.Select(c =>
					{
						MvpHelper.SetLabelTextOfContribution(c);
						return c;
					});

					List.AddRange(finalList);
				}

				CanLoadMore = contributions.Contributions.Count == 20;

                progress?.Hide();
			}
			catch (Exception ex)
			{
                progress?.Hide();
				await UserDialogs.Instance.AlertAsync(string.Format(TranslateServices.GetResourceString(CommonConstants.DialogDescriptionForCheckNetworkFormat1), ex.Message), TranslateServices.GetResourceString(CommonConstants.DialogOK));
			}
		}

		#endregion
	}
}