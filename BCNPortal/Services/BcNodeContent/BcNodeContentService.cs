using System;
using System.Text;
using BCNPortal.Dto.BcNodeContent;
using BCNPortal.DTO.Filter;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Utility;
using Newtonsoft.Json;

namespace BCNPortal.Services.BcNodeContent
{
    public class BcNodeContentService : IBcNodeContentService
    {
        private readonly string _udrfAddress;
        private readonly string _getBcNodeContents;

        private readonly ITokenRequestService _tokenRequestService;

        public BcNodeContentService(ITokenRequestService tokenRequestService)
        {
            _udrfAddress = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_Address"];
            _getBcNodeContents = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getBcNodeContents"];
        }

        public async Task<List<BcNodeContentDto>> GetBcNodeContentDto(TokenPlusId tokenPlusId, BaseFilter baseFilter)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(baseFilter), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(_udrfAddress + _getBcNodeContents, content))
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
    }
}

