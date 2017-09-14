using MVP.Models;
using MVP.ViewModels;
using MVPUI.Helpers;
using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MVPUI
{
    public partial class ContributionDetail : ContentPage
    {

        #region Private Fields

        Geocoder geoCoder;

        #endregion

        #region Constructor

        public ContributionDetail()
        {
            InitializeComponent();

            geoCoder = new Geocoder();
            this.BindingContext = ContributionViewModel.Instance;
            SetImageSource();
            if (Device.OS == TargetPlatform.Android)
            {
                PersonGroupSelector.HeightRequest = 40;
            }
        }
        #endregion

        #region Private and Protected Methods

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await BindData();
            BindingSelectors();
            BindingGestureRecognizers();
            await BindingLocation();
        }

        private async Task<bool> BindData()
        {
            if (ContributionViewModel.Instance.ContributionTypeNames == null || ContributionViewModel.Instance.ContributionTypeNames.Count == 0)
            {
                await MyProfileViewModel.Instance.BindingListData();
            }
            return true;
        }

        private async Task BindingLocation()
        {
            labelLocation.Text = "Getting gps"; try
            {

                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

                if (position == null)
                {
                    labelLocation.Text = "null gps :(";
                    return;
                }

                labelLocation.Text = string.Format("Lat: {0} \nLong: {1}", position.Latitude, position.Longitude);

                var position1 = new Position(position.Latitude, position.Longitude);
                var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position1);
                foreach (var address in possibleAddresses)
                    labelLocation.Text += "\n" + address;
            }
            catch (Exception ex)
            {
                labelLocation.Text = ex.Message;
            }
        }

        private void BindingGestureRecognizers()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnCloseClicked;
            btnCancel.GestureRecognizers.Add(tapGestureRecognizer);
            //labelCancel.GestureRecognizers.Add(tapGestureRecognizer);

            var tapGestureRecognizerForSave = new TapGestureRecognizer();
            tapGestureRecognizerForSave.Tapped += OnSaveClicked;
            imgSave.GestureRecognizers.Add(tapGestureRecognizerForSave);
        }

        private void BindingSelectors()
        {
            contributionTypeSelector.Items.Clear();
            ContributionAreaSelector.Items.Clear();
            PersonGroupSelector.Items.Clear();

            foreach (var item in ContributionViewModel.Instance.ContributionTypeNames)
            {
                contributionTypeSelector.Items.Add(item.Name);
            }
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
                if (ContributionViewModel.Instance.ContributionTypeNames.Count > 0)
                {
                    var activeContributionType = ContributionViewModel.Instance.ContributionTypeNames.Where(item => item.Id == ContributionViewModel.Instance.MyContribution.ContributionType.Id).FirstOrDefault();
                    ContributionViewModel.Instance.ContributionTypeIndex = ContributionViewModel.Instance.ContributionTypeNames.IndexOf(activeContributionType);

                }
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
                contributionTypeSelector.SelectedIndex = 0;
                ContributionAreaSelector.SelectedIndex = 0;
            }
            else
            {
                contributionTypeSelector.SelectedIndex = ContributionViewModel.Instance.ContributionTypeIndex;
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
                backup.ContributionTechnology = new ContributionTechnologyModel() { Id = contributionInfo.MyContribution.ContributionTechnology.Id, Name = contributionInfo.MyContribution.ContributionTechnology.Name, AwardName = contributionInfo.MyContribution.ContributionTechnology.AwardName  };
                //backup.AllAnswersUrl = contributionInfo.MyContribution.AllAnswersUrl;
                //backup.AllPostsUrl = contributionInfo.MyContribution.AllPostsUrl;
                backup.AnnualQuantity = contributionInfo.MyContribution.AnnualQuantity;
                backup.SecondAnnualQuantity = contributionInfo.MyContribution.SecondAnnualQuantity;
                backup.AnnualReach = contributionInfo.MyContribution.AnnualReach;
                backup.ContributionId = contributionInfo.MyContribution.ContributionId;
                backup.StartDate = contributionInfo.MyContribution.StartDate;
                backup.Description = contributionInfo.MyContribution.Description;
                backup.Icon = contributionInfo.MyContribution.Icon;
                //backup.IsBelongToLatestAwardCycle = contributionInfo.MyContribution.IsBelongToLatestAwardCycle;
                //backup.IsSystemCollected = contributionInfo.MyContribution.IsSystemCollected;
                backup.LabelTextOfContribution = contributionInfo.MyContribution.LabelTextOfContribution;
                backup.ReferenceUrl = contributionInfo.MyContribution.ReferenceUrl;
                backup.Title = contributionInfo.MyContribution.Title;
                contributionInfo.MyContributionBackup = backup;
            }
        }

        private void SetImageSource()
        {
            string StrWPResourcePath = (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows) ? "Resources/" : string.Empty;
            btnCancel.Source = string.Format("{0}{1}", StrWPResourcePath, "Cancel.png");
            imgSave.Source = string.Format("{0}{1}", StrWPResourcePath, "Right.png");

        }

        public async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                bool isValid = CheckData();
                if (!isValid)
                {
                    return;
                }

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
                        AnnualQuantity = Convert.ToInt32(entryAnnualQuantity.Text),
                        AnnualReach = Convert.ToInt32(entryAnnualReach.Text),
                        SecondAnnualQuantity = Convert.ToInt32(entrySecondAnnualQuantity.Text)
                    };
                    var result = await MvpService.AddContributionModel(model, LoginViewModel.StoredToken);
                    if (result != null && result.ContributionId != "0")
                    {
                        Helpers.MvpHelper.SetIconAndLabelTextOfContribution(result);
                        MyProfileViewModel.Instance.List.Insert(0, result);
                        MyProfileViewModel.Instance.TotalOfData += 1;
                    }
                }
                else
                {

                    ContributionViewModel.Instance.MyContribution.ContributionType = ContributionViewModel.Instance.ContributionTypeNames[contributionTypeSelector.SelectedIndex];
                    ContributionViewModel.Instance.MyContribution.ContributionTechnology = ContributionViewModel.Instance.ContributionAreas[ContributionAreaSelector.SelectedIndex];
                    ContributionViewModel.Instance.MyContribution.Visibility = ContributionViewModel.Instance.PersonGroups[PersonGroupSelector.SelectedIndex];
                    ContributionViewModel.Instance.MyContribution.StartDate = ContributionDateSelector.Date.ToUniversalTime();                   
                    ContributionViewModel.Instance.MyContribution.AnnualQuantity = Convert.ToInt32(entryAnnualQuantity.Text);
                    ContributionViewModel.Instance.MyContribution.AnnualReach = Convert.ToInt32(entryAnnualReach.Text);
                    ContributionViewModel.Instance.MyContribution.SecondAnnualQuantity = Convert.ToInt32(entrySecondAnnualQuantity.Text);
                    string result = await MvpService.EditContributionModel(ContributionViewModel.Instance.MyContribution, LoginViewModel.StoredToken);
                    if (result == CommonConstants.OKResult)
                    {
                        MyProfileViewModel.Instance.List = new ObservableCollection<ContributionModel>(MyProfileViewModel.Instance.List);
                    }
                    else
                    {
                        var m = MyProfileViewModel.Instance.List.Where(item => item.ContributionId == ContributionViewModel.Instance.MyContribution.ContributionId).FirstOrDefault();
                        int index = MyProfileViewModel.Instance.List.IndexOf(m);
                        MyProfileViewModel.Instance.List.Remove(m);
                        MyProfileViewModel.Instance.List.Insert(index, ContributionViewModel.Instance.MyContributionBackup);
                    }
                }

                ContributionViewModel.Instance.MyContribution = null;
                await Navigation.PushModalAsync(new MyProfile());
            }
            catch (Exception ex)
            {
                ContributionViewModel.Instance.ErrorMessage = ex.Message;
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
                ContributionViewModel.Instance.ErrorMessageForTitle = string.Format(" {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.RequiredFieldMessageText);
                isValid = false;
            }
            if (ContributionViewModel.Instance.IsNeededURL)
            {
                if (string.IsNullOrEmpty(url))
                {
                    ContributionViewModel.Instance.ErrorMessageForURL = string.Format(" {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.InValidURLMessageText);
                    isValid = false;
                }
                else if (!Regex.IsMatch(url, CommonConstants.UrlPattern))
                {
                    ContributionViewModel.Instance.ErrorMessageForURL = string.Format(" {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.InValidURLMessageText);
                    isValid = false;
                }
                else
                {
                    ContributionViewModel.Instance.ErrorMessageForURL = string.Format("{0}", CommonConstants.HighlightMessageText);
                }
            }
            if (ContributionViewModel.Instance.IsNeededAnnualQuantity)
            {
                if (annualQuantity != null)
                {
                    if (!Regex.IsMatch(annualQuantity, CommonConstants.NumberPattern))
                    {
                        ContributionViewModel.Instance.ErrorMessageForAnnualQuantity = string.Format(" {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.FieldMustbeNumberMessageText);
                        isValid = false;
                    }
                    else
                    {
                        ContributionViewModel.Instance.ErrorMessageForAnnualQuantity = string.Format("{0}", CommonConstants.HighlightMessageText);
                    }
                }
                else
                {
                    ContributionViewModel.Instance.ErrorMessageForAnnualQuantity = string.Format(" {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.RequiredFieldMessageText);
                }
            }
            if (ContributionViewModel.Instance.IsNeededSecondAnnualQuantity)
            {
                if (secondAnnualQuantity != null)
                {
                    if (!Regex.IsMatch(secondAnnualQuantity, CommonConstants.NumberPattern))
                    {
                        ContributionViewModel.Instance.ErrorMessageForSecondAnnualQuantity = string.Format(" {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.FieldMustbeNumberMessageText);
                        isValid = false;
                    }
                    else
                    {
                        ContributionViewModel.Instance.ErrorMessageForSecondAnnualQuantity = string.Format("{0}", CommonConstants.HighlightMessageText);
                    }
                }
                else
                {
                    ContributionViewModel.Instance.ErrorMessageForSecondAnnualQuantity = string.Format(" {0} {1}", CommonConstants.HighlightMessageText, CommonConstants.RequiredFieldMessageText);
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
                else
                {
                    ContributionViewModel.Instance.ErrorMessageForAnnualReach = string.Format("{0}", CommonConstants.HighlightMessageText);
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
                var m = MyProfileViewModel.Instance.List.Where(item => item.ContributionId == ContributionViewModel.Instance.MyContribution.ContributionId).FirstOrDefault();
                int index = MyProfileViewModel.Instance.List.IndexOf(m);
                MyProfileViewModel.Instance.List.Remove(m);
                MyProfileViewModel.Instance.List.Insert(index, ContributionViewModel.Instance.MyContributionBackup);

            }

            ContributionViewModel.Instance.MyContribution = null;
            await Navigation.PushModalAsync(new MyProfile());

        }

        public void OnContributionTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            //Method call every time when picker selection changed.
            var selectedValue = contributionTypeSelector.Items[contributionTypeSelector.SelectedIndex];
            ContributionViewModel.Instance.ErrorMessageForTitle = CommonConstants.HighlightMessageText;
            ContributionViewModel.Instance.ErrorMessageForURL = CommonConstants.HighlightMessageText;
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
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.BSP:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = true;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Posts";
                    ContributionViewModel.Instance.AnnualReachTipText = "Annual Unique Visitors";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = "Number of Subscribers";
                    ContributionViewModel.Instance.IsNeededURL = true;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.BA:
                case CommonConstants.BCA:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Books";
                    ContributionViewModel.Instance.AnnualReachTipText = "Copies sold";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.CS:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Samples";
                    ContributionViewModel.Instance.AnnualReachTipText = "Number of Downloads";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = true;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.CPT:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Projects";
                    ContributionViewModel.Instance.AnnualReachTipText = "Number of Downloads";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = true;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.CBP:
                case CommonConstants.CO:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Conferences";
                    ContributionViewModel.Instance.AnnualReachTipText = "Number of Visitors";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.FM:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Threads moderated";
                    ContributionViewModel.Instance.AnnualReachTipText = "Annual reach";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.FP:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = true;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Answers";
                    ContributionViewModel.Instance.AnnualReachTipText = "View of answers";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = "Number of Posts";
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = false;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = true;
                    break;
                case CommonConstants.FPMF:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = true;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Answers";
                    ContributionViewModel.Instance.AnnualReachTipText = "View of answers";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = "Number of Posts";
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.MS:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Mentees";
                    ContributionViewModel.Instance.AnnualReachTipText = "Annual reach";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.OSP:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Projects";
                    ContributionViewModel.Instance.AnnualReachTipText = "Commits";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.OT:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Annual quantity";
                    ContributionViewModel.Instance.AnnualReachTipText = "Annual reach";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.PCF:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Events provided";
                    ContributionViewModel.Instance.AnnualReachTipText = "Number of Feedbacks provided";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.PGF:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Events provided";
                    ContributionViewModel.Instance.AnnualReachTipText = "Number of Feedbacks provided";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.PGI:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Events provided";
                    ContributionViewModel.Instance.AnnualReachTipText = "Number of Feedbacks provided";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.PTDP:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Events provided";
                    ContributionViewModel.Instance.AnnualReachTipText = "Number of Feedbacks provided";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.SO:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Posts";
                    ContributionViewModel.Instance.AnnualReachTipText = "Visitors";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.SC:
                case CommonConstants.SL:
                case CommonConstants.SUG:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Talks";
                    ContributionViewModel.Instance.AnnualReachTipText = "Attendees of talks";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.TSM:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Posts";
                    ContributionViewModel.Instance.AnnualReachTipText = "Number of Followers";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = true;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.TRFE:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Annual quantity";
                    ContributionViewModel.Instance.AnnualReachTipText = "Annual reach";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.UGO:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Meetings";
                    ContributionViewModel.Instance.AnnualReachTipText = "Members";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.VD:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Videos";
                    ContributionViewModel.Instance.AnnualReachTipText = "Number of Views";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = true;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.WB:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = false;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Videos";
                    ContributionViewModel.Instance.AnnualReachTipText = "Number of Views";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = string.Empty;
                    ContributionViewModel.Instance.IsNeededURL = false;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                case CommonConstants.WP:
                    ContributionViewModel.Instance.IsSecondAnnualQuantityVisible = true;
                    ContributionViewModel.Instance.AnnualQuantityTipText = "Number of Posts";
                    ContributionViewModel.Instance.AnnualReachTipText = "Annual Unique Visitors";
                    ContributionViewModel.Instance.SecondAnnualQuantityTipText = "Number of Subscribers";
                    ContributionViewModel.Instance.IsNeededURL = true;
                    ContributionViewModel.Instance.IsNeededAnnualQuantity = true;
                    ContributionViewModel.Instance.IsNeededSecondAnnualQuantity = false;
                    break;
                default:
                    break;
            }
        }

        #endregion


    }
}
