using System;
using System.Globalization;
using Microsoft.Mvp.Helpers;
using Microsoft.Mvp.Models;
using Xamarin.Forms;

namespace Microsoft.Mvpui.Converters
{
    public class ContributionToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ContributionModel))
                return string.Empty;

            var contribution = value as ContributionModel;
            var icon = string.Empty;

            if (contribution.ContributionType == null)
            {
                icon = "image_defaultactivity.png";
            }
            else
            {
                switch (contribution.ContributionType.Name)
                {
                    case CommonConstants.AT:
                    case CommonConstants.Bsp:
                        icon = "image_blog.png";
                        break;
                    case CommonConstants.BA:
                    case CommonConstants.Bca:
                        icon = "image_book.png";
                        break;
                    case CommonConstants.CS:
                    case CommonConstants.Cpt:
                    case CommonConstants.Osp:
                        icon = "image_code.png";
                        break;
                    case CommonConstants.Cbp:
                    case CommonConstants.CO:
                        icon = "image_speaker.png";
                        break;
                    case CommonConstants.FM:
                    case CommonConstants.FP:
                    case CommonConstants.Fpmf:
                        icon = "image_discussion.png";
                        break;
                    case CommonConstants.Pcf:
                    case CommonConstants.Pgf:
                    case CommonConstants.Pgfg:
                    case CommonConstants.Pgi:
                    case CommonConstants.Ptdp:
                        icon = "image_feedback.png";
                        break;
                    case CommonConstants.SC:
                    case CommonConstants.SL:
                    case CommonConstants.Sug:
                        icon = "image_speaker.png";
                        break;
                    case CommonConstants.Tsm:
                    case CommonConstants.VD:
                    case CommonConstants.WB:
                    case CommonConstants.WP:
                        icon = "image_video.png";
                        break;
                    case CommonConstants.MS:
                    case CommonConstants.OT:
                    case CommonConstants.SO:
                    case CommonConstants.Trfe:
                    case CommonConstants.Ugo:
                    default:
                        icon = "image_defaultactivity.png";
                        break;
                }
            }

            // For Windows platforms we need an Assets folder.
            if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WinPhone)
                icon = $"Assets\\{icon}";

            return icon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
