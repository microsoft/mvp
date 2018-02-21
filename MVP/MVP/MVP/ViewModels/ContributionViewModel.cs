using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.Models;
using MvvmHelpers;
using System.Threading.Tasks;

namespace Microsoft.Mvp.ViewModels
{
	public class ContributionViewModel : ViewModelBase
	{

		#region Singleton pattern and constructors

		public ContributionViewModel()
		{
			ContributionTypeNames = new ObservableRangeCollection<ContributionTypeModel>();
			ContributionAreas = new ObservableRangeCollection<ContributionTechnologyModel>();

			VisibilityModel[] visibilityModels = new VisibilityModel[3] {
				  new VisibilityModel() { Description= "Everyone",Id=299600000,LocalizeKey=null },
				  new VisibilityModel() { Description= "MVP Community",Id=100000001,LocalizeKey=null },
				  new VisibilityModel() { Description=  "Microsoft" ,Id=100000000,LocalizeKey=null}
			};
			PersonGroups = new ObservableRangeCollection<VisibilityModel>(visibilityModels);


		}

		#endregion

		#region Private Members      

		private bool _isSecondAnnualQuantityVisible;
		private string _annualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextDefault);
		private string _secondAnnualQuantityTipText = string.Empty;
		private string _annualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextDefault);		 

		private ContributionModel _myContribution;
		private ContributionModel _myContributionBackup;

		private bool _isNeededUrl = false;
		private bool _isNeededAnnualQuantity = true;
		private bool _iNeededSecondAnnualQuantity = false;

		private string _errorMessageForTitle = CommonConstants.HighlightMessageText;
		private string _errorMessageForUrl = CommonConstants.HighlightMessageText;
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
			get => _isSecondAnnualQuantityVisible;
			set => SetProperty<bool>(ref _isSecondAnnualQuantityVisible, value);
		}

		public string AnnualQuantityTipText
		{
			get => _annualQuantityTipText;
			set => SetProperty<string>(ref _annualQuantityTipText, value);
		}

		public string SecondAnnualQuantityTipText
		{
			get => _secondAnnualQuantityTipText;
			set => SetProperty<string>(ref _secondAnnualQuantityTipText, value);
		}

		public string AnnualReachTipText
		{
			get => _annualReachTipText;
			set => SetProperty<string>(ref _annualReachTipText, value);
		}

		public ObservableRangeCollection<ContributionTypeModel> ContributionTypeNames { get; set; }

		public ObservableRangeCollection<ContributionTechnologyModel> ContributionAreas { get; set; }

		public ObservableRangeCollection<VisibilityModel> PersonGroups { get; set; }		

		public string LabelForContributionType { get; } = TranslateServices.GetResourceString(CommonConstants.LabelForContributionType);

		public string LabelForContributionArea { get; } = TranslateServices.GetResourceString(CommonConstants.LabelForContributionArea);

		public string LabelForContributionDate { get; } = TranslateServices.GetResourceString(CommonConstants.LabelForContributionDate);

		public string LabelForContributionTitle { get; } = TranslateServices.GetResourceString(CommonConstants.LabelForContributionTitle);

		public string LabelForURL { get; } = TranslateServices.GetResourceString(CommonConstants.LabelForURL);

		public string LabelForVisibility { get; } = TranslateServices.GetResourceString(CommonConstants.LabelForVisibility);

		public string LabelForDescription { get; } = TranslateServices.GetResourceString(CommonConstants.LabelForDescription);

		public string SaveButton { get; } = TranslateServices.GetResourceString(CommonConstants.SaveButton);

		public string CloseButton { get; } = TranslateServices.GetResourceString(CommonConstants.CloseButton);

		public ContributionModel MyContribution
		{
			get => _myContribution;

			set
			{
				SetProperty<ContributionModel>(ref _myContribution, value);

				if (value == null)
				{
					ContributionActionType = TranslateServices.GetResourceString(CommonConstants.ContributionDetailTitleForNew);
				}
				else
				{
					ContributionActionType = TranslateServices.GetResourceString(CommonConstants.ContributionDetailTitleForEditing);
				}
			}
		}

		private string _contributionActionType = TranslateServices.GetResourceString(CommonConstants.ContributionDetailTitleForNew);
		public string ContributionActionType
		{
			get => _contributionActionType;
			set => SetProperty<string>(ref _contributionActionType, value);
		}

		public ContributionModel MyContributionBackup
		{
			get => _myContributionBackup;
			set
			{
				if (_myContributionBackup != value)
				{
					SetProperty<ContributionModel>(ref _myContributionBackup, value);
				}
			}
		}

		public bool IsNeededUrl
		{
			get => _isNeededUrl;
			set => SetProperty<bool>(ref _isNeededUrl, value);
		}

		public bool IsNeededAnnualQuantity
		{
			get => _isNeededAnnualQuantity;
			set => SetProperty<bool>(ref _isNeededAnnualQuantity, value);
		}

		public bool IsNeededSecondAnnualQuantity
		{
			get => _iNeededSecondAnnualQuantity;
			set => SetProperty<bool>(ref _iNeededSecondAnnualQuantity, value);
		}

		public int ContributionTypeIndex
		{
			get => _contributionTypeIndex;
			set => _contributionTypeIndex = value;

			//TODO: Original code has no notifypropchanged? Why?
		}

		public int ContributionAreaIndex
		{
			get => _contributionAreaIndex;
			set => _contributionAreaIndex = value;

			//TODO: Original code has no notifypropchanged? Why?
		}

		public int VibilityIndex
		{
			get => _vibilityIndex;
			set => _vibilityIndex = value;

			//TODO: Original code has no notifypropchanged? Why?
		}

		public string ErrorMessageForTitle
		{
			get => _errorMessageForTitle;
			set => SetProperty<string>(ref _errorMessageForTitle, value);
		}

		public string ErrorMessageForUrl
		{
			get => _errorMessageForUrl;
			set => SetProperty<string>(ref _errorMessageForUrl, value);
		}

		public string ErrorMessageForAnnualQuantity
		{
			get => _errorMessageForAnnualQuantity;
			set => SetProperty<string>(ref _errorMessageForAnnualQuantity, value);
		}

		public string ErrorMessageForSecondAnnualQuantity
		{
			get => _errorMessageForSecondAnnualQuantity;
			set => SetProperty<string>(ref _errorMessageForSecondAnnualQuantity, value);
		}

		public string ErrorMessageForAnnualReach
		{
			get => _errorMessageForAnnualReach;
			set => SetProperty<string>(ref _errorMessageForAnnualReach, value);
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

		public async Task<bool> BindingContributionType()
		{
			try
			{
				var contributionTypeDetail = await MvpHelper.MvpService.GetContributionTypes(LogOnViewModel.StoredToken);
				ContributionTypeNames = new ObservableRangeCollection<ContributionTypeModel>(contributionTypeDetail.ContributionTypes);
			}
			catch (TaskCanceledException tce)
			{
				ErrorMessage = tce.Message;
			}
			return true;
		}

		public async Task<bool> BindingAreas()
		{
			try
			{
				var contributionDetail = await MvpHelper.MvpService.GetContributionAreas(LogOnViewModel.StoredToken);
				ContributionAreas = new ObservableRangeCollection<ContributionTechnologyModel>(contributionDetail.ContributionArea);
			}
			catch (TaskCanceledException tce)
			{
				ErrorMessage = tce.Message;
			}
			return true;
		}
		#endregion
	}
}
