using Acr.UserDialogs;
using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.Models;
using Microsoft.Mvp.ViewModels;
using MvvmHelpers;
using Plugin.Connectivity;
//using Plugin.Geolocator;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppResources = Microsoft.Mvp.Resources.AppResources;
//using Xamarin.Forms.Maps;

namespace Microsoft.Mvpui
{
    public partial class ContributionDetail : ContentPage
    {

        #region Private Fields
		
		ContributionViewModel viewModel;
		ContributionViewModel ViewModel
			=> viewModel ?? (viewModel = BindingContext as ContributionViewModel);
	
		#endregion

		#region Constructor

		public ContributionDetail()
		{
			Logger.Log("Page-ContributionDetail");
			InitializeComponent();

            ToolbarClose.Command = new Command(async () => await Navigation.PopModalAsync());

			if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WinPhone)
				ToolbarClose.Icon = "Assets\\toolbar_close.png";

            this.BindingContext = new ContributionViewModel();

            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
            {
                PersonGroupSelector.HeightRequest = 40;
                entrySecondAnnualQuantity.HeightRequest = 40;
                entryAnnualReach.HeightRequest = 40;
                entryAnnualQuantity.HeightRequest = 40;
            }

			ContributionDateSelector.Date = DateTime.Today;



		}

        #endregion

        #region Private and Protected Methods

        protected async override void OnAppearing()
        {
			if (IsBusy)
				return;

			IsBusy = true;
			ViewModel.ErrorMessage = "";
            base.OnAppearing();
			IProgressDialog progress = null;
            try
            {
				progress = UserDialogs.Instance.Loading(TranslateServices.GetResourceString(CommonConstants.DialogLoading), maskType: MaskType.Clear);
				progress.Show();

				InitContributionType();
                await BindContributionAreas();
                BindingSelectors();
            }
			catch(Exception ex)
			{
				await UserDialogs.Instance.AlertAsync(string.Format(CommonConstants.DialogDescriptionForCheckNetworkFormat,ex.Message), TranslateServices.GetResourceString(CommonConstants.DialogOK));
			}
            finally
            {
				progress?.Hide();
                IsBusy = false;
            }
        }

        private async void InitContributionType()
        {
			try
			{
				await BindContributionType();
			}
			catch (Exception ex)
			{ }

        }


        private async Task BindContributionAreas()
        {
			if(ViewModel == null)
				return;
				
            if (ViewModel.ContributionAreas == null || ViewModel.ContributionAreas.Count == 0)
            {
                await ViewModel.BindingAreas();
            }
        }

        private async Task BindContributionType()
        {
			if(ViewModel == null)
				return;
				
            if (ViewModel.ContributionTypeNames == null || ViewModel.ContributionTypeNames.Count == 0)
            {
                await ViewModel.BindingContributionType();
            }

            BindingContributionType();
        }


        private void BindingContributionType()
        {
            contributionTypeSelector.Items.Clear();

            foreach (var item in ViewModel.ContributionTypeNames)
            {
                contributionTypeSelector.Items.Add(item.Name);
            }

            if (ViewModel.MyContribution != null)
            {
                if (ViewModel.ContributionTypeNames.Count > 0)
                {
                    var activeContributionType = ViewModel.ContributionTypeNames.Where(item => item.Id == ViewModel.MyContribution.ContributionType.Id).FirstOrDefault();
                    ViewModel.ContributionTypeIndex = ViewModel.ContributionTypeNames.IndexOf(activeContributionType);
                }
            }

            if ((BindingContext as ContributionViewModel).MyContribution == null)
            {
                contributionTypeSelector.SelectedIndex = 0;
            }
            else
            {
                contributionTypeSelector.SelectedIndex = ViewModel.ContributionTypeIndex;
            }
        }

