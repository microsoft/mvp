using Microsoft.Mvp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Mvp.ViewModels;
using MvvmHelpers;
using Plugin.Connectivity;
using Microsoft.Mvp.Helpers;
using Xamarin.Forms;

//[assembly: Dependency(typeof(LiveMvpService))]
namespace Microsoft.Mvp.Helpers
{
    public class LiveMvpService : IDisposable, IMvpService
    {
        public enum HttpMethod
        {
            Put,
            Delete,
            Get,
            Post
        }

        /// <summary>
        /// Handle Add/Del/Modify of Contributions.
        /// </summary>
        /// <param name="model">Instance of ContributionModel</param>
        /// <param name="httpMethod">Put/Post/Delete</param>
        /// <returns></returns>
        async Task<string> DoWork(string url, Object model, HttpMethod httpMethod, string token, bool isImage, bool isRefreshedToken, bool isAddOrUpdateContribution = false)
        {

            MyProfileViewModel.Instance.ErrorMessage = string.Empty;
            string errorMsg = "";
			try
			{
				var handler = new HttpClientHandler
				{
					UseDefaultCredentials = false,
				};

				using (var client = new HttpClient(handler) { Timeout = new System.TimeSpan(0, 0, 15) })
				{
					client.DefaultRequestHeaders.Add(CommonConstants.OcpApimSubscriptionKey, CommonConstants.OcpApimSubscriptionValue);
					client.DefaultRequestHeaders.Add(CommonConstants.AuthorizationKey, "Bearer " + token);
					if (!isImage)
					{
						client.DefaultRequestHeaders.Add(CommonConstants.AcceptsKey, CommonConstants.MediaTypeForJson);
					}

					string requestJsonString = string.Empty;
					if (model != null)
					{
						requestJsonString = JsonConvert.SerializeObject(model);
					}

					HttpResponseMessage response = null;
					using (StringContent theContent = new StringContent(requestJsonString, System.Text.Encoding.UTF8, CommonConstants.MediaTypeForJson))
					{
						switch (httpMethod)
						{
							case HttpMethod.Put:
								response = await client.PutAsync(url, theContent);
								break;
							case HttpMethod.Post:
								response = await client.PostAsync(url, theContent);
								break;
							case HttpMethod.Delete:
								response = await client.DeleteAsync(url);
								break;
							case HttpMethod.Get:
								response = await client.GetAsync(url);
								break;
							default:
								break;
						}

						// Parse response
						if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.Created)
						{
							var responseString = await response.Content.ReadAsStringAsync();
							if (string.IsNullOrEmpty(responseString))
							{
								return CommonConstants.OkResult;
							}
							return responseString;
						}
						else if (response.StatusCode == HttpStatusCode.BadRequest)
						{
							if (isAddOrUpdateContribution)
							{
								var responseString = await response.Content.ReadAsStringAsync();
								if (!string.IsNullOrEmpty(responseString))
								{
									errorMsg = responseString;
								}
							}
							else
							{
								throw new WebException(string.Format(System.Globalization.CultureInfo.InvariantCulture, TranslateServices.GetResourceString(CommonConstants.NetworkErrorFormatString), response.StatusCode.ToString()));
							}
						}
						else if (response.StatusCode == HttpStatusCode.Forbidden)
						{
							if (isRefreshedToken)
							{
								throw new WebException(string.Format(System.Globalization.CultureInfo.InvariantCulture, TranslateServices.GetResourceString(CommonConstants.NetworkErrorFormatString), response.StatusCode.ToString()));
							}
							else
							{
								string newAccessToken = await LiveIdLogOnViewModel.GetNewAccessToken();
								string result = await DoWork(url, model, httpMethod, newAccessToken, isImage, true, isAddOrUpdateContribution);
								return result;
							}
						}
						else
						{
							if (CheckInternetConnection())
							{
								string newAccessToken = await LiveIdLogOnViewModel.GetNewAccessToken();
								string result = await DoWork(url, model, httpMethod, newAccessToken, isImage, true, isAddOrUpdateContribution);
								return result;
							}
							else
							{
								throw new WebException(string.Format(System.Globalization.CultureInfo.InvariantCulture, TranslateServices.GetResourceString(CommonConstants.NetworkErrorFormatString), response.StatusCode.ToString()));
							}

						}
					}
				}
			}
			catch (WebException ex)
			{
				errorMsg = TranslateServices.GetResourceString(CommonConstants.DefaultNetworkErrorString);
			}
			catch (HttpRequestException ex)
			{
				errorMsg = TranslateServices.GetResourceString(CommonConstants.DefaultNetworkErrorString);
			}
			catch (Exception ex) {
				errorMsg = TranslateServices.GetResourceString(CommonConstants.DefaultNetworkErrorString);
			}

