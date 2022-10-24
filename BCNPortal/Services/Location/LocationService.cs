using BCNPortal.Dto.Location;
using BCNPortal.DTO.Filter;
using BCNPortal.DTO.Location;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Utility;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Text;

namespace BCNPortal.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly ITokenRequestService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string _udrfAddress;
        private readonly string _getAllLocation;
        private readonly string _getLocation;
        private readonly string _getSingleLocation;
        private readonly ITokenRequestService _tokenRequestService;
        public LocationService(ITokenRequestService tokenRequestService)
        {
            _udrfAddress = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_Address"];
            _getAllLocation = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getAllLocations"];
            _getLocation = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getLocations"];
            _getSingleLocation = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getSingleLocation"];
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
                    using (var response = await httpClient.PostAsync(_udrfAddress + _getLocation, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            locationDto = JsonConvert.DeserializeObject<List<LocationDto>>(apiResponse);
                        }
                       
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
        public async Task<List<LocationListDto>> GetAllLocations(TokenPlusId tokenPlusId)
        {
            //var locationDto = new List<LocationDto>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //StringContent content = new StringContent(JsonConvert.SerializeObject(baseFilter), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.GetAsync(_udrfAddress + _getAllLocation))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var locationDto = JsonConvert.DeserializeObject<List<LocationListDto>>(apiResponse);
                            return locationDto;
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
        public async Task<LocationDto> GetSingleLocation(TokenPlusId tokenPlusId, Guid locationId)
        {
            //var locationDto = new List<LocationDto>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(locationId), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(_udrfAddress + _getSingleLocation, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var locationDto = JsonConvert.DeserializeObject<LocationDto>(apiResponse);
                            return locationDto;
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
