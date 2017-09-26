using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Microsoft.Mvp.Models
{
    public class ContributionTechnologyModel
    {
        public string Id { get; set; }

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