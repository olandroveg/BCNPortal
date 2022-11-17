using BCNPortal.Data;
using BCNPortal.Models;
using BCNPortal.Services.ApiMapping;
using Microsoft.EntityFrameworkCore;

namespace BCNPortal.Services.NFMapping
{
    public class NFMapService : INFMapService
    {
        private readonly ApplicationDbContext _context;
        private readonly IApiMapService _apiMapService;

        public NFMapService (ApplicationDbContext context, IApiMapService apiMapService)
        {
            _context = context;
            _apiMapService = apiMapService;
        }
        public async Task<Guid> AddOrUpdate (NFmapping nFMapping)
        {
            try
            {
                var existent = _context.NFmapping.Include (x=> x.Apis).FirstOrDefault(x => x.NF == nFMapping.NF);
                if (existent == null)
                    await _context.NFmapping.AddAsync(nFMapping);
                else
                {
                    if (existent.Apis != null && existent.Apis.Count()> 0)
                        _apiMapService.DeleteRange(existent.Apis);
                    _context.NFmapping.Update(nFMapping);
                }
                await _context.SaveChangesAsync();
                return nFMapping.Id;
                    
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Guid.Empty;
            }
        }
        public void DeleteByName (NFmapping nFmapping)
        {
            var existent = _context.NFmapping.FirstOrDefault(x => x.NF == nFmapping.NF);
            if (existent != null)
            {
                //_apiMapService.DeleteRange(existent.Apis);
                _context.NFmapping.Remove(existent);
                _context.SaveChanges();
            }
               
        }
    }
}
