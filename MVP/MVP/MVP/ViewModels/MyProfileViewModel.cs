using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.Models;
using Microsoft.Mvpui.Helpers;
using MvvmHelpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        public async Task<bool> BindingContributionType()
        {
            try
            {
                ContributionTypeDetail contributionTypeDetail = await MvpHelper.MvpService.GetContributionTypes(LogOnViewModel.StoredToken);
                ContributionViewModel.Instance.ContributionTypeNames = new ObservableRangeCollection<ContributionTypeModel>(contributionTypeDetail.ContributionTypes);

            }
            catch (TaskCanceledException tce)
            {
                ContributionViewModel.Instance.ErrorMessage = tce.Message;
            }
            return true;
        }

        public async Task<bool> BindingAreas()
        {
            try
            {
                ContributionDetail contributionDetail = await MvpHelper.MvpService.GetContributionAreas(LogOnViewModel.StoredToken);
                ContributionViewModel.Instance.ContributionAreas = new ObservableRangeCollection<ContributionTechnologyModel>(contributionDetail.ContributionArea);
            }
            catch (TaskCanceledException tce)
            {
                ContributionViewModel.Instance.ErrorMessage = tce.Message;
            }
            return true;
        }





        #endregion

        #region Private Members
        private string _strWPResourcePath;
        private string _strAvatar = "user1.jpg";
        private string _strAvatarBackground = "mvplogoplus.png";
        private string _strBenefitsImage = "Benefits.png";
        private string _strMyNetworkImage = "MyNetwork.png";
        private string _strMeImage = "Me.png";
        private string _strNotificationsImage = "Notifications.png";
        private string _strSupportImage = "Support.png";
        private string _strSettingsImage = "icon_settings.png";

        private string _personName = string.Empty;
        private string _awardCategoriesTip = "Award Categories:";
        private string _awardCategoriesValue = string.Empty;
        private string _firstAwardTip = "First year awarded:";
        private string _firstAwardValue = string.Empty;
        private string _awardsCountTip = "Number of MVP Awards:";
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
            get
            {
                return _personName;
            }

            set
            {
                _personName = value;
                OnPropertyChanged("PersonName");
            }
        }

        public string AwardCategoriesTip
        {
            get
            {
                return _awardCategoriesTip;
            }

            set
            {
                _awardCategoriesTip = value;
                OnPropertyChanged("AwardCategoriesTip");
            }
        }

        public string AwardCategoriesValue
        {
            get
            {
                return _awardCategoriesValue;
            }

            set
            {
                _awardCategoriesValue = value;
                OnPropertyChanged("AwardCategoriesValue");
            }
        }

        public string FirstAwardTip
        {
            get
            {
                return _firstAwardTip;
            }

            set
            {
                _firstAwardTip = value;
                OnPropertyChanged("FirstAwardTip");
            }
        }

        public string FirstAwardValue
        {
            get
            {
                return _firstAwardValue;
            }

            set
            {
                _firstAwardValue = value;
                OnPropertyChanged("FirstAwardValue");
            }
        }

        public string AwardsCountTip
        {
            get
            {
                return _awardsCountTip;
            }

            set
            {
                _awardsCountTip = value;
                OnPropertyChanged("AwardsCountTip");
            }
        }

        public string AwardsCountValue
        {
            get
            {
                return _awardsCountValue;
            }

            set
            {
                _awardsCountValue = value;
                OnPropertyChanged("AwardsCountValue");
            }
        }

        public string Description
        {
            get => _description;


            set => SetProperty(ref _description, value);

        }

        public ObservableRangeCollection<ContributionModel> List
        {
            get
            {
                return _list;
            }

            set
            {
                SetProperty(ref _list, value);
                CanLoadMore = HasMoreItems;
            }
        }

        public string StrBenefitsImage
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", StrWPResourcePath, _strBenefitsImage);
            }

            set
            {
                _strBenefitsImage = value;
                OnPropertyChanged("StrBenefitsImage");
            }
        }

        public string StrMyNetworkImage
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", StrWPResourcePath, _strMyNetworkImage);
            }

            set
            {
                _strMyNetworkImage = value;
                OnPropertyChanged("StrMyNetworkImage");
            }
        }

        public string StrMeImage
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", StrWPResourcePath, _strMeImage);
            }

            set
            {
                _strMeImage = value;
                OnPropertyChanged("StrMeImage");
            }
        }

        public string StrNotificationsImage
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", StrWPResourcePath, _strNotificationsImage);
            }

            set
            {
                _strNotificationsImage = value;
                OnPropertyChanged("StrNotificationsImage");
            }
        }

        public string StrSupportImage
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", StrWPResourcePath, _strSupportImage);
            }

            set
            {
                _strSupportImage = value;
                OnPropertyChanged("StrSupportImage");
            }
        }

        public string StrSettingImage
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", StrWPResourcePath, _strSettingsImage);
            }

            set
            {
                _strSettingsImage = value;
                OnPropertyChanged("StrSettingImage");
            }
        }

        public string StrWPResourcePath
        {
            get
            {
                return _strWPResourcePath;
            }

            set
            {
                _strWPResourcePath = value;
            }
        }

        public string StrAvatar
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", StrWPResourcePath, _strAvatar);
            }

            set
            {
                _strAvatar = value;
                OnPropertyChanged("StrAvatar");
            }
        }

        public string StrAvatarBackground
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", StrWPResourcePath, _strAvatarBackground);
            }

            set
            {
                _strAvatarBackground = value;
                OnPropertyChanged("StrAvatarBackground");
            }
        }

        public bool HasMoreItems
        {
            get
            {
                return List.Count < TotalOfData;
            }
        }


        public int TotalOfData
        {
            get
            {
                return _totalOfData;
            }

            set
            {
                _totalOfData = value;
                CanLoadMore = HasMoreItems;
            }
        }

        public string StoreImageBase64Str
        {
            get
            {
                return _storeImageBase64Str;
            }

            set
            {
                _storeImageBase64Str = value;
                OnPropertyChanged("ProfilePhoto");
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

        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(ErrorMessage);
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }

            set
            {
                _ErrorMessage = value;
                OnPropertyChanged("ErrorMessage");
                OnPropertyChanged("HasError");
            }
        }
        #endregion

        #region Methods


        #endregion
    }
}
