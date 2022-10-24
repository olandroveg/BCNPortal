using BCNPortal.DTO.Filter;
using BCNPortal.DTO.Location;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Utility;
using Newtonsoft.Json;
using System.Text;

namespace BCNPortal.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly string _udrfAddress;
        private readonly string _getBcNodeApi;
        private readonly ITokenRequestService _tokenRequestService;
        public LocationService(ITokenRequestService tokenRequestService)
        {
            _udrfAddress = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_Address"];
            _getBcNodeApi = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getAllLocations"];
            _tokenRequestService = tokenRequestService;
        }
        public async Task<List<LocationDto>> GetLocations(TokenPlusId tokenPlusId, BaseFilter baseFilter)
        {
            var locationDto = new List<LocationDto>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(baseFilter), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(_udrfAddress + _getBcNodeApi, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            locationDto = JsonConvert.DeserializeObject<List<LocationDto>>(apiResponse);
                        }
                        else
                            throw new Exception(HttpResponseCode.GetMessageFromStatus(response.StatusCode));
                    }

                }
                return locationDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<LocationDto>> GetAllLocations(TokenPlusId tokenPlusId)
        {
            //var locationDto = new List<LocationDto>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //StringContent content = new StringContent(JsonConvert.SerializeObject(baseFilter), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.GetAsync(_udrfAddress + _getBcNodeApi))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var locationDto = JsonConvert.DeserializeObject<List<LocationDto>>(apiResponse);
                            return locationDto;
                        }
                        else
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
