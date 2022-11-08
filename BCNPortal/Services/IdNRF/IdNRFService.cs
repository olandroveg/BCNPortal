using System;
using BCNPortal.Data;
using BCNPortal.Models;

namespace BCNPortal.Services.IdNRF
{
    public class IdNRFService : IIdNRFService
    {
        private readonly ApplicationDbContext _context;

        public IdNRFService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IDinNRF GetNF_IDinNRF()
        {
            var idInNRF = _context.IDinNRF;
            return idInNRF != null && idInNRF.Count() > 0 ? idInNRF.FirstOrDefault() : new IDinNRF { Id = Guid.Empty };
        }

        public async Task<Guid> AddOrUpdate(IDinNRF idInNRF)
        {
            if (idInNRF == null || idInNRF.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(idInNRF));
            var nfIdInNRF = GetNF_IDinNRF();
            if (nfIdInNRF.Id != Guid.Empty && idInNRF.Id != nfIdInNRF.Id)
            {
                _context.IDinNRF.Remove(nfIdInNRF);
                await _context.IDinNRF.AddAsync(idInNRF);
            }
            else if (nfIdInNRF.Id == Guid.Empty)
                await _context.IDinNRF.AddAsync(idInNRF);
            await _context.SaveChangesAsync();
            return idInNRF.Id ?? Guid.Empty;
        }
    }
}

