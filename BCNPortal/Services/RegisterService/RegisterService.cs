using BCNPortal.DTO.Portal;
using BCNPortal.Models;
using BCNPortal.Services.IdNRF;
using BCNPortal.Utility;
using Newtonsoft.Json;
using System.Text;

namespace BCNPortal.Services.RegisterService
{
    public class RegisterService : IRegisterService
    {
        private readonly string _nrfAddress;
        private readonly string _registerApi;
        private readonly IIdNRFService _idNRFService;
        private readonly string _portalLocationName;
        private readonly string _portalLocationLat;
        private readonly string _portalLocationLong;
        private readonly string _portalBusyIndex;
        private readonly string _portalName;

        public RegisterService(IIdNRFService idNRFService)
        {
            _nrfAddress = StaticConfigurationManager.AppSetting["ApiAddress:NRF_Address"];
            _registerApi = StaticConfigurationManager.AppSetting["ApiAddress:NRF_registerNF"];
            _portalLocationName = StaticConfigurationManager.AppSetting["ThisPortalInfo:NFLocation:Name"];
            _portalLocationLat = StaticConfigurationManager.AppSetting["ThisPortalInfo:NFLocation:Latitude"];
            _portalLocationLong = StaticConfigurationManager.AppSetting["ThisPortalInfo:NFLocation:Longitude"];
            _portalBusyIndex = StaticConfigurationManager.AppSetting["ThisPortalInfo:BusyIndex"];
            _portalName = StaticConfigurationManager.AppSetting["ThisPortalInfo:PortalName"];
            _idNRFService = idNRFService;
        }
        public async Task<Guid> RegisterPortal(TokenPlusId tokenPlusId)
        {
            try
            {
                var portalDto = ConformRegisterDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(portalDto), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenPlusId.Token);
                    using (var response = await httpClient.PostAsync(_nrfAddress + _registerApi, content))
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
        private PortalRegisterDto ConformRegisterDto()
        {
            return new PortalRegisterDto
            {
                Id = _idNRFService.GetNF_IDinNRF().Id ?? Guid.Empty,
                Location = new DTO.Location.IncomeLocationDto
                {
                    Name = _portalLocationName,
                    Latitude = double.Parse(_portalLocationLat),
                    Longitude = double.Parse(_portalLocationLong)

                },
                BusyIndex = float.Parse(_portalBusyIndex),
                PortalName = _portalName
            };
        }
    }
}
