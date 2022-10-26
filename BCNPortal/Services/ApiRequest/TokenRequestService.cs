using BCNPortal.Models;
using BCNPortal.Services.Token;
using BCNPortal.Utility;
using Newtonsoft.Json;
using System.Text;

namespace BCNPortal.Services.ApiRequest
{
    public class TokenRequestService : ITokenRequestService
    {
        private string _aafAddress;
        private string _tokenApi;
        private readonly ITokenEntityService _tokenEntityService;
        
        public TokenRequestService(ITokenEntityService tokenEntityService)
        {
            _aafAddress = StaticConfigurationManager.AppSetting["ApiAddress:AAF_Address"];
            _tokenApi = StaticConfigurationManager.AppSetting["ApiAddress:AAF_getToken"];
            _tokenEntityService = tokenEntityService;
        }
        private async Task <TokenApi> RequestToken(string username, string password, Guid userId)
        {
            var tokenApi = new TokenApi();
            try
            {
                var dataObj = new TokenRqst(username, password);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(dataObj), Encoding.UTF8, "application/json");
                    using ( var response = await httpClient.PostAsync(_aafAddress + _tokenApi, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            tokenApi = JsonConvert.DeserializeObject<TokenApi>(apiResponse);
                            if (tokenApi.status == "Success")
                            {
                                var token = new BCNPortal.Models.Token
                                {
                                    Id = Guid.Empty,
                                    Value = tokenApi.token,
                                    DateTime = DateTime.Now,
                                    BcnUserId = tokenApi.bcnUserId,
                                    PortalUserId = userId
                                };
                                await _tokenEntityService.AddOrUpdate(token);
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
        public async Task<TokenPlusId> ManageToken(string username, string password, Guid userId)
        {

            TokenPlusId token = new TokenPlusId();
            if (_tokenEntityService.TokenAvailability(userId))
                token = _tokenEntityService.GetToken(userId);
            else
            {
                var tokenApi = await RequestToken(username, password, userId);
                if (tokenApi.status == "Success")
                {
                    token.Token = tokenApi.token;
                    token.BcnUserId = token.BcnUserId;
                }
                    
            }
            return token;
             
        }
    }
}
