using System;
using BCNPortal.Models;

namespace BCNPortal.Services.IdNRF
{
    public interface IIdNRFService
    {
        public IDinNRF GetNF_IDinNRF();
        public Task<Guid> AddOrUpdate(IDinNRF idInNRF);
    }
}

