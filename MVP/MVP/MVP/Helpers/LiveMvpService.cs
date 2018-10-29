using Microsoft.Mvp.Models;
using Microsoft.Mvp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using MvvmHelpers;

namespace Microsoft.Mvp.Helpers
{
	public class LiveMvpService : IDisposable, IMvpService
    {
		private const int _bufferSize = 1024;

		private readonly JsonSerializer _serializer = JsonSerializer.CreateDefault();

		private HttpClient _httpClient;

		private HttpClient GetHttpClient(string token)
		{
			if (_httpClient == default(HttpClient))
			{
				_httpClient = new HttpClient { Timeout = new System.TimeSpan(0, 0, 15) };

				_httpClient.DefaultRequestHeaders.Add(CommonConstants.OcpApimSubscriptionKey, CommonConstants.OcpApimSubscriptionValue);

				return _httpClient;
			}
			
			return _httpClient;
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
			HttpResponseMessage response = null;

			try
			{				
				using (var msg = new HttpRequestMessage(httpMethod, url))
				{
					HttpContent theContent = null;
					if (model != null)
						msg.Content = new JsonContent(model, true, _bufferSize);
						
					msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
					
					if (!isImage)
						msg.Headers.Add(CommonConstants.AcceptsKey, CommonConstants.MediaTypeForJson);
					
					response = await GetHttpClient(token).SendAsync(msg).ConfigureAwait(false);
					theContent?.Dispose();
				}
					
				// Parse response
				if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.Created)
				{
					var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
						var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
						string newAccessToken = await LiveIdLogOnViewModel.GetNewAccessToken().ConfigureAwait(false);
						string result = await DoWork(url, model, httpMethod, newAccessToken, isImage, true, isAddOrUpdateContribution).ConfigureAwait(false);
						return result;
					}
				}
				else
				{
					if (CheckInternetConnection())
					{
						string newAccessToken = await LiveIdLogOnViewModel.GetNewAccessToken().ConfigureAwait(false);
						string result = await DoWork(url, model, httpMethod, newAccessToken, isImage, true, isAddOrUpdateContribution).ConfigureAwait(false);
						return result;
					}
					else
					{
						throw new WebException(string.Format(System.Globalization.CultureInfo.InvariantCulture, TranslateServices.GetResourceString(CommonConstants.NetworkErrorFormatString), response.StatusCode.ToString()));
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
			finally
			{
				response?.Dispose();
			}

            if (!string.IsNullOrEmpty(errorMsg))
            {
                MyProfileViewModel.Instance.ErrorMessage = errorMsg;
				throw new Exception(errorMsg);
            }
            return null;
        }

		/// <summary>
        /// Handle Add/Del/Modify of Contributions.
        /// </summary>
        /// <param name="model">Instance of ContributionModel</param>
        /// <param name="httpMethod">Put/Post/Delete</param>
        /// <returns></returns>
        async Task<T> DoWork<T>(string url, Object model, HttpMethod httpMethod, string token, bool isImage, bool isRefreshedToken, bool isAddOrUpdateContribution = false)
        {

            MyProfileViewModel.Instance.ErrorMessage = string.Empty;
            string errorMsg = "";
			HttpResponseMessage response = null;

			try
			{				
				using (var msg = new HttpRequestMessage(httpMethod, url))
				{
					HttpContent theContent = null;
					if (model != null)
						msg.Content = new JsonContent(model, false, _bufferSize);
						
					msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
					
					if (!isImage)
						msg.Headers.Add(CommonConstants.AcceptsKey, CommonConstants.MediaTypeForJson);
					
					response = await GetHttpClient(token).SendAsync(msg).ConfigureAwait(false);
					theContent?.Dispose();
				}
					
				// Parse response
				if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.Created)
				{
					using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new StreamReader(stream))
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        return _serializer.Deserialize<T>(jsonTextReader);
                    }
				}
				else if (response.StatusCode == HttpStatusCode.BadRequest)
				{
					if (isAddOrUpdateContribution)
					{
						var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
						string newAccessToken = await LiveIdLogOnViewModel.GetNewAccessToken().ConfigureAwait(false);
						return await DoWork<T>(url, model, httpMethod, newAccessToken, isImage, true, isAddOrUpdateContribution).ConfigureAwait(false);
					}
				}
				else
				{
					if (CheckInternetConnection())
					{
						string newAccessToken = await LiveIdLogOnViewModel.GetNewAccessToken().ConfigureAwait(false);
						return await DoWork<T>(url, model, httpMethod, newAccessToken, isImage, true, isAddOrUpdateContribution).ConfigureAwait(false);
					}
					else
					{
						throw new WebException(string.Format(System.Globalization.CultureInfo.InvariantCulture, TranslateServices.GetResourceString(CommonConstants.NetworkErrorFormatString), response.StatusCode.ToString()));
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
			finally
			{
				response?.Dispose();
			}

            if (!string.IsNullOrEmpty(errorMsg))
            {
                MyProfileViewModel.Instance.ErrorMessage = errorMsg;
				throw new Exception(errorMsg);
            }
            return default(T);
        }

        public static bool CheckInternetConnection() => CrossConnectivity.Current.IsConnected;

        public async Task<string> GetPhoto(string token)
        {
            string responseTxt = await DoWork(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfGetMvpProfileImage, CommonConstants.BaseUrl), null, HttpMethod.Get, token, false, false).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(responseTxt))
            {
                responseTxt = responseTxt.Substring(1, responseTxt.Length - 2);
                return responseTxt;
            }
            return "";
        }

