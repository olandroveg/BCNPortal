using BCNPortal.DTO.Filter;
using BCNPortal.DTO.Location;
using BCNPortal.Models;

namespace BCNPortal.Services.Location
{
    public interface ILocationService
    {
        Task<List<LocationDto>> GetLocations(TokenPlusId tokenPlusId);
    }
}
