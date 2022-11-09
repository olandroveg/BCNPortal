using BCNPortal.DTO.Portal;
using BCNPortal.Models;

namespace BCNPortal.Services.RegisterService
{
    public interface IRegisterService
    {
        public Task<Guid> RegisterPortal(TokenPlusId tokenPlusId);
    }
}
