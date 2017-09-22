using Microsoft.Mvp.Models;
using System.Threading.Tasks;

namespace Microsoft.Mvp.Helpers
{
    public interface IMvpService
    {
        Task<string> GetPhoto(string token);

        Task<ProfileModel> GetProfile(string token);


        Task<ContributionTypeDetail> GetContributionTypes(string token);


        Task<ContributionDetail> GetContributionAreas(string token);


        Task<ContributionInfo> GetContributions(int start, int size, string token);


        Task<ContributionModel> GetContributionModel(int privateSiteId, string token);


        Task<ContributionModel> AddContributionModel(ContributionModel model, string token);

        Task<string> EditContributionModel(ContributionModel model, string token);


        Task<string> DeleteContributionModel(int privateSiteId, string token);

    }
}
