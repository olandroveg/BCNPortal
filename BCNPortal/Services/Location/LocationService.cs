﻿using BCNPortal.Dto.Location;
using BCNPortal.DTO.Filter;
using BCNPortal.DTO.Location;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.NFMapping;
using BCNPortal.Utility;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Text;

namespace BCNPortal.Services.Location
{
    public class LocationService : ILocationService
    {
        
        
        private readonly string _udrfAddress;
        private readonly string _getAllLocation;
        private readonly string _getLocation;
        private readonly string _getSingleLocation;
        private readonly string _sendLocation;
        private readonly string _deleteRange;
        private readonly string _udrfName;
        private readonly INFMapService _nfMapService;
        
        public LocationService(INFMapService nFMapService)
        {
            _udrfAddress = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_Address"];
            _getAllLocation = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getAllLocations"];
            _getLocation = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getLocations"];
            _getSingleLocation = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getSingleLocation"];
            _sendLocation = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_sendLocation"];
            _deleteRange = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_deleteRangeLocations"];
            _udrfName = StaticConfigurationManager.AppSetting["NRFdiscoveryNF:UDRF"];
            _nfMapService = nFMapService;


        }
        public async Task<List<LocationDto>> GetLocations(TokenPlusId tokenPlusId, BaseFilter baseFilter)
        {
            
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _getLocation);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(baseFilter), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(api, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var locationDto = JsonConvert.DeserializeObject<List<LocationDto>>(apiResponse);
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
        public async Task<List<LocationListDto>> GetAllLocations(TokenPlusId tokenPlusId)
        {
            
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _getAllLocation);
                using (var httpClient = new HttpClient())
                {
                    //StringContent content = new StringContent(JsonConvert.SerializeObject(baseFilter), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.GetAsync(api))
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
            
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _getSingleLocation);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(locationId), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(api, content))
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
        public async Task<Guid> SendLocationDto(TokenPlusId tokenPlusId, LocationDto locationDto)
        {
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _sendLocation);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(locationDto), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(api, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var locationId = JsonConvert.DeserializeObject<Guid>(apiResponse);
                            return locationId;
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
        public async Task<bool> DeleteRange (TokenPlusId tokenPlusId, IEnumerable<Guid> locationIds)
        {
            try
            {
                var api = _nfMapService.GetEndPoint(_udrfName, _deleteRange);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(locationIds), Encoding.UTF8, "application/json");
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
