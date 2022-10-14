using BCNPortal.Data;
using BCNPortal.Models;

namespace BCNPortal.Services.BcnUser
{
    public class BcnUserService : IBcnUserService
    {
        private readonly ApplicationDbContext _context;
        public BcnUserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> AddOrUpdate (BcnUserAccount bcnUserAccount)
        {
            if (string.IsNullOrEmpty(bcnUserAccount.BcnUsername) || string.IsNullOrEmpty(bcnUserAccount.BcnPassword) || Guid.Empty == bcnUserAccount.PortalUserId)
                throw new ArgumentNullException("fields cannot be null");
            if (bcnUserAccount.Id == Guid.Empty)
                await _context.BcnUserAccount.AddAsync(bcnUserAccount);
            else
                _context.BcnUserAccount.Update(bcnUserAccount);
            await _context.SaveChangesAsync();
            return bcnUserAccount.Id;
        }
        public BcnUserAccount GetBcnUserAccountByUserPortalId (Guid id)
        {
            return _context.BcnUserAccount.Where(x => x.PortalUserId == id).FirstOrDefault();
        }
    }
}
