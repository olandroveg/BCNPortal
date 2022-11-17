using BCNPortal.DTO.BcNode;
using BCNPortal.DTO.Filter;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.NFMapping;
using BCNPortal.Utility;
using Newtonsoft.Json;
using System.Text;

namespace BCNPortal.Services.BcNodeRqst
{
    public class BcNodeService : IBcNodeService
    {
        private readonly string _udrfName;
        private readonly string _udrfAddress; 
        private readonly string _getBcNodes;
        private readonly string _getAllBcNodes;
        private readonly string _getSingleBcNode;
        private readonly string _sendBcNode;
        private readonly string _deleteRange;
        private readonly INFMapService _nfMapService;

        public BcNodeService(INFMapService nFMapService)
        {
            _udrfName= StaticConfigurationManager.AppSetting["NRFdiscoveryNF:UDRF"];
            _udrfAddress = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_Address"];
            _getBcNodes = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getBcNodes"];
            _getAllBcNodes = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getAllBcNodes"];
            _getSingleBcNode = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getSingleBcNode"];
            _sendBcNode = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_sendBcNode"];
            _deleteRange = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_deleteRangeBcNode"];
            _nfMapService = nFMapService;
        }

        
        public async Task <List<BcNodeDto>> GetBcNodes(TokenPlusId tokenPlusId, BaseFilter baseFilter)
        {
            
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _getBcNodes);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(baseFilter), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(api, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var bcNodesDto = JsonConvert.DeserializeObject<List<BcNodeDto>>(apiResponse);
                            return bcNodesDto;
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
        public async Task<List<BcNodeDto>> GetAllBcNodes(TokenPlusId tokenPlusId)
        {
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _getAllBcNodes);
                using (var httpClient = new HttpClient())
                {
                    //StringContent content = new StringContent(JsonConvert.SerializeObject(baseFilter), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.GetAsync(api))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var bcNodesDto = JsonConvert.DeserializeObject<List<BcNodeDto>>(apiResponse);
                            return bcNodesDto;
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
        public async Task<BcNodeDto> GetBcNode(TokenPlusId tokenPlusId, Guid bcNodeId)
        {
            try
            {
                if (bcNodeId == Guid.Empty)
                    throw new ArgumentNullException("bcNodeId is empty");

                var api = _nfMapService.GetEndPoint(_udrfName, _getSingleBcNode);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(bcNodeId), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(api, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var bcNodesDto = JsonConvert.DeserializeObject<BcNodeDto>(apiResponse);
                            return bcNodesDto;
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

        public async Task<Guid> SendContentDto(TokenPlusId tokenPlusId, BcNodeDto bcNodeDto)
        {
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _sendBcNode);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(bcNodeDto), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(api, content))
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
        public async Task<bool> DeleteRange(TokenPlusId tokenPlusId, IEnumerable<Guid> bcnodeContentsIds)
        {
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _deleteRange);
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
