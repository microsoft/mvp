using Microsoft.Mvp.Models;
using Microsoft.Mvp.ViewModels;
using Microsoft.Mvpui.Helpers;
//using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
//using Xamarin.Forms.Maps;

namespace Microsoft.Mvpui
{
    public partial class ContributionDetail : ContentPage
    {

        #region Private Fields

        private bool _isTapped = false;
        //Geocoder geoCoder;

        #endregion

        #region Constructor

        public ContributionDetail()
        {
            InitializeComponent();
            //geoCoder = new Geocoder();
            this.BindingContext = ContributionViewModel.Instance;
            SetImageSource();
            if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS)
            {
                PersonGroupSelector.HeightRequest = 40;
                entrySecondAnnualQuantity.HeightRequest = 40;
                entryAnnualReach.HeightRequest = 40;
                entryAnnualQuantity.HeightRequest = 40;

                if (Device.OS == TargetPlatform.iOS)
                {
                    locationContainer.HeightRequest = 140;
                }
            }
        }
        #endregion

        #region Private and Protected Methods

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushAsync(new MyProfile());
            return true;
        }
        protected async override void OnAppearing()
        {
            stkOveryLay.IsVisible = true;
            ContributionViewModel.Instance.ErrorMessage = "";
            base.OnAppearing();
            try
            {
                BindingGestureRecognizers();
                InitContributionType();
                await BindContributionAreas();
                BindingSelectors();
            }
            finally
            {
                stkOveryLay.IsVisible = false;
            }
        }

        private async void InitContributionType()
        {
            await BindContributionType();
        }


        private async Task BindContributionAreas()
        {
            if (ContributionViewModel.Instance.ContributionAreas == null || ContributionViewModel.Instance.ContributionAreas.Count == 0)
            {
                await MyProfileViewModel.Instance.BindingAreas();
            }
        }

        private async Task BindContributionType()
        {
            if (ContributionViewModel.Instance.ContributionTypeNames == null || ContributionViewModel.Instance.ContributionTypeNames.Count == 0)
            {
                await MyProfileViewModel.Instance.BindingContributionType();
            }

            BindingContributionType();
        }

        private void BindingGestureRecognizers()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnCloseClicked;
            btnCancel.GestureRecognizers.Add(tapGestureRecognizer);

            var tapGestureRecognizerForSave = new TapGestureRecognizer();
            tapGestureRecognizerForSave.Tapped += OnSaveClicked;
            imgSave.GestureRecognizers.Add(tapGestureRecognizerForSave);
        }

        private void BindingContributionType()
        {
            contributionTypeSelector.Items.Clear();

            foreach (var item in ContributionViewModel.Instance.ContributionTypeNames)
            {
                contributionTypeSelector.Items.Add(item.Name);
            }

            if (ContributionViewModel.Instance.MyContribution != null)
            {
                if (ContributionViewModel.Instance.ContributionTypeNames.Count > 0)
                {
                    var activeContributionType = ContributionViewModel.Instance.ContributionTypeNames.Where(item => item.Id == ContributionViewModel.Instance.MyContribution.ContributionType.Id).FirstOrDefault();
                    ContributionViewModel.Instance.ContributionTypeIndex = ContributionViewModel.Instance.ContributionTypeNames.IndexOf(activeContributionType);
                }
            }

            if ((BindingContext as ContributionViewModel).MyContribution == null)
            {
                contributionTypeSelector.SelectedIndex = 0;
            }
            else
            {
                contributionTypeSelector.SelectedIndex = ContributionViewModel.Instance.ContributionTypeIndex;
            }
        }

        private void BindingSelectors()
        {

            ContributionAreaSelector.Items.Clear();
            PersonGroupSelector.Items.Clear();

            foreach (var item in ContributionViewModel.Instance.ContributionAreas)
            {
                ContributionAreaSelector.Items.Add(item.Name);
            }
            foreach (var item in ContributionViewModel.Instance.PersonGroups)
            {
                PersonGroupSelector.Items.Add(item.Description);
            }

            if (ContributionViewModel.Instance.MyContribution != null)
            {

                if (ContributionViewModel.Instance.ContributionAreas.Count > 0)
                {
                    var activeContributionArea = ContributionViewModel.Instance.ContributionAreas.Where(item => item.Id == ContributionViewModel.Instance.MyContribution.ContributionTechnology.Id).FirstOrDefault();
                    ContributionViewModel.Instance.ContributionAreaIndex = ContributionViewModel.Instance.ContributionAreas.IndexOf(activeContributionArea);
                }

                if (ContributionViewModel.Instance.PersonGroups.Count > 0)
                {
                    var activeVisibility = ContributionViewModel.Instance.PersonGroups.Where(item => item.Id == ContributionViewModel.Instance.MyContribution.Visibility.Id).FirstOrDefault();
                    ContributionViewModel.Instance.VibilityIndex = ContributionViewModel.Instance.PersonGroups.IndexOf(activeVisibility);
                }
            }

            if ((BindingContext as ContributionViewModel).MyContribution == null)
            {

                //ContributionAreaSelector.SelectedIndex = 0;
                var activeContributionType = ContributionViewModel.Instance.ContributionAreas.Where(item => string.Compare(item.AwardName, MyProfileViewModel.Instance.AwardCategoriesValue, StringComparison.CurrentCultureIgnoreCase) == 0).FirstOrDefault();
                ContributionAreaSelector.SelectedIndex = ContributionViewModel.Instance.ContributionAreas.IndexOf(activeContributionType);
            }
            else
            {
                ContributionAreaSelector.SelectedIndex = ContributionViewModel.Instance.ContributionAreaIndex;
            }

            PersonGroupSelector.SelectedIndex = ContributionViewModel.Instance.VibilityIndex;
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
                backup.Icon = contributionInfo.MyContribution.Icon;
                backup.IsBelongToLatestAwardCycle = contributionInfo.MyContribution.IsBelongToLatestAwardCycle;
                backup.IsSystemCollected = contributionInfo.MyContribution.IsSystemCollected;
                backup.LabelTextOfContribution = contributionInfo.MyContribution.LabelTextOfContribution;
                backup.ReferenceUrl = contributionInfo.MyContribution.ReferenceUrl;
                backup.Title = contributionInfo.MyContribution.Title;
                contributionInfo.MyContributionBackup = backup;
            }
        }

        private void SetImageSource()
        {

            string StrWPResourcePath = (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows) ? CommonConstants.ImageFolderForWP : string.Empty;
            btnCancel.Source = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", StrWPResourcePath, "Cancel.png");
            imgSave.Source = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", StrWPResourcePath, "Right.png");
        }

        public async void OnSaveClicked(object sender, EventArgs e)
        {

            if (_isTapped == true)
            {
                return;
            }
            var imageButton = sender as Image;
            try
            {

                _isTapped = true;

                if (imageButton != null)
                {
                    imageButton.Opacity = 0.5;
                    await imageButton.FadeTo(1);
                }

                bool isValid = CheckData();
                if (!isValid)
                {
                    return;
                }
                stkOveryLay.IsVisible = true;
                progressText.Text = "Saving ...";
                if (ContributionViewModel.Instance.MyContribution == null)
                {
                    var model = new ContributionModel()
                    {
                        ContributionId = "0",
                        ContributionType = ContributionViewModel.Instance.ContributionTypeNames[contributionTypeSelector.SelectedIndex],
                        ContributionTechnology = ContributionViewModel.Instance.ContributionAreas[ContributionAreaSelector.SelectedIndex],
                        Visibility = ContributionViewModel.Instance.PersonGroups[PersonGroupSelector.SelectedIndex],
                        StartDate = ContributionDateSelector.Date.ToUniversalTime(),
                        Title = entryTitle.Text,
                        ReferenceUrl = entryURL.Text,
                        Description = entryDescription.Text,
                        AnnualQuantity = Convert.ToInt32(entryAnnualQuantity.Text, System.Globalization.CultureInfo.InvariantCulture),
                        AnnualReach = Convert.ToInt32(entryAnnualReach.Text, System.Globalization.CultureInfo.InvariantCulture),
                        SecondAnnualQuantity = Convert.ToInt32(entrySecondAnnualQuantity.Text, System.Globalization.CultureInfo.InvariantCulture)
                    };
                    var result = await MvpService.AddContributionModel(model, LogOnViewModel.StoredToken);
                    if (result != null && result.ContributionId != "0")
                    {
                        Helpers.MvpHelper.SetIconAndLabelTextOfContribution(result);
                        MyProfileViewModel.Instance.List.Insert(0, result);
                        MyProfileViewModel.Instance.TotalOfData += 1;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {

                    ContributionViewModel.Instance.MyContribution.ContributionType = ContributionViewModel.Instance.ContributionTypeNames[contributionTypeSelector.SelectedIndex];
                    ContributionViewModel.Instance.MyContribution.ContributionTechnology = ContributionViewModel.Instance.ContributionAreas[ContributionAreaSelector.SelectedIndex];
                    ContributionViewModel.Instance.MyContribution.Visibility = ContributionViewModel.Instance.PersonGroups[PersonGroupSelector.SelectedIndex];
                    ContributionViewModel.Instance.MyContribution.StartDate = ContributionDateSelector.Date.ToUniversalTime();
                    ContributionViewModel.Instance.MyContribution.AnnualQuantity = Convert.ToInt32(entryAnnualQuantity.Text, System.Globalization.CultureInfo.InvariantCulture);
                    ContributionViewModel.Instance.MyContribution.AnnualReach = Convert.ToInt32(entryAnnualReach.Text, System.Globalization.CultureInfo.InvariantCulture);
                    ContributionViewModel.Instance.MyContribution.SecondAnnualQuantity = Convert.ToInt32(entrySecondAnnualQuantity.Text, System.Globalization.CultureInfo.InvariantCulture);
                    string result = await MvpService.EditContributionModel(ContributionViewModel.Instance.MyContribution, LogOnViewModel.StoredToken);
                    if (result == CommonConstants.OkResult)
                    {
                        MyProfileViewModel.Instance.List = new ObservableCollection<ContributionModel>(MyProfileViewModel.Instance.List);
                    }
                    else
                    {
                        var currentContribution = MyProfileViewModel.Instance.List.Where(item => item.ContributionId == ContributionViewModel.Instance.MyContribution.ContributionId).FirstOrDefault();
                        int index = MyProfileViewModel.Instance.List.IndexOf(currentContribution);
                        MyProfileViewModel.Instance.List.Remove(currentContribution);
                        MyProfileViewModel.Instance.List.Insert(index, ContributionViewModel.Instance.MyContributionBackup);
                        return;
                    }
                }

                ContributionViewModel.Instance.MyContribution = null;

                await Navigation.PopModalAsync();

            }
            catch (WebException ex)
            {
                ContributionViewModel.Instance.ErrorMessage = ex.Message;
            }
            catch (HttpRequestException ex)
            {
                ContributionViewModel.Instance.ErrorMessage = ex.Message;
            }
            finally
            {
                if (imageButton != null)
                {
                    _isTapped = false;
                }
                stkOveryLay.IsVisible = false;
                progressText.Text = "Loading ...";
            }
        }

        /// <summary>
        /// Form Validation
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            bool isValid = true;

            string title = entryTitle.Text;
            string url = entryURL.Text;
            string annualQuantity = entryAnnualQuantity.Text;
            string secondAnnualQuantity = entrySecondAnnualQuantity.Text;
            string annualReach = entryAnnualReach.Text;

            if (string.IsNullOrEmpty(title))
            {
                ContributionViewModel.Instance.ErrorMessageForTitle = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.RequiredFieldMessageText);
                isValid = false;
            }
            if (ContributionViewModel.Instance.IsNeededUrl)
            {
                if (string.IsNullOrEmpty(url))
                {
                    ContributionViewModel.Instance.ErrorMessageForUrl = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.InvalidUrlMessageText);
                    isValid = false;
                }
                else if (!Regex.IsMatch(url, CommonConstants.UrlPattern))
                {
                    ContributionViewModel.Instance.ErrorMessageForUrl = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.InvalidUrlMessageText);
                    isValid = false;
                }
            }
            if (ContributionViewModel.Instance.IsNeededAnnualQuantity)
            {
                if (annualQuantity != null)
                {
                    if (!Regex.IsMatch(annualQuantity, CommonConstants.NumberPattern))
                    {
                        ContributionViewModel.Instance.ErrorMessageForAnnualQuantity = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.FieldMustbeNumberMessageText);
                        isValid = false;
                    }
                }
                else
                {
                    ContributionViewModel.Instance.ErrorMessageForAnnualQuantity = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.RequiredFieldMessageText);
                    isValid = false;
                }
            }
            if (ContributionViewModel.Instance.IsNeededSecondAnnualQuantity)
            {
                if (secondAnnualQuantity != null)
                {
                    if (!Regex.IsMatch(secondAnnualQuantity, CommonConstants.NumberPattern))
                    {
                        ContributionViewModel.Instance.ErrorMessageForSecondAnnualQuantity = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.FieldMustbeNumberMessageText);
                        isValid = false;
                    }
                }
                else
                {
                    ContributionViewModel.Instance.ErrorMessageForSecondAnnualQuantity = string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.RequiredFieldMessageText);
                    isValid = false;
                }
            }
            if (annualReach != null)
            {
                if (!Regex.IsMatch(annualReach, CommonConstants.NumberPattern))
                {
                    ContributionViewModel.Instance.ErrorMessageForAnnualReach = CommonConstants.FieldMustbeNumberMessageText;
                    isValid = false;
                }
            }

            return isValid;
        }
        #endregion

        #region Public Methods

        public async void OnCloseClicked(object sender, EventArgs e)
        {
            if (ContributionViewModel.Instance.MyContribution != null)
            {
                var currentContribution = MyProfileViewModel.Instance.List.Where(item => item.ContributionId == ContributionViewModel.Instance.MyContribution.ContributionId).FirstOrDefault();
                int index = MyProfileViewModel.Instance.List.IndexOf(currentContribution);
                MyProfileViewModel.Instance.List.Remove(currentContribution);
                MyProfileViewModel.Instance.List.Insert(index, ContributionViewModel.Instance.MyContributionBackup);
                //MyProfileViewModel.Instance.List[index] = ContributionViewModel.Instance.MyContributionBackup;
            }

            ContributionViewModel.Instance.MyContribution = null;
            await Navigation.PopModalAsync();
        }

        public void OnContributionTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (contributionTypeSelector.SelectedIndex != -1)
            {

                //Method call every time when picker selection changed.
                var selectedValue = contributionTypeSelector.Items[contributionTypeSelector.SelectedIndex];
                ContributionViewModel.Instance.ErrorMessageForTitle = CommonConstants.HighlightMessageText;
                ContributionViewModel.Instance.ErrorMessageForUrl = CommonConstants.HighlightMessageText;
                ContributionViewModel.Instance.ErrorMessageForAnnualQuantity = CommonConstants.HighlightMessageText;
                ContributionViewModel.Instance.ErrorMessageForSecondAnnualQuantity = CommonConstants.HighlightMessageText;
                ContributionViewModel.Instance.ErrorMessageForAnnualReach = "";

                switch (selectedValue)
                {
                    case CommonConstants.AT:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Articles";
                        ContributionViewModel.Instance.AnnualReachTipText = "Number of Views";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Bsp:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = true;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Posts";
                        ContributionViewModel.Instance.AnnualReachTipText = "Annual Unique Visitors";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = "Number of Subscribers";
                        ContributionViewModel.Instance.IsNeededUrl = true;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.BA:
                    case CommonConstants.Bca:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Books";
                        ContributionViewModel.Instance.AnnualReachTipText = "Copies sold";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.CS:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Samples";
                        ContributionViewModel.Instance.AnnualReachTipText = "Number of Downloads";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = true;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Cpt:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Projects";
                        ContributionViewModel.Instance.AnnualReachTipText = "Number of Downloads";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = true;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Cbp:
                    case CommonConstants.CO:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Conferences";
                        ContributionViewModel.Instance.AnnualReachTipText = "Number of Visitors";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.FM:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Threads moderated";
                        ContributionViewModel.Instance.AnnualReachTipText = "Annual reach";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.FP:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = true;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Answers";
                        ContributionViewModel.Instance.AnnualReachTipText = "View of answers";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = "Number of Posts";
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = false;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = true;
                        break;
                    case CommonConstants.Fpmf:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = true;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Answers";
                        ContributionViewModel.Instance.AnnualReachTipText = "View of answers";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = "Number of Posts";
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.MS:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Mentees";
                        ContributionViewModel.Instance.AnnualReachTipText = "Annual reach";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Osp:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Projects";
                        ContributionViewModel.Instance.AnnualReachTipText = "Commits";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.OT:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Annual quantity";
                        ContributionViewModel.Instance.AnnualReachTipText = "Annual reach";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Pcf:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Events provided";
                        ContributionViewModel.Instance.AnnualReachTipText = "Number of Feedbacks provided";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Pgf:
                    case CommonConstants.Pgfg:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Events provided";
                        ContributionViewModel.Instance.AnnualReachTipText = "Number of Feedbacks provided";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Pgi:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Events provided";
                        ContributionViewModel.Instance.AnnualReachTipText = "Number of Feedbacks provided";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Ptdp:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Events provided";
                        ContributionViewModel.Instance.AnnualReachTipText = "Number of Feedbacks provided";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.SO:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Posts";
                        ContributionViewModel.Instance.AnnualReachTipText = "Visitors";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.SC:
                    case CommonConstants.SL:
                    case CommonConstants.Sug:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Talks";
                        ContributionViewModel.Instance.AnnualReachTipText = "Attendees of talks";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Tsm:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Posts";
                        ContributionViewModel.Instance.AnnualReachTipText = "Number of Followers";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = true;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Trfe:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Annual quantity";
                        ContributionViewModel.Instance.AnnualReachTipText = "Annual reach";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.Ugo:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Meetings";
                        ContributionViewModel.Instance.AnnualReachTipText = "Members";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.VD:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Videos";
                        ContributionViewModel.Instance.AnnualReachTipText = "Number of Views";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = true;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.WB:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Videos";
                        ContributionViewModel.Instance.AnnualReachTipText = "Number of Views";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                        ContributionViewModel.Instance.IsNeededUrl = false;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    case CommonConstants.WP:
                        ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = true;
                        ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Posts";
                        ContributionViewModel.Instance.AnnualReachTipText = "Annual Unique Visitors";
                        ContributionViewModel.Instance.SecondAnnualQuantityTipText = "Number of Subscribers";
                        ContributionViewModel.Instance.IsNeededUrl = true;
                        ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                        ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion


    }
}
