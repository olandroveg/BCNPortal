using BCNPortal.Dto.Content;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Utility;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace BCNPortal.Services.Content
{
    public class ContentService : IContentService
    {

        
        private readonly ITokenRequestService _tokenRequestService;
        private readonly string _udrfAddress;
        private readonly string _getAllContents;

        public ContentService(ITokenRequestService tokenRequestService)
        {
            _udrfAddress = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_Address"];
            _getAllContents = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getAllContents"];

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
    }
}
