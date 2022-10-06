using BCNPortal.Models;
using BCNPortal.Utility;
using Newtonsoft.Json;
using System.Text;

namespace BCNPortal.Services.ApiRequest
{
    public class ApiRequestService : IApiRequestService
    {
        private string _aafAddress;
        private string _tokenApi;
        public ApiRequestService()
        {
            _aafAddress = StaticConfigurationManager.AppSetting["ApiAddress:AAF_Address"];
            _tokenApi = StaticConfigurationManager.AppSetting["ApiAddress:AAF_getToken"];
        }
        public async Task <TokenApi> RequestToken(string username, string password)
        {
            var tokenApi = new TokenApi();
            try
            {
                var dataObj = new TokenApi.TokenBody(username, password);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(dataObj), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(_aafAddress + _tokenApi, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            tokenApi = JsonConvert.DeserializeObject<TokenApi>(apiResponse);
                            if (tokenApi.status == "Success")
                            {
                                
                                return tokenApi;
                            }

                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            tokenApi.status = "unauthorized";
                    }
                    return tokenApi;
                }
            }
            catch (Exception) 
            {
                tokenApi.status = "exception";
                return tokenApi;
            }
        }
    }
}
