using BCNPortal.Dto.Content;
using BCNPortal.Dto.Service;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Utility;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Text;

namespace BCNPortal.Services.Content
{
    public class ContentService : IContentService
    {

        
        private readonly ITokenRequestService _tokenRequestService;
        private readonly string _udrfAddress;
        private readonly string _getAllContents;
        private readonly string _getAllServices;
        private readonly string _getSingleContent;
        private readonly string _sendContent;
        private readonly string _deleteRange;

        public ContentService(ITokenRequestService tokenRequestService)
        {
            _udrfAddress = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_Address"];
            _getAllContents = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getAllContents"];
            _getAllServices = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getAllServices"];
            _getSingleContent = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getSingleContent"];
            _sendContent = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_sendContent"];
            _deleteRange = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_deleteRangeContents"];
            _tokenRequestService = tokenRequestService;
            
        }
        public async Task<List<ContentTableDto>> GetAllContents(TokenPlusId tokenPlusId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //StringContent content = new StringContent(JsonConvert.SerializeObject(baseFilter), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.GetAsync(_udrfAddress + _getAllContents))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var listContentsDto = JsonConvert.DeserializeObject<List<ContentTableDto>>(apiResponse);
                            return listContentsDto;
                        }

                        throw new Exception(HttpResponseCode.GetMessageFromStatus(response.StatusCode));
                    }

                }
                //return locationDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<ServiceDto>> GetAllServices(TokenPlusId tokenPlusId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //StringContent content = new StringContent(JsonConvert.SerializeObject(baseFilter), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.GetAsync(_udrfAddress + _getAllServices))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var listServiceDto = JsonConvert.DeserializeObject<List<ServiceDto>>(apiResponse);
                            return listServiceDto;
                        }

                        throw new Exception(HttpResponseCode.GetMessageFromStatus(response.StatusCode));
                    }

                }
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<ContentDto> GetSingleContent(TokenPlusId tokenPlusId, Guid contentId)
        {
            
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(contentId), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(_udrfAddress + _getSingleContent, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var locationDto = JsonConvert.DeserializeObject<ContentDto>(apiResponse);
                            return locationDto;
                        }

                        throw new Exception(HttpResponseCode.GetMessageFromStatus(response.StatusCode));
                    }

                }
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Guid> SendContentDto(TokenPlusId tokenPlusId, ContentDto contentDto)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(contentDto), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(_udrfAddress + _sendContent, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var contentId = JsonConvert.DeserializeObject<Guid>(apiResponse);
                            return contentId;
                        }

                        throw new Exception(HttpResponseCode.GetMessageFromStatus(response.StatusCode));
                    }

                }
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> DeleteRange(TokenPlusId tokenPlusId, IEnumerable<Guid> contentIds)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(contentIds), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(_udrfAddress + _deleteRange, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return true;
                        }

                        throw new Exception(HttpResponseCode.GetMessageFromStatus(response.StatusCode));
                    }

                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
