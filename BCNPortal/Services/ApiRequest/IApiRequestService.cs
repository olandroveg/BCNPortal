using BCNPortal.Models;

namespace BCNPortal.Services.ApiRequest
{
    public interface IApiRequestService
    {
        public Task<TokenApi> RequestToken(string username, string password);
    }
}
