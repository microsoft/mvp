using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Mvp.Models;
using Xamarin.Forms;
using Microsoft.Mvp.Helpers;

namespace Microsoft.Mvp.Helpers
{
    public class DesignMvpService : IMvpService
    {
        public Task<ContributionModel> AddContributionModel(ContributionModel model, string token)
        {
            return Task.FromResult(model);
        }

        public Task<string> DeleteContributionModel(int privateSiteId, string token)
        {
            return Task.FromResult("OK");
        }

        public Task<string> EditContributionModel(ContributionModel model, string token)
        {
            return Task.FromResult("OK");
        }

        public Task<ContributionDetail> GetContributionAreas(string token)
        {
            return Task.FromResult(new ContributionDetail
            {
                AwardName = "Xamarin",
                ContributionArea = new List<ContributionTechnologyModel>
                {
                    new ContributionTechnologyModel
                    {
                        Active = "Active",
                        AwardName = "Award Name",
                        Id = "1",
                        Name = "Technology Name",
                        Statuscode = "Code"
                    }
                }
            });
        }

        public Task<ContributionModel> GetContributionModel(int privateSiteId, string token)
        {
            return Task.FromResult(new ContributionModel
            {
                AllAnswersUrl = "Url",
                AllPostsUrl = "Url",
                AnnualQuantity = 1,
                AnnualReach = 1,
                ContributionId = "Id",
                ContributionTechnology = new ContributionTechnologyModel
                {
                    Active = "Active",
                    AwardName = "Award Name",
                    Id = "1",
                    Name = "Technology Name",
                    Statuscode = "Code"
                },
                ContributionType = new ContributionTypeModel
                {
                    EnglishName = "Xamarin",
                    Id = "Id",
                    Name = "Xamarin"
                },
                Description = "Awesome Description",
                // Icon = "Icon",
                IsBelongToLatestAwardCycle = true,
                IsSystemCollected = true,
                LabelTextOfContribution = "Contribution",
                ReferenceUrl = "ref url",
                SecondAnnualQuantity = 1,
                StartDate = DateTime.Now,
                Title = "Title",
                Visibility = new VisibilityModel
                {
                    Description = "Description",
                    Id = 1,
                    LocalizeKey = "en"
                }
            });
        }

        public Task<ContributionInfo> GetContributions(int start, int size, string token)
        {
            return Task.FromResult(new ContributionInfo
            {
                Contributions = new List<ContributionModel>
                {
                   new ContributionModel
                    {
                        AllAnswersUrl = "Url",
                        AllPostsUrl = "Url",
                        AnnualQuantity = 1,
                        AnnualReach = 1,
                        ContributionId = "Id",
                        ContributionTechnology = new ContributionTechnologyModel
                        {
                            Active = "Active",
                            AwardName = "Award Name",
                            Id = "1",
                            Name = "Technology Name",
                            Statuscode = "Code"
                        },
                        ContributionType = new ContributionTypeModel
                        {
                            EnglishName = "Xamarin",
                            Id = "Id",
                            Name = "Conference (organizer)"
                        },
                        Description = "Awesome Description",
                        IsBelongToLatestAwardCycle = true,
                        IsSystemCollected = true,
                        LabelTextOfContribution = "Contribution",
                        ReferenceUrl = "ref url",
                        SecondAnnualQuantity = 1,
                        StartDate = DateTime.Now,
                        Title = "Title",
                        Visibility = new VisibilityModel
                        {
                            Description = "Description",
                            Id = 1,
                            LocalizeKey = "en"
                        }
                    }
                },
                TotalContributions = 1
            });
        }

        public Task<ContributionTypeDetail> GetContributionTypes(string token)
        {
            return Task.FromResult(new ContributionTypeDetail
            {
                ContributionTypes = new MvvmHelpers.ObservableRangeCollection<ContributionTypeModel>
                {
                    new ContributionTypeModel
                        {
                            EnglishName = "Xamarin",
                            Id = "Id",
                            Name = "Xamarin"
                        },
                }
            });
        }

        public Task<string> GetPhoto(string token)
        {
            return Task.FromResult("https://s.gravatar.com/avatar/5df4d86308e585c879c19e5f909d8bfe?s=200");
        }

        public Task<ProfileModel> GetProfile(string token)
        {
            return Task.FromResult(new ProfileModel
            {
                Abstract = "Live, love, bike, and code. Principal Program Manager, Mobile Developer Tools @Microsoft",
                AwardCategoryDisplay = "Xamarin,Other cool stuff",
                Biography = "James Montemagno is a Principal Program Manager for Mobile Developer Tools at Microsoft. He has been a .NET developer since 2005, working in a wide range of industries including game development, printer software, and web services. Prior to becoming a Principal Program Manager, James was a professional mobile developer and has now been crafting apps since 2011 with Xamarin. In his spare time, he is most likely cycling around Seattle or guzzling gallons of coffee at a local coffee shop. He can be found on Twitter @JamesMontemagno, blogs code regularly on his personal blog http://www.montemagno.com, and co-hosts the weekly development podcast Merge Conflict http://mergeconflict.fm.",
                ContributionCount = 10000000,
                DisplayName = "James Montemagno",
                FirstAwardYear = 2015,
                YearsAsMVP = 2,
                MvpId = 12345678
            });
        }
    }
}
