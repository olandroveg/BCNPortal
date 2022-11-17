using System;
using System.Text;
using BCNPortal.Dto.BcNodeContent;
using BCNPortal.Dto.Service;
using BCNPortal.DTO;
using BCNPortal.DTO.Filter;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.NFMapping;
using BCNPortal.Utility;
using Newtonsoft.Json;

namespace BCNPortal.Services.BcNodeContent
{
    public class BcNodeContentService : IBcNodeContentService
    {
        private readonly string _udrfAddress;
        private readonly string _getBcNodeContents;
        private readonly string _getContents;
        private readonly string _sendBcNodeContents;
        private readonly string _getServicesDto;
        private readonly string _getBcNodeContentDto;
        private readonly string _deleteRangeBcNodeContents;
        private readonly string _udrfName;
        private readonly INFMapService _nfMapService;

        public BcNodeContentService(INFMapService nFMapService)
        {
            _udrfName = StaticConfigurationManager.AppSetting["NRFdiscoveryNF:UDRF"];
            _udrfAddress = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_Address"];
            _getBcNodeContents = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getBcNodeContents"];
            _getContents = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getContents"];
            _sendBcNodeContents = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_sendBcNodeContents"];
            _getServicesDto = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getServicesDto"];
            _getBcNodeContentDto = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getBcNodeContentDto"];
            _deleteRangeBcNodeContents = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_deleteRangeBcNodeContents"];
            _nfMapService = nFMapService;

        }

        public async Task<List<BcNodeContentDto>> GetBcNodeContentDto(TokenPlusId tokenPlusId, BaseFilter baseFilter)
        {
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _getBcNodeContents);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(baseFilter), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(api, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var bcNodesContentDto = JsonConvert.DeserializeObject<List<BcNodeContentDto>>(apiResponse);
                            return bcNodesContentDto;
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
        public async Task<List<BaseDTO>> GetAllContents(TokenPlusId tokenPlusId, string bcNodeId)
        {
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _getContents);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(bcNodeId), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(api, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var contentsBaseDto = JsonConvert.DeserializeObject<List<BaseDTO>>(apiResponse);
                            return contentsBaseDto;
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
        public async Task<Guid> SendBcNodeContentDto(TokenPlusId tokenPlusId, BcNodeContentDto bcNodeContentDto)
        {
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _sendBcNodeContents);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(bcNodeContentDto), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(api, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var bcNodeContentId = JsonConvert.DeserializeObject<Guid>(apiResponse);
                            return bcNodeContentId;
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

        public async Task<List<ServiceDto>> GetAllServicesDto(TokenPlusId tokenPlusId)
        {
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _getServicesDto);
                using (var httpClient = new HttpClient())
                {
                    //StringContent content = new StringContent(JsonConvert.SerializeObject(bcNodeId), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.GetAsync(api))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var serviceDto = JsonConvert.DeserializeObject<List<ServiceDto>>(apiResponse);
                            return serviceDto;
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
        public async Task<BcNodeContentDto> GetBcNodeContentDto(TokenPlusId tokenPlusId, Guid bcNodeContentId)
        {
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _getBcNodeContentDto);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(bcNodeContentId), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(api, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var bcNodeContentDto = JsonConvert.DeserializeObject<BcNodeContentDto>(apiResponse);
                            return bcNodeContentDto;
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
        public async Task<bool> DeleteRangeBcNodeContents(TokenPlusId tokenPlusId, IEnumerable<Guid> bcnodeContentsIds)
        {
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _deleteRangeBcNodeContents);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(bcnodeContentsIds), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(api, content))
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