        private void BindingSelectors()
        {

            ContributionAreaSelector.Items.Clear();
            PersonGroupSelector.Items.Clear();

            foreach (var item in ViewModel.ContributionAreas)
            {
                ContributionAreaSelector.Items.Add(item.Name);
            }
            foreach (var item in ViewModel.PersonGroups)
            {
                PersonGroupSelector.Items.Add(item.Description);
            }

            if (ViewModel.MyContribution != null)
            {

                if (ViewModel.ContributionAreas.Count > 0)
                {
                    var activeContributionArea = ViewModel.ContributionAreas.Where(item => item.Id == ViewModel.MyContribution.ContributionTechnology.Id).FirstOrDefault();
                    ViewModel.ContributionAreaIndex = ViewModel.ContributionAreas.IndexOf(activeContributionArea);
                }

                if (ViewModel.PersonGroups.Count > 0)
                {
                    var activeVisibility = ViewModel.PersonGroups.Where(item => item.Id == ViewModel.MyContribution.Visibility.Id).FirstOrDefault();
                    ViewModel.VibilityIndex = ViewModel.PersonGroups.IndexOf(activeVisibility);
                }
            }

            if ((BindingContext as ContributionViewModel).MyContribution == null)
            {

                //ContributionAreaSelector.SelectedIndex = 0;
                var activeContributionType = ViewModel.ContributionAreas.Where(item => string.Compare(item.AwardName, MyProfileViewModel.Instance.AwardCategoriesValue, StringComparison.CurrentCultureIgnoreCase) == 0).FirstOrDefault();
                ContributionAreaSelector.SelectedIndex = ViewModel.ContributionAreas.IndexOf(activeContributionType);
            }
            else
            {
                ContributionAreaSelector.SelectedIndex = ViewModel.ContributionAreaIndex;
            }

            PersonGroupSelector.SelectedIndex = ViewModel.VibilityIndex;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            var contributionInfo = BindingContext as ContributionViewModel;

            if (contributionInfo != null && contributionInfo.MyContribution != null)
            {
                var backup = new ContributionModel();
                backup.ContributionType = new ContributionTypeModel() { Id = contributionInfo.MyContribution.ContributionType.Id, Name = contributionInfo.MyContribution.ContributionType.Name, EnglishName = contributionInfo.MyContribution.ContributionType.EnglishName };
                backup.Visibility = new VisibilityModel() { Id = contributionInfo.MyContribution.Visibility.Id, Description = contributionInfo.MyContribution.Visibility.Description, LocalizeKey = contributionInfo.MyContribution.Visibility.LocalizeKey };
                backup.ContributionTechnology = new ContributionTechnologyModel() { Id = contributionInfo.MyContribution.ContributionTechnology.Id, Name = contributionInfo.MyContribution.ContributionTechnology.Name, AwardName = contributionInfo.MyContribution.ContributionTechnology.AwardName, Active = contributionInfo.MyContribution.ContributionTechnology.Active, Statuscode = contributionInfo.MyContribution.ContributionTechnology.Statuscode };
                backup.AllAnswersUrl = contributionInfo.MyContribution.AllAnswersUrl;
                backup.AllPostsUrl = contributionInfo.MyContribution.AllPostsUrl;
                backup.AnnualQuantity = contributionInfo.MyContribution.AnnualQuantity;
                backup.SecondAnnualQuantity = contributionInfo.MyContribution.SecondAnnualQuantity;
                backup.AnnualReach = contributionInfo.MyContribution.AnnualReach;
                backup.ContributionId = contributionInfo.MyContribution.ContributionId;
                backup.StartDate = contributionInfo.MyContribution.StartDate;
                backup.Description = contributionInfo.MyContribution.Description;
                backup.IsBelongToLatestAwardCycle = contributionInfo.MyContribution.IsBelongToLatestAwardCycle;
                backup.IsSystemCollected = contributionInfo.MyContribution.IsSystemCollected;
                backup.LabelTextOfContribution = contributionInfo.MyContribution.LabelTextOfContribution;
                backup.ReferenceUrl = contributionInfo.MyContribution.ReferenceUrl;
                backup.Title = contributionInfo.MyContribution.Title;
                contributionInfo.MyContributionBackup = backup;
            }
        }

