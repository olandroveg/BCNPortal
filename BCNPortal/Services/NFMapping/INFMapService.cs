using BCNPortal.Models;

namespace BCNPortal.Services.NFMapping
{
    public interface INFMapService
    {
        public void DeleteByName(NFmapping nFmapping);
        public Task<Guid> AddOrUpdate(NFmapping nFMapping);
    }
}
