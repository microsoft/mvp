using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Microsoft.Mvp.Models
{
    /// <summary>
    /// The contribution technology model.
    /// </summary>
    public class ContributionTechnologyModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        //[Required(ErrorMessage = null, ErrorMessageResourceName = "RequiredFieldMessageText")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        public string AwardName { get; set; } 

        public string Statuscode { get; set; }

        public string Active { get; set; }

    }

    public class ContributionAreaInfo
    {
        public string ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public List<ContributionAreaData> Data { get; set; }

    }

    public class ContributionAreaData
    {
        public string AwardCategory { get; set; }

        public List<ContributionDetail> Contributions { get; set; }
    }

    public class ContributionDetail
    {
        public string AwardName { get; set; }

        public List<ContributionTechnologyModel> ContributionArea { get; set; }
    }
}