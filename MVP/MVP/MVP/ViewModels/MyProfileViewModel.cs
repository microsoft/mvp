using System;
using System.Threading.Tasks;
using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.Models;
using MvvmHelpers;
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

        private string _personName = string.Empty;
        private string _awardCategoriesValue = string.Empty;
        private string _firstAwardValue = string.Empty;
        private string _awardsCountValue = string.Empty;
        private string _description = string.Empty;
        private bool _isLoading = false;
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
    }
}
