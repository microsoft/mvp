using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.Models;
using Microsoft.Mvp.Resources;
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

		public static void SetLabelTextOfContribution(ContributionModel contribution)
		{
			string icon = string.Empty;
			string labelText = string.Empty;

			if (contribution.ContributionType == null)
			{
				labelText = CommonConstants.AnnualReachTipTextDefault;
			}
			else
			{
				switch (contribution.ContributionType.Name)
				{
					case CommonConstants.AT:
						labelText = CommonConstants.AnnualReachTipTextForArticle;
						break;
					case CommonConstants.Bsp:
						labelText = CommonConstants.AnnualReachTipTextForPosts;
						break;
					case CommonConstants.BA:
					case CommonConstants.Bca:
						labelText = CommonConstants.AnnualReachTipTextForBook;
						break;
					case CommonConstants.CS:
						labelText = CommonConstants.AnnualReachTipTextForSamples;
						break;
					case CommonConstants.Cpt:
						labelText = CommonConstants.AnnualReachTipTextForProject;
						break;
					case CommonConstants.Cbp:
					case CommonConstants.CO:
						labelText = CommonConstants.AnnualReachTipTextForConference;
						break;
					case CommonConstants.FM:
						labelText = CommonConstants.AnnualReachTipTextForForumModerator;
						break;
					case CommonConstants.FP:
						labelText = CommonConstants.AnnualReachTipTextForForumParticipation;
						break;
					case CommonConstants.Fpmf:
						labelText = CommonConstants.AnnualReachTipTextForForumParticipation;
						break;
					case CommonConstants.MS:
						labelText = CommonConstants.AnnualReachTipTextForMentorship;
						break;
					case CommonConstants.Osp:
						labelText = CommonConstants.AnnualReachTipTextForOpenSource;
						break;
					case CommonConstants.OT:
						labelText = CommonConstants.AnnualReachTipTextForOther;
						break;
					case CommonConstants.Pcf:
						labelText = CommonConstants.AnnualReachTipTextForFeedback;
						break;
					case CommonConstants.Pgf:
					case CommonConstants.Pgfg:
						labelText = CommonConstants.AnnualReachTipTextForFeedback;
						break;
					case CommonConstants.Pgi:
						labelText = CommonConstants.AnnualReachTipTextForFeedback;
						break;
					case CommonConstants.Ptdp:
						labelText = CommonConstants.AnnualReachTipTextForFeedback;
						break;
					case CommonConstants.SO:
						labelText = CommonConstants.AnnualReachTipTextForSiteOwner;
						break;
					case CommonConstants.SC:
					case CommonConstants.SL:
					case CommonConstants.Sug:
						labelText = CommonConstants.AnnualReachTipTextForSpeaking;
						break;
					case CommonConstants.Tsm:
						labelText = CommonConstants.AnnualReachTipTextForSocialMedia;
						break;
					case CommonConstants.Trfe:
						labelText = CommonConstants.AnnualReachTipTextForTranslationReviewFeedbackEditing;
						break;
					case CommonConstants.Ugo:
						labelText = CommonConstants.AnnualReachTipTextForUserGroupOwner;
						break;
					case CommonConstants.VD:
						labelText = CommonConstants.AnnualReachTipTextForVideo;
						break;
					case CommonConstants.WB:
						labelText = CommonConstants.AnnualReachTipTextForVideo;
						break;
					case CommonConstants.WP:
						labelText = CommonConstants.AnnualReachTipTextForWebsitePosts;
						break;
					default:
						labelText = CommonConstants.AnnualReachTipTextDefault;
						break;
				}
			}

			contribution.LabelTextOfContribution = labelText;
		}
	}
}