            if (!string.IsNullOrEmpty(errorMsg))
            {
                MyProfileViewModel.Instance.ErrorMessage = errorMsg;
				throw new Exception(errorMsg);
            }
            return null;
        }


        public static bool CheckInternetConnection() => CrossConnectivity.Current.IsConnected;

        public async Task<string> GetPhoto(string token)
        {
            string responseTxt = await DoWork(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfGetMvpProfileImage, CommonConstants.BaseUrl), null, HttpMethod.Get, token, false, false);

            if (!string.IsNullOrEmpty(responseTxt))
            {
                responseTxt = responseTxt.Substring(1, responseTxt.Length - 2);
                return responseTxt;
            }
            return "";
        }

        public async Task<ProfileModel> GetProfile(string token)
        {
            string responseTxt = await DoWork(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfGetMvpProfile, CommonConstants.BaseUrl), null, HttpMethod.Get, token, false, false);

            if (!string.IsNullOrEmpty(responseTxt))
            {
                return JsonConvert.DeserializeObject<ProfileModel>(responseTxt);
            }
            return null;

        }

        public async Task<ContributionTypeDetail> GetContributionTypes(string token)
        {
            string responseTxt = await DoWork(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfGetContributionTypes, CommonConstants.BaseUrl), null, HttpMethod.Get, token, false, false);

            if (!string.IsNullOrEmpty(responseTxt))
            {
                ContributionTypeDetail contributionTypeDetail = new ContributionTypeDetail();
                contributionTypeDetail.ContributionTypes = JsonConvert.DeserializeObject<ObservableRangeCollection<ContributionTypeModel>>(responseTxt);
                return contributionTypeDetail;
            }
            return null;


        }

        public async Task<ContributionDetail> GetContributionAreas(string token)
        {
            List<ContributionTechnologyModel> contributionModels = new List<ContributionTechnologyModel>();

            string responseTxt = await DoWork(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfGetContributionAreas, CommonConstants.BaseUrl), null, HttpMethod.Get, token, false, false);
            if (!string.IsNullOrEmpty(responseTxt))
            {
                IList<ContributionAreaData> liInfo = JsonConvert.DeserializeObject<IList<ContributionAreaData>>(responseTxt);

                foreach (var liData in liInfo)
                {
                    foreach (var contribution in liData.Contributions)
                    {
                        foreach (var area in contribution.ContributionArea)
                        {
                            contributionModels.Add(area);
                        }
                    }
                }

                ContributionDetail detail = new ContributionDetail();
                detail.ContributionArea = contributionModels;

                return detail;
            }
            return null;


        }

        public async Task<ContributionInfo> GetContributions(int start, int size, string token)
        {
            string result = await DoWork(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfGetContributions, CommonConstants.BaseUrl, start, size), null, HttpMethod.Get, token, false, false);
            if (!string.IsNullOrEmpty(result))
            {
                return JsonConvert.DeserializeObject<ContributionInfo>(result);
            }
            return null;

        }

        public async Task<ContributionModel> GetContributionModel(int privateSiteId, string token)
        {
            string result = await DoWork(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfGetContributionById, privateSiteId), null, HttpMethod.Get, token, false, false);

            if (!string.IsNullOrEmpty(result))
            {
                return JsonConvert.DeserializeObject<ContributionModel>(result);
            }
            return null;

        }

        public async Task<ContributionModel> AddContributionModel(ContributionModel model, string token)
        {
            string responseTxt = await DoWork(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfPutContribution, CommonConstants.BaseUrl), model, HttpMethod.Post, token, false, false, true);
            if (!string.IsNullOrEmpty(responseTxt))
            {
                var item = JsonConvert.DeserializeObject<ContributionModel>(responseTxt);

                item.ContributionType.Name = item.ContributionType.Name.Replace("Sample Code", "Code Samples");
                return item;
            }
            return null;

        }

        public async Task<string> EditContributionModel(ContributionModel model, string token)
        {
            string responseTxt = await DoWork(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfPostContribution, CommonConstants.BaseUrl), model, HttpMethod.Put, token, false, false, true);

            return responseTxt;
        }

        public async Task<string> DeleteContributionModel(int privateSiteId, string token)
        {
            string responseTxt = await DoWork(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfDeleteContribution, CommonConstants.BaseUrl, privateSiteId), null, HttpMethod.Delete, token, false, false);
            return responseTxt;
        }

        public void Dispose()
        {
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);

        }


        private void Dispose(bool disposing)
        {

        }
    }
}
