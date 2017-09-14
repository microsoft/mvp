using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Microsoft.Mvp.Models
{
    public class ContributionModel
    {
        public string ContributionId { get; set; }

        /// <summary>
        /// Gets or sets the contribution type.
        /// </summary>
        /// 
        public ContributionTypeModel ContributionType { get; set; }

        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the contribution technology.
        /// </summary>
        public ContributionTechnologyModel ContributionTechnology { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime? StartDate { get; set; }

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

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Local copy of TitleOfContribution.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string ReferenceUrl { get; set; }

        /// <summary>
        /// Gets or sets the visibility.
        /// </summary>
        [DataMember]
        public VisibilityModel Visibility { get; set; }

        /// <summary>
        /// Gets or sets the annual quantity.
        /// </summary>
        public int? AnnualQuantity { get; set; }

        /// <summary>
        /// Gets or sets the second annual quantity.
        /// </summary>
        public int? SecondAnnualQuantity { get; set; }

        /// <summary>
        /// Gets or sets the reach score.
        /// </summary>
        public int? AnnualReach { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Online identity by which this contribution was collected. Should be null if the contribution was not system collected
        /// </summary>
        //public OnlineIdentityViewModel OnlineIdentity { get; set; }

        /// <summary>
        /// Social network. Should be null or ignore for non system collected contribution
        /// </summary>
        //public SocialNetworkViewModel SocialNetwork { get; set; }

        /// <summary>
        /// AllAnswersUrl
        /// </summary>
        public string AllAnswersUrl { get; set; }

        /// <summary>
        /// AllAnswersUrl
        /// </summary>
        public string AllPostsUrl { get; set; }

        /// <summary>
        /// If this contribution is system collected
        /// </summary>
        public bool IsSystemCollected { get; set; }

        /// <summary>
        /// If this contribution belongs to latest award cycle.
        /// </summary>
        public bool IsBelongToLatestAwardCycle { get; set; }
    }
}
