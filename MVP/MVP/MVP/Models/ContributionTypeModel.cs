using MvvmHelpers;

namespace Microsoft.Mvp.Models
{
    /// <summary>
    /// The contribution type.
    /// </summary>
    public class ContributionTypeModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the english name
        /// </summary>
        public string EnglishName { get; set; }
    }

    public class ContributionTypeDetail
    {  
        public ObservableRangeCollection<ContributionTypeModel> ContributionTypes { get; set; }
    }
}