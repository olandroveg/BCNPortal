using BCNPortal.Models;

namespace BCNPortal.Services.ApiMapping
{
    public interface IApiMapService
    {
        public void DeleteRange(IEnumerable<APImapping> apiMappings);
    }
}
