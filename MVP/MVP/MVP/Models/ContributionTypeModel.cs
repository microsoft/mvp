using MvvmHelpers;

namespace Microsoft.Mvp.Models
{
    public class ContributionTypeModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string EnglishName { get; set; }
    }

    public class ContributionTypeDetail
    {
        public ObservableRangeCollection<ContributionTypeModel> ContributionTypes { get; set; }
    }
}