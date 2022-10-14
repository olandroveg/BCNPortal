using BCNPortal.Models;

namespace BCNPortal.Services.BcnUser
{
    public interface IBcnUserService
    {
        public Task<Guid> AddOrUpdate(BcnUserAccount bcnUserAccount);
        public BcnUserAccount GetBcnUserAccountByUserPortalId(Guid id);
    }
}
