﻿using System;
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
                            var apis = JsonConvert.DeserializeObject<List<ServicesAnswerDto>>(apiResponse);
                            if (apis != null && apis.Count() > 0)
                                ProcessNFMapp(apis, targetNFName);
                            else
                                throw new Exception("no apis found");
                            
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
        private void ProcessNFMapp (List<ServicesAnswerDto> apis, string name)
        {
            var nfMapp = new NFmapping
            {
                Id = Guid.Empty,
                NFId = apis.FirstOrDefault().NFId,
                NF = name,
                Version = "",
                Available = true,
                Priority = 1,
                DateTime = DateTime.Now,
                Apis = apis.Select(x => new APImapping
                {
                    Id = Guid.Empty,
                    ServiceApi = x.ServicesAPI,
                    ServiceName = x.Description
                }).ToList()
            };
            var id = _iNFMapService.AddOrUpdate(nfMapp);
        }
    }
}

