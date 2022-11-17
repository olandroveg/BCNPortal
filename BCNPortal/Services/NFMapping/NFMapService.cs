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

                    existent = ConvertNFmapp(nFMapping, existent);
                    _context.NFmapping.Update(existent);
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
        public NFmapping GetNFMappingByNFName(string name)
        {
            try
            {
                return _context.NFmapping.FirstOrDefault(x => x.NF == name);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            

        }
        public Guid GetNFMapIdByName (string name)
        {
            return GetNFMappingByNFName(name) != null ? GetNFMappingByNFName(name).NFId : Guid.Empty;
        }
        private NFmapping ConvertNFmapp (NFmapping source, NFmapping destiny)
        {
            destiny.NFId = source.NFId;
            destiny.NFbaseAdd = source.NFbaseAdd;
            destiny.Version = source.Version;
            destiny.Available = source.Available;
            destiny.Priority = source.Priority;
            destiny.DateTime = source.DateTime;
            destiny.Apis = source.Apis;
            return destiny;
        }
        public string GetEndPoint(string nfName, string apiReference)
        {
            try
            {
                var nf = _context.NFmapping.Include(x => x.Apis).FirstOrDefault(x => x.NF == nfName);
                if (nf == null)
                    throw new Exception("No NF found");
                var api = nf.Apis.FirstOrDefault(x => x.ServiceName == apiReference);
                if (api == null)
                    throw new Exception("No Api found");
                return nf.NFbaseAdd + api.ServiceApi;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}
