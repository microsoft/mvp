using Microsoft.Mvp.Models;
using Microsoft.Mvp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Microsoft.Mvpui.Helpers
{
    public class MvpHelper
    {
        private MvpHelper()
        {

        }

        public static void RemoveProperties()
        {
            Application.Current.Properties.Remove(CommonConstants.TokenKey);
            Application.Current.Properties.Remove(CommonConstants.AccessTokenKey);
            Application.Current.Properties.Remove(CommonConstants.RefreshTokenKey);
            Application.Current.Properties.Remove(CommonConstants.AuthCodeKey);
        }

        public static async void SetDataToProfileViewModel()//(ProfileModel profile)
        {
            //ProfileModel profile = null;

            //if (Application.Current.Properties.ContainsKey(CommonConstants.ProfileCacheKey))
            //{
            //    DateTime cachedDate = DateTime.Parse(Application.Current.Properties[CommonConstants.ProfileCacheDateKey].ToString());
            //    DateTime ExpiredDate = cachedDate.AddHours(24);
            //    if (DateTime.Compare(ExpiredDate, DateTime.Now) > 0) //Valid data.
            //    {
            //        string profileString = Application.Current.Properties[CommonConstants.ProfileCacheKey].ToString();
            //        profile = Newtonsoft.Json.JsonConvert.DeserializeObject<ProfileModel>(profileString);
            //    }
            //    else
            //    {
            //        profile = await MvpService.GetProfile(LogOnViewModel.StoredToken);
            //        Application.Current.Properties.Add(CommonConstants.ProfileCacheKey, Newtonsoft.Json.JsonConvert.SerializeObject(profile));
            //        Application.Current.Properties.Add(CommonConstants.ProfileCacheDateKey, DateTime.Now);
            //    }
            //}
            //else
            //{
            //    profile = await MvpService.GetProfile(LogOnViewModel.StoredToken);
            //    Application.Current.Properties.Add(CommonConstants.ProfileCacheKey, Newtonsoft.Json.JsonConvert.SerializeObject(profile));
            //    Application.Current.Properties.Add(CommonConstants.ProfileCacheDateKey, DateTime.Now);
            //}

            //if (profile != null)
            //{
            //    MyProfileViewModel.Instance.FirstAwardValue = profile.FirstAwardYear.ToString(System.Globalization.CultureInfo.CurrentCulture);
            //    MyProfileViewModel.Instance.PersonName = profile.DisplayName;
            //    MyProfileViewModel.Instance.AwardCategoriesValue = profile.AwardCategoryDisplay;
            //    MyProfileViewModel.Instance.Description = profile.Biography;
            //    MyProfileViewModel.Instance.AwardsCountValue = profile.YearsAsMVP.ToString(System.Globalization.CultureInfo.CurrentCulture);
            //}
        }

        public static void SetContributionInfoToProfileViewModel(ContributionInfo profile)
        {

            if (profile != null && profile.Contributions != null)
            {
                MyProfileViewModel.Instance.TotalOfData = profile.TotalContributions;
                MyProfileViewModel.Instance.List = new System.Collections.ObjectModel.ObservableCollection<ContributionModel>();
                foreach (var contribution in profile.Contributions)
                {
                    MvpHelper.SetIconAndLabelTextOfContribution(contribution);
                    MyProfileViewModel.Instance.List.Add(contribution);
                }
            }
        }

        public static void SetIconAndLabelTextOfContribution(ContributionModel contribution)
        {
            string icon = string.Empty;
            string labelText = string.Empty;
            string StrWPResourcePath = (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows) ? CommonConstants.ImageFolderForWP : string.Empty;
            if (contribution.ContributionType == null)
            {
                icon = CommonConstants.DefaultContributionIcon;
                labelText = "Number of Views";
            }
            else
            {
                switch (contribution.ContributionType.Name)
                {
                    case CommonConstants.AT:
                        icon = CommonConstants.BlogIcon;
                        labelText = "Number of Views";
                        break;
                    case CommonConstants.Bsp:
                        icon = CommonConstants.BlogIcon;
                        labelText = "Annual Unique Visitors";
                        break;
                    case CommonConstants.BA:
                    case CommonConstants.Bca:
                        icon = CommonConstants.BookIcon;
                        labelText = "Copies sold";
                        break;
                    case CommonConstants.CS:
                        icon = CommonConstants.CodeIcon;
                        labelText = "Number of Downloads";
                        break;
                    case CommonConstants.Cpt:
                        icon = CommonConstants.CodeIcon;
                        labelText = "Number of Downloads";
                        break;
                    case CommonConstants.Cbp:
                    case CommonConstants.CO:
                        icon = CommonConstants.SpeakerIcon;
                        labelText = "Number of Visitors";
                        break;
                    case CommonConstants.FM:
                        icon = CommonConstants.DiscussionIcon;
                        labelText = "Annual reach";
                        break;
                    case CommonConstants.FP:
                        icon = CommonConstants.DiscussionIcon;
                        labelText = "View of answers";
                        break;
                    case CommonConstants.Fpmf:
                        icon = CommonConstants.DiscussionIcon;
                        labelText = "View of answers";
                        break;
                    case CommonConstants.MS:
                        icon = CommonConstants.DefaultContributionIcon;
                        labelText = "Annual reach";
                        break;
                    case CommonConstants.Osp:
                        icon = CommonConstants.CodeIcon;
                        labelText = "Commits";
                        break;
                    case CommonConstants.OT:
                        icon = CommonConstants.DefaultContributionIcon;
                        labelText = "Annual reach";
                        break;
                    case CommonConstants.Pcf:
                        icon = CommonConstants.FeedbackIcon;
                        labelText = "Number of Feedbacks provided";
                        break;
                    case CommonConstants.Pgf:
                    case CommonConstants.Pgfg:
                        icon = CommonConstants.FeedbackIcon;
                        labelText = "Number of Feedbacks provided";
                        break;
                    case CommonConstants.Pgi:
                        icon = CommonConstants.FeedbackIcon;
                        labelText = "Number of Feedbacks provided";
                        break;
                    case CommonConstants.Ptdp:
                        icon = CommonConstants.FeedbackIcon;
                        labelText = "Number of Feedbacks provided";
                        break;
                    case CommonConstants.SO:
                        icon = CommonConstants.DefaultContributionIcon;
                        labelText = "Visitors";
                        break;
                    case CommonConstants.SC:
                    case CommonConstants.SL:
                    case CommonConstants.Sug:
                        icon = CommonConstants.SpeakerIcon;
                        labelText = "Attendees of talks";
                        break;
                    case CommonConstants.Tsm:
                        icon = CommonConstants.VideoIcon;
                        labelText = "Number of Followers";
                        break;
                    case CommonConstants.Trfe:
                        icon = CommonConstants.DefaultContributionIcon;
                        labelText = "Annual reach";
                        break;
                    case CommonConstants.Ugo:
                        icon = CommonConstants.DefaultContributionIcon;
                        labelText = "Members";
                        break;
                    case CommonConstants.VD:
                        icon = CommonConstants.VideoIcon;
                        labelText = "Number of Views";
                        break;
                    case CommonConstants.WB:
                        icon = CommonConstants.VideoIcon;
                        labelText = "Number of Views";
                        break;
                    case CommonConstants.WP:
                        icon = CommonConstants.VideoIcon;
                        labelText = "Annual Unique Visitors";
                        break;
                    default:
                        icon = CommonConstants.DefaultContributionIcon;
                        labelText = "Number of Views";
                        break;
                }
            }

            contribution.LabelTextOfContribution = labelText;
            contribution.Icon = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", StrWPResourcePath, icon);

        }
    }
}
