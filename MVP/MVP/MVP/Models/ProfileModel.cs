using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Mvp.Models
{
    public class ProfileModel
    {
        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string AwardCategoryDisplay { get; set; }

        [DataMember]
        public string Biography { get; set; }

        [DataMember]
        public long FirstAwardYear { get; set; }

        [DataMember]
        public long YearsAsMVP { get; set; }

        [DataMember]
        public string Abstract { get; set; }

        [DataMember]
        public int ContributionCount { get; set; }

        [DataMember]
        public int MvpId { get; set; }
    }

    public class ContributionInfo
    {
        public int TotalContributions { get; set; }
        public List<ContributionModel> Contributions { get; set; }
    }
}
