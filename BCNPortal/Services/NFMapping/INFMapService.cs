using BCNPortal.Models;

namespace BCNPortal.Services.NFMapping
{
    public interface INFMapService
    {
        public void DeleteByName(NFmapping nFmapping);
        public Task<Guid> AddOrUpdate(NFmapping nFMapping);
        public NFmapping GetNFMappingByNFName(string name);
        public Guid GetNFMapIdByName(string name);
        public string GetEndPoint(string nfName, string apiReference);
    }
}
