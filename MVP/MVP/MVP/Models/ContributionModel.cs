using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Microsoft.Mvp.Models
{
    public class ContributionModel
    {
        public string ContributionId { get; set; }

        public ContributionTypeModel ContributionType { get; set; }

        public ContributionTechnologyModel ContributionTechnology { get; set; }

        public DateTime? StartDate { get; set; }

        public string Title { get; set; }

        public bool ContributionEnableEditDelete
        {
            get
            {
                if (ContributionType == null)
                {
                    return true;
                }
                else
                {
                    return ContributionType.Name != "Product Group Interaction (PGI)";
                }
            }
        }

        public string LabelTextOfContribution { get; set; }

        public string ReferenceUrl { get; set; }

        [DataMember]
        public VisibilityModel Visibility { get; set; }

        public int? AnnualQuantity { get; set; }

        public int? SecondAnnualQuantity { get; set; }

        public int? AnnualReach { get; set; }

        public string Description { get; set; }

        public string AllAnswersUrl { get; set; }

        public string AllPostsUrl { get; set; }

        public bool IsSystemCollected { get; set; }

        public bool IsBelongToLatestAwardCycle { get; set; }

        public string TitleDisplay
        {
            get
            {
                if (!string.IsNullOrEmpty(Title) && Title.Length > 40)
                {
                    return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}", Title.Substring(0, 40), Title.Length > 40 ? "..." : "");
                }
                else
                {
                    return Title;
                }
            }
        }

        public string DataFormat
        {
            get
            {
                if (StartDate != null)
                {
                    return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:yyyy/MM/dd}", StartDate.Value);
                }
                else
                {
                    return "";
                }

            }
        }
    }
}
