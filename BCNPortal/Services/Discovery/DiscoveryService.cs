using System;
using System.Text;
using BCNPortal.DTO.Portal;
using BCNPortal.Models;
using BCNPortal.Services.IdNRF;
using BCNPortal.Services.NFMapping;
using BCNPortal.Utility;
using Newtonsoft.Json;

namespace BCNPortal.Services.Discovery
{
    public class DiscoveryService : IDiscoveryService
    {
        private readonly string _nrfAddress;
        private readonly string _discovery;
        private readonly IIdNRFService _idNRFService;
        private readonly INFMapService _iNFMapService;

        public DiscoveryService(IIdNRFService idNRFService, INFMapService iNFMapService)
        {
            _nrfAddress = StaticConfigurationManager.AppSetting["ApiAddress:NRF_Address"];
            _discovery = StaticConfigurationManager.AppSetting["ApiAddress:NRF_discovery"];
            _iNFMapService = iNFMapService;
            _idNRFService = idNRFService;
        }
        public async Task DiscoverAllApiOfNF (TokenPlusId tokenPlusId, string targetNFName, Guid targetNFId)
        {
            try
            {
                var portalId = _idNRFService.GetNF_IDinNRF().Id ?? Guid.Empty;
                var portalDisc = new DiscoverRqstDto(portalId, targetNFName, targetNFId);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(portalDisc), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(_nrfAddress + _discovery, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var apis = JsonConvert.DeserializeObject<ServicesAnswerDto>(apiResponse);
                            var test = 1;
                            
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

