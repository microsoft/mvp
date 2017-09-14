using MVP.Models;
using MVPUI.Helpers;
using System.Collections.ObjectModel;

namespace MVP.ViewModels
{
    public class ContributionViewModel : ViewModelBase
    {

        #region Singleton pattern and constructors
        private static ContributionViewModel _instance = null;
        private static readonly object _synObject = new object();
        public static ContributionViewModel Instance
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
                            _instance = new ContributionViewModel();
                        }
                    }
                }
                return _instance;
            }
        }

        public ContributionViewModel()
        {
            ContributionTypeNames = new ObservableCollection<ContributionTypeModel>();
            ContributionAreas = new ObservableCollection<ContributionTechnologyModel>(); 

            VisibilityModel[] visibilityModels = new VisibilityModel[3] {
                  new VisibilityModel() { Description= "Everyone",Id=299600000,LocalizeKey=null },
                  new VisibilityModel() { Description= "MVP Community",Id=100000001,LocalizeKey=null },
                  new VisibilityModel() { Description=  "Microsoft" ,Id=100000000,LocalizeKey=null}
            };
            PersonGroups = new ObservableCollection<VisibilityModel>(visibilityModels);
        }


        #endregion

        #region Private Members      
        private bool _isSecondAnnualQuantityVisible;
        private string _annualQuantityTipText = CommonConstants.InitialOfAnnualQuantityTipText;
        private string _secondAnnualQuantityTipText = string.Empty;
        private string _annualReachTipText = CommonConstants.InitialOfAnnualReachTipText;
        private string _contributionAggregate;

        private ContributionModel _myContribution;
        private ContributionModel _myContributionBackup;

        private bool _isNeededURL = false;
        private bool _isNeededAnnualQuantity = true;
        private bool _iNeededSecondAnnualQuantity = false;

        private string _errorMessageForTitle = CommonConstants.HighlightMessageText;
        private string _errorMessageForURL = CommonConstants.HighlightMessageText;
        private string _errorMessageForAnnualQuantity = CommonConstants.HighlightMessageText;
        private string _errorMessageForSecondAnnualQuantity = CommonConstants.HighlightMessageText;
        private string _errorMessageForAnnualReach = "";

        private int _contributionTypeIndex = 0;
        private int _contributionAreaIndex = 0;
        private int _vibilityIndex = 0; 
  
        private string _ErrorMessage;

        #endregion

        #region Public Members


        public bool IsSecondAnnualQuantityVisible
        {
            get
            {
                return _isSecondAnnualQuantityVisible;
            }

            set
            {
                _isSecondAnnualQuantityVisible = value;
                OnPropertyChanged("IsSecondAnnualQuantityVisible");
            }
        }
        public string AnnualQuantityTipText
        {
            get
            {
                return _annualQuantityTipText;
            }

            set
            {
                _annualQuantityTipText = value;
                OnPropertyChanged("AnnualQuantityTipText");
            }
        }
        public string SecondAnnualQuantityTipText
        {
            get
            {
                return _secondAnnualQuantityTipText;
            }

            set
            {
                _secondAnnualQuantityTipText = value;
                OnPropertyChanged("SecondAnnualQuantityTipText");
            }
        }
        public string AnnualReachTipText
        {
            get
            {
                return _annualReachTipText;
            }

            set
            {
                _annualReachTipText = value;
                OnPropertyChanged("AnnualReachTipText");
            }
        }

        public ObservableCollection<ContributionTypeModel> ContributionTypeNames { get; set; }

        public ObservableCollection<ContributionTechnologyModel> ContributionAreas { get; set; }
        public ObservableCollection<VisibilityModel> PersonGroups { get; set; }

        public string ContributionAggregate
        {
            get
            {
                return _contributionAggregate;
            }

            set
            {
                _contributionAggregate = value;
            }
        }

        public ContributionModel MyContribution
        {
            get
            {
                return _myContribution;
            }

            set
            {
                _myContribution = value;
                OnPropertyChanged("MyContribution");
            }
        } 
 
        public ContributionModel MyContributionBackup
        {
            get
            {
                return _myContributionBackup;
            }

            set
            {
                if (_myContributionBackup != value)
                {
                    _myContributionBackup = value;
                    OnPropertyChanged("MyContributionBackup");
                }
            }
        }

        public bool IsNeededURL
        {
            get
            {
                return _isNeededURL;
            }

            set
            {
                _isNeededURL = value;
                OnPropertyChanged("IsNeededURL");
            }
        }

        public bool IsNeededAnnualQuantity
        {
            get
            {
                return _isNeededAnnualQuantity;
            }

            set
            {
                _isNeededAnnualQuantity = value;
                OnPropertyChanged("IsNeededAnnualQuantity");
            }
        }

        public bool IsNeededSecondAnnualQuantity
        {
            get
            {
                return _iNeededSecondAnnualQuantity;
            }

            set
            {
                _iNeededSecondAnnualQuantity = value;
                OnPropertyChanged("IsNeededSecondAnnualQuantity");
            }
        }

        public int ContributionTypeIndex
        {
            get
            {
                return _contributionTypeIndex;
            }

            set
            {
                _contributionTypeIndex = value;
            }
        }

        public int ContributionAreaIndex
        {
            get
            {
                return _contributionAreaIndex;
            }

            set
            {
                _contributionAreaIndex = value;
            }
        }

        public int VibilityIndex
        {
            get
            {
                return _vibilityIndex;
            }

            set
            {
                _vibilityIndex = value;
            }
        }

        public string ErrorMessageForTitle
        {
            get
            {
                return _errorMessageForTitle;
            }

            set
            {
                _errorMessageForTitle = value;
                OnPropertyChanged("ErrorMessageForTitle");
            }
        }

        public string ErrorMessageForURL
        {
            get
            {
                return _errorMessageForURL;
            }

            set
            {
                _errorMessageForURL = value;
                OnPropertyChanged("ErrorMessageForURL");
            }
        }

        public string ErrorMessageForAnnualQuantity
        {
            get
            {
                return _errorMessageForAnnualQuantity;
            }

            set
            {
                _errorMessageForAnnualQuantity = value;
                OnPropertyChanged("ErrorMessageForAnnualQuantity");
            }
        }

        public string ErrorMessageForSecondAnnualQuantity
        {
            get
            {
                return _errorMessageForSecondAnnualQuantity;
            }

            set
            {
                _errorMessageForSecondAnnualQuantity = value;
                OnPropertyChanged("ErrorMessageForSecondAnnualQuantity");
            }
        }

        public string ErrorMessageForAnnualReach
        {
            get
            {
                return _errorMessageForAnnualReach;
            }

            set
            {
                _errorMessageForAnnualReach = value;
                OnPropertyChanged("ErrorMessageForAnnualReach");
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
