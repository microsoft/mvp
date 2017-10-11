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

        #endregion
    }
}
