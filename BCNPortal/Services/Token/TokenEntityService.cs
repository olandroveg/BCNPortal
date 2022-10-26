using BCNPortal.Data;
using BCNPortal.Models;

namespace BCNPortal.Services.Token
{
    public class TokenEntityService : ITokenEntityService
    {
        private readonly ApplicationDbContext _context;

        public TokenEntityService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BCNPortal.Models.Token> AddOrUpdate(BCNPortal.Models.Token token)
        {
            if (string.IsNullOrEmpty(token.Value))
                throw new ArgumentNullException("fields cannot be null");
            var formerToken = _context.Token.FirstOrDefault(x=> x.PortalUserId == token.PortalUserId);
            if (formerToken != null && formerToken.Id != Guid.Empty)
            {
                formerToken.Value = token.Value;
                formerToken.DateTime = token.DateTime;
                _context.Token.Update(formerToken);
                await _context.SaveChangesAsync();
                return formerToken;
            }
            else
                await _context.Token.AddAsync(token);
            await _context.SaveChangesAsync();
            return token;
        }
        public void Delete(BCNPortal.Models.Token token)
        {
            if (token.Id == Guid.Empty)
                throw new ArgumentNullException("token does not exist");
            _context.Token.Remove(token);
        }
        public bool TokenAvailability(Guid userId)
        {
            // set token renewal as 30 min interval.
            var formerToken = _context.Token.FirstOrDefault(x=> x.PortalUserId == userId);
            if (formerToken != null && formerToken.Id != Guid.Empty && (((DateTime.Now - formerToken.DateTime).TotalMinutes)< 30))
                return true;
            return false;
        }
        public TokenPlusId GetToken(Guid userId)
        {
            var token = _context.Token.Where(x => x.PortalUserId == userId).Count() > 0 ? _context.Token.FirstOrDefault(x=> x.PortalUserId == userId) : null;
            if (token != null)
                return new TokenPlusId
                {
                    Token = token.Value,
                    BcnUserId = token.BcnUserId

                };
            return null;
        }

    }
}
