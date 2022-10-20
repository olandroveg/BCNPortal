using BCNPortal.DTO.BcNode;
using BCNPortal.DTO.Filter;
using BCNPortal.Models;
using BCNPortal.Utility;
using Newtonsoft.Json;
using System.Text;

namespace BCNPortal.Services.BcNodeRqst
{
    public class BcNodeService : IBcNodeService
    {
        private readonly string _udrfAddress; 
        private readonly string _getBcNodeApi;
        
        public BcNodeService()
        {
            _udrfAddress = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_Address"];
            _getBcNodeApi = StaticConfigurationManager.AppSetting["ApiAddress:UDRF_getBcNodes"];
        }

        public async Task <BcNodeDto> GetBcNodes(TokenPlusId tokenPlusId, BaseFilter baseFilter)
        {
            var bcNodesDto = new BcNodeApiDto();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(baseFilter), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                using (var response = await httpClient.PostAsync(_udrfAddress + _getBcNodeApi, content))
            }
        }
    }
}
