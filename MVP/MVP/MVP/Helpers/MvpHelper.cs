using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.Models;
using Microsoft.Mvp.ViewModels;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Microsoft.Mvp.Helpers
{
    public class MvpHelper
    {
        private MvpHelper()
        {

        }

        public static IMvpService MvpService => DependencyService.Get<IMvpService>();

        public static void RemoveProperties()
        {
			Settings.SetSetting(CommonConstants.TokenKey, string.Empty);
			Settings.SetSetting(CommonConstants.AccessTokenKey, string.Empty);
			Settings.SetSetting(CommonConstants.RefreshTokenKey, string.Empty);
			Settings.SetSetting(CommonConstants.AuthCodeKey, string.Empty);
        }

        public static void SetContributionInfoToProfileViewModel(ContributionInfo profile)
        {

            if (profile != null && profile.Contributions != null)
            {
                MyProfileViewModel.Instance.TotalOfData = profile.TotalContributions;
                MyProfileViewModel.Instance.List = new ObservableRangeCollection<ContributionModel>();

                var contributions = profile.Contributions.Select(c =>
                {
                    SetLabelTextOfContribution(c);
                    return c;
                });

                MyProfileViewModel.Instance.List.AddRange(contributions);
            }
        }

        public static void SetLabelTextOfContribution(ContributionModel contribution)
        {
            string icon = string.Empty;
            string labelText = string.Empty;

            if (contribution.ContributionType == null)
            {
                labelText = "Number of Views";
            }
            else
            {
                switch (contribution.ContributionType.Name)
                {
                    case CommonConstants.AT:
                        labelText = "Number of Views";
                        break;
                    case CommonConstants.Bsp:
                        labelText = "Annual Unique Visitors";
                        break;
                    case CommonConstants.BA:
                    case CommonConstants.Bca:
                        labelText = "Copies sold";
                        break;
                    case CommonConstants.CS:
                        labelText = "Number of Downloads";
                        break;
                    case CommonConstants.Cpt:
                        labelText = "Number of Downloads";
                        break;
                    case CommonConstants.Cbp:
                    case CommonConstants.CO:
                        labelText = "Number of Visitors";
                        break;
                    case CommonConstants.FM:
                        labelText = "Annual reach";
                        break;
                    case CommonConstants.FP:
                        labelText = "View of answers";
                        break;
                    case CommonConstants.Fpmf:
                        labelText = "View of answers";
                        break;
                    case CommonConstants.MS:
                        labelText = "Annual reach";
                        break;
                    case CommonConstants.Osp:
                        labelText = "Commits";
                        break;
                    case CommonConstants.OT:
                        labelText = "Annual reach";
                        break;
                    case CommonConstants.Pcf:
                        labelText = "Number of Feedbacks provided";
                        break;
                    case CommonConstants.Pgf:
                    case CommonConstants.Pgfg:
                        labelText = "Number of Feedbacks provided";
                        break;
                    case CommonConstants.Pgi:
                        labelText = "Number of Feedbacks provided";
                        break;
                    case CommonConstants.Ptdp:
                        labelText = "Number of Feedbacks provided";
                        break;
                    case CommonConstants.SO:
                        labelText = "Visitors";
                        break;
                    case CommonConstants.SC:
                    case CommonConstants.SL:
                    case CommonConstants.Sug:
                        labelText = "Attendees of talks";
                        break;
                    case CommonConstants.Tsm:
                        labelText = "Number of Followers";
                        break;
                    case CommonConstants.Trfe:
                        labelText = "Annual reach";
                        break;
                    case CommonConstants.Ugo:
                        labelText = "Members";
                        break;
                    case CommonConstants.VD:
                        labelText = "Number of Views";
                        break;
                    case CommonConstants.WB:
                        labelText = "Number of Views";
                        break;
                    case CommonConstants.WP:
                        labelText = "Annual Unique Visitors";
                        break;
                    default:
                        labelText = "Number of Views";
                        break;
                }
            }

            contribution.LabelTextOfContribution = labelText;
        }
    }
}