        public async void ButtonSaveClicked(object sender, EventArgs e)
        {
			

			IProgressDialog progress = null;
            try
            {

               
                bool isValid = CheckData();
                if (!isValid)
                {
                    return;
                }

				if (!CrossConnectivity.Current.IsConnected)
				{
					await UserDialogs.Instance.AlertAsync(TranslateServices.GetResourceString(CommonConstants.DialogDescriptionForCheckNetworkFormat1), TranslateServices.GetResourceString(CommonConstants.DialogTitleForCheckNetwork), TranslateServices.GetResourceString(CommonConstants.DialogOK));

					return;
				}

				IsBusy = true;
				progress = UserDialogs.Instance.Loading(TranslateServices.GetResourceString(CommonConstants.DialogSaving), maskType: MaskType.Clear);
				progress.Show();
                

                if (ViewModel.MyContribution == null)
                {
                    var model = new ContributionModel()
                    {
                        ContributionId = "0",
                        ContributionType = ViewModel.ContributionTypeNames[contributionTypeSelector.SelectedIndex],
                        ContributionTechnology = ViewModel.ContributionAreas[ContributionAreaSelector.SelectedIndex],
                        Visibility = ViewModel.PersonGroups[PersonGroupSelector.SelectedIndex],
                        StartDate = ContributionDateSelector.Date.ToUniversalTime(),
                        Title = entryTitle.Text,
                        ReferenceUrl = entryURL.Text,
                        Description = entryDescription.Text,
                        AnnualQuantity = Convert.ToInt32(entryAnnualQuantity.Text, System.Globalization.CultureInfo.InvariantCulture),
                        AnnualReach = Convert.ToInt32(entryAnnualReach.Text, System.Globalization.CultureInfo.InvariantCulture),
                        SecondAnnualQuantity = Convert.ToInt32(entrySecondAnnualQuantity.Text, System.Globalization.CultureInfo.InvariantCulture)
                    };
                    var result = await MvpHelper.MvpService.AddContributionModel(model, LogOnViewModel.StoredToken);
                    if (result != null && result.ContributionId != "0")
                    {

						Logger.Log("Activity-Added");
						MvpHelper.SetLabelTextOfContribution(result);
                        MyProfileViewModel.Instance.List.Insert(0, result);
                    }
                    else
                    {
						throw new Exception(MyProfileViewModel.Instance.ErrorMessage); 
                    }
                }
                else
                {

                    ViewModel.MyContribution.ContributionType = ViewModel.ContributionTypeNames[contributionTypeSelector.SelectedIndex];
                    ViewModel.MyContribution.ContributionTechnology = ViewModel.ContributionAreas[ContributionAreaSelector.SelectedIndex];
                    ViewModel.MyContribution.Visibility = ViewModel.PersonGroups[PersonGroupSelector.SelectedIndex];
                    ViewModel.MyContribution.StartDate = ContributionDateSelector.Date.ToUniversalTime();
                    ViewModel.MyContribution.AnnualQuantity = Convert.ToInt32(entryAnnualQuantity.Text, System.Globalization.CultureInfo.InvariantCulture);
                    ViewModel.MyContribution.AnnualReach = Convert.ToInt32(entryAnnualReach.Text, System.Globalization.CultureInfo.InvariantCulture);
                    ViewModel.MyContribution.SecondAnnualQuantity = Convert.ToInt32(entrySecondAnnualQuantity.Text, System.Globalization.CultureInfo.InvariantCulture);
                    string result = await MvpHelper.MvpService.EditContributionModel(ViewModel.MyContribution, LogOnViewModel.StoredToken);
                    if (result == CommonConstants.OkResult)
					{
						Logger.Log("Activity-Edit");
						MyProfileViewModel.Instance.List = new ObservableRangeCollection<ContributionModel>(MyProfileViewModel.Instance.List);
                    }
                    else
                    {
                        var currentContribution = MyProfileViewModel.Instance.List.Where(item => item.ContributionId == ViewModel.MyContribution.ContributionId).FirstOrDefault();
                        int index = MyProfileViewModel.Instance.List.IndexOf(currentContribution);
                        MyProfileViewModel.Instance.List.Remove(currentContribution);
                        MyProfileViewModel.Instance.List.Insert(index, ViewModel.MyContributionBackup);
						throw new Exception(MyProfileViewModel.Instance.ErrorMessage);
					}
                }

                ViewModel.MyContribution = null;

#if DEBUG
				await Task.Delay(3000);
#endif

				progress?.Hide();

				await UserDialogs.Instance.AlertAsync(TranslateServices.GetResourceString(CommonConstants.DialogDescriptionForSaved), TranslateServices.GetResourceString(CommonConstants.DialogTitleForSaved), TranslateServices.GetResourceString(CommonConstants.DialogOK));

                await Navigation.PopModalAsync();

            }
            catch (Exception ex)
            {
				progress?.Hide();
				ViewModel.ErrorMessage = ex.Message;
				await UserDialogs.Instance.AlertAsync(string.Format(TranslateServices.GetResourceString(CommonConstants.DialogDescriptionForUnableSave), ex.Message), TranslateServices.GetResourceString(CommonConstants.DialogOK));

			}
			finally
            {
				if(progress?.IsShowing ?? false)
					progress?.Hide();
                IsBusy = false;
            }
        }

