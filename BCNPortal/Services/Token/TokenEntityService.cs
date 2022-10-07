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
            var formerToken = _context.Token.AsEnumerable().FirstOrDefault();
            if (formerToken != null && formerToken.Id != Guid.Empty)
            {
                formerToken.Value = token.Value;
                formerToken.DateTime = token.DateTime;
                _context.Token.Update(formerToken);
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
        public bool TokenAvailability()
        {
            var formerToken = _context.Token.AsEnumerable().FirstOrDefault();
            if (formerToken != null && formerToken.Id != Guid.Empty && (((DateTime.Now - formerToken.DateTime).TotalMinutes)< 30))
                return true;
            return false;
        }
        public string GetToken()
        {
            return _context.Token.Count() > 0 ? _context.Token.AsEnumerable().FirstOrDefault().Value : string.Empty;
        }

    }
}