        public Task<ProfileModel> GetProfile(string token)
        {
            return DoWork<ProfileModel>(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfGetMvpProfile, CommonConstants.BaseUrl), null, HttpMethod.Get, token, false, false);
        }

        public async Task<ContributionTypeDetail> GetContributionTypes(string token)
        {
            var response = await DoWork<ObservableRangeCollection<ContributionTypeModel>>(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfGetContributionTypes, CommonConstants.BaseUrl), null, HttpMethod.Get, token, false, false).ConfigureAwait(false);

			if (response == default(ObservableRangeCollection<ContributionTypeModel>))
				return default(ContributionTypeDetail);

			return new ContributionTypeDetail
			{
				ContributionTypes = response
			};
        }

        public async Task<ContributionDetail> GetContributionAreas(string token)
        {
            var contributionModels = new List<ContributionTechnologyModel>();

            var liInfo = await DoWork<IList<ContributionAreaData>>(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfGetContributionAreas, CommonConstants.BaseUrl), null, HttpMethod.Get, token, false, false).ConfigureAwait(false);
            
            if (liInfo?.Any() ?? false)
            {
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

				var detail = new ContributionDetail
				{
					ContributionArea = contributionModels
				};

				return detail;
            }
            return null;
        }

        public Task<ContributionInfo> GetContributions(int start, int size, string token)
        {
            return DoWork<ContributionInfo>(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfGetContributions, CommonConstants.BaseUrl, start, size), null, HttpMethod.Get, token, false, false);
        }

        public Task<ContributionModel> GetContributionModel(int privateSiteId, string token)
        {
            return DoWork<ContributionModel>(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfGetContributionById, privateSiteId), null, HttpMethod.Get, token, false, false);
        }

        public async Task<ContributionModel> AddContributionModel(ContributionModel model, string token)
        {
            var item = await DoWork<ContributionModel>(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfPutContribution, CommonConstants.BaseUrl), model, HttpMethod.Post, token, false, false, true).ConfigureAwait(false);

			if (item == default(ContributionModel))
				return item;
            
            item.ContributionType.Name = item.ContributionType.Name.Replace("Sample Code", "Code Samples");
            return item;
        }

        public Task<string> EditContributionModel(ContributionModel model, string token)
        {
            return DoWork(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfPostContribution, CommonConstants.BaseUrl), model, HttpMethod.Put, token, false, false, true);
        }

        public Task<string> DeleteContributionModel(int privateSiteId, string token)
        {
            return DoWork(string.Format(System.Globalization.CultureInfo.InvariantCulture, CommonConstants.ApiUrlOfDeleteContribution, CommonConstants.BaseUrl, privateSiteId), null, HttpMethod.Delete, token, false, false);
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
