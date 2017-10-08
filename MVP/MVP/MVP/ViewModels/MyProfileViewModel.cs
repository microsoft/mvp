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
            catch (TaskCanceledException tce) {
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
            catch(TaskCanceledException tce)
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
        private string _strSettingsImage = "Settings.png";

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
            get => _personName;
            set => SetProperty<string>(ref _personName, value);
        }

        public string AwardCategoriesTip
        {
            get => _awardCategoriesTip;
            set => SetProperty<string>(ref _awardCategoriesTip, value);
        }

        public string AwardCategoriesValue
        {
            get => _awardCategoriesValue;
            set => SetProperty<string>(ref _awardCategoriesValue, value);
        }

        public string FirstAwardTip
        {
            get => _firstAwardTip;
            set => SetProperty<string>(ref _firstAwardTip, value);
        }

        public string FirstAwardValue
        {
            get => _firstAwardValue;
            set => SetProperty<string>(ref _firstAwardValue, value);
        }

        public string AwardsCountTip
        {
            get => _awardsCountTip;
            set => SetProperty<string>(ref _awardsCountTip, value);
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

        public string StrBenefitsImage
        {
            get => string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}{1}", StrWPResourcePath, _strBenefitsImage);
            set => SetProperty<string>(ref _strBenefitsImage, value);
        }

        public string StrMyNetworkImage
        {
            get => string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}{1}", StrWPResourcePath, _strMyNetworkImage);
            set => SetProperty<string>(ref _strMyNetworkImage, value);
        }

        public string StrMeImage
        {
            get => string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}{1}", StrWPResourcePath, _strMeImage);
            set => SetProperty<string>(ref _strMeImage, value);
        }

        public string StrNotificationsImage
        {
            get => string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}{1}", StrWPResourcePath, _strNotificationsImage);
            set => SetProperty<string>(ref _strNotificationsImage, value);
        }

        public string StrSupportImage
        {
            get => string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}{1}", StrWPResourcePath, _strSupportImage);
            set => SetProperty<string>(ref _strSupportImage, value);
        }

        public string StrSettingImage
        {
            get => string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}{1}", StrWPResourcePath, _strSettingsImage);
            set => SetProperty<string>(ref _strSettingsImage, value);
        }

        public string StrWPResourcePath
        {
            get => _strWPResourcePath;
            set
            {
                _strWPResourcePath = value;

                //TODO: No property change notification in original code? Why?
            }
        }

        public string StrAvatar
        {
            get => string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}{1}", StrWPResourcePath, _strAvatar);
            set => SetProperty<string>(ref _strAvatar, value);
        }

        public string StrAvatarBackground
        {
            get => string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0}{1}", StrWPResourcePath, _strAvatarBackground);
            set => SetProperty<string>(ref _strAvatarBackground, value);
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
            set => SetProperty<string>(ref _storeImageBase64Str, value);
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
        #endregion

        #region Methods


        #endregion
    }
}
