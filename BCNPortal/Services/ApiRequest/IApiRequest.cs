using BCNPortal.Models;

namespace BCNPortal.Services.ApiRequest
{
    public interface IApiRequest
    {
        public Task<TokenApi> RequestToken(string username, string password);
    }
}