        private bool CheckData()
        {
            var isValid = true;

            var title = entryTitle.Text;
            var url = entryURL.Text;
            var annualQuantity = entryAnnualQuantity.Text;
            var secondAnnualQuantity = entrySecondAnnualQuantity.Text;
            var annualReach = entryAnnualReach.Text;

            if (string.IsNullOrEmpty(title))
            {
                ViewModel.ErrorMessageForTitle = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, TranslateServices.GetResourceString(CommonConstants.RequiredFieldMessageText));
                isValid = false;
            }
            if (ViewModel.IsNeededUrl)
            {
                if (string.IsNullOrEmpty(url))
                {
                    ViewModel.ErrorMessageForUrl = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, TranslateServices.GetResourceString(CommonConstants.InvalidUrlMessageText));
                    isValid = false;
                }
                else if (!Regex.IsMatch(url, CommonConstants.UrlPattern))
                {
                    ViewModel.ErrorMessageForUrl = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, TranslateServices.GetResourceString(CommonConstants.InvalidUrlMessageText));
                    isValid = false;
                }
            }
            if (ViewModel.IsNeededAnnualQuantity)
            {
                if (annualQuantity != null)
                {
                    if (!Regex.IsMatch(annualQuantity, CommonConstants.NumberPattern))
                    {
                        ViewModel.ErrorMessageForAnnualQuantity = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, TranslateServices.GetResourceString(CommonConstants.FieldMustbeNumberMessageText));
                        isValid = false;
                    }
                }
                else
                {
                    ViewModel.ErrorMessageForAnnualQuantity = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, TranslateServices.GetResourceString(CommonConstants.RequiredFieldMessageText));
                    isValid = false;
                }
            }
            if (ViewModel.IsNeededSecondAnnualQuantity)
            {
                if (secondAnnualQuantity != null)
                {
                    if (!Regex.IsMatch(secondAnnualQuantity, CommonConstants.NumberPattern))
                    {
                        ViewModel.ErrorMessageForSecondAnnualQuantity = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, TranslateServices.GetResourceString(CommonConstants.FieldMustbeNumberMessageText));
                        isValid = false;
                    }
                }
                else
                {
                    ViewModel.ErrorMessageForSecondAnnualQuantity = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, TranslateServices.GetResourceString(CommonConstants.RequiredFieldMessageText));
                    isValid = false;
                }
            }
            if (annualReach != null)
            {
                if (!Regex.IsMatch(annualReach, CommonConstants.NumberPattern))
                {
                    ViewModel.ErrorMessageForAnnualReach = TranslateServices.GetResourceString(CommonConstants.FieldMustbeNumberMessageText);
                    isValid = false;
                }
            }

            return isValid;
        }
        #endregion

        #region Public Methods

        public async void OnCloseClicked(object sender, EventArgs e)
        {
            if (ViewModel.MyContribution != null)
            {
                var currentContribution = MyProfileViewModel.Instance.List.Where(item => item.ContributionId == ViewModel.MyContribution.ContributionId).FirstOrDefault();
                int index = MyProfileViewModel.Instance.List.IndexOf(currentContribution);
                MyProfileViewModel.Instance.List.Remove(currentContribution);
                MyProfileViewModel.Instance.List.Insert(index, ViewModel.MyContributionBackup);
                //MyProfileViewModel.Instance.List[index] = viewModel.MyContributionBackup;
            }

            ViewModel.MyContribution = null;
            await Navigation.PopModalAsync();
        }

        public void OnContributionTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (contributionTypeSelector.SelectedIndex != -1)
            {

                //Method call every time when picker selection changed.
                var selectedValue = contributionTypeSelector.Items[contributionTypeSelector.SelectedIndex];
                ViewModel.ErrorMessageForTitle = CommonConstants.HighlightMessageText;
                ViewModel.ErrorMessageForUrl = CommonConstants.HighlightMessageText;
                ViewModel.ErrorMessageForAnnualQuantity = CommonConstants.HighlightMessageText;
                ViewModel.ErrorMessageForSecondAnnualQuantity = CommonConstants.HighlightMessageText;
                ViewModel.ErrorMessageForAnnualReach = "";

                switch (selectedValue)
                {
                    case CommonConstants.AT:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText =TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForArticle);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForArticle);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Bsp:
                        ViewModel.IsSecondAnnualQuantityVisible = true;
                        ViewModel.AnnualQuantityTipText =TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForPosts);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForPosts);
                        ViewModel.SecondAnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.SecondAnnualQuantityTipTextForPosts);
                        ViewModel.IsNeededUrl = true;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.BA:
                    case CommonConstants.Bca:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText =TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForBook);
                        ViewModel.AnnualReachTipText =TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForBook);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.CS:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText =TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForSamples);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForSamples);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = true;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Cpt:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForProject);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForProject);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = true;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Cbp:
                    case CommonConstants.CO:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForConference);
                        ViewModel.AnnualReachTipText =TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForConference);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.FM:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForForumModerator);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForForumModerator);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.FP:
                        ViewModel.IsSecondAnnualQuantityVisible = true;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForForumParticipation);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForForumParticipation);
                        ViewModel.SecondAnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.SecondAnnualQuantityTipTextForumParticipation);
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = false;
                        ViewModel.IsNeededSecondAnnualQuantity = true;
                        break;
                    case CommonConstants.Fpmf:
                        ViewModel.IsSecondAnnualQuantityVisible = true;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForForumParticipation);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForForumParticipation);
                        ViewModel.SecondAnnualQuantityTipText =TranslateServices.GetResourceString(CommonConstants.SecondAnnualQuantityTipTextForumParticipation);
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.MS:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForMentorship);
                        ViewModel.AnnualReachTipText =TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForMentorship);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Osp:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForOpenSource);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForOpenSource);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.OT:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForOther);
                        ViewModel.AnnualReachTipText =TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForOther);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Pcf:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForFeedback);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForFeedback);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Pgf:
                    case CommonConstants.Pgfg:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
						ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForFeedback);
						ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForFeedback);
						ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Pgi:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
						ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForFeedback);
						ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForFeedback);
						ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Ptdp:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
						ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForFeedback);
						ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForFeedback);
						ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.SO:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForSiteOwner);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForSiteOwner);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.SC:
                    case CommonConstants.SL:
                    case CommonConstants.Sug:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForSpeaking);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForSpeaking);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Tsm:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForSocialMedia);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForSocialMedia);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = true;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Trfe:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForTranslationReviewFeedbackEditing);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForTranslationReviewFeedbackEditing);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Ugo:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForUserGroupOwner);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForUserGroupOwner);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.VD:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForVideo);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForVideo);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = true;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.WB:
                        ViewModel.IsSecondAnnualQuantityVisible = false;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForVideo);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForVideo);
                        ViewModel.SecondAnnualQuantityTipText = string.Empty;
                        ViewModel.IsNeededUrl = false;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.WP:
                        ViewModel.IsSecondAnnualQuantityVisible = true;
                        ViewModel.AnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.AnnualQuantityTipTextForWebsitePosts);
                        ViewModel.AnnualReachTipText = TranslateServices.GetResourceString(CommonConstants.AnnualReachTipTextForWebsitePosts);
                        ViewModel.SecondAnnualQuantityTipText = TranslateServices.GetResourceString(CommonConstants.SecondAnnualQuantityTipTextForWebsitePosts);
                        ViewModel.IsNeededUrl = true;
                        ViewModel.IsNeededAnnualQuantity = true;
                        ViewModel.IsNeededSecondAnnualQuantity = false;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

    }
}
