using BCNPortal.Dto.Location;
using BCNPortal.DTO.Filter;
using BCNPortal.DTO.Location;
using BCNPortal.Models;

namespace BCNPortal.Services.Location
{
    public interface ILocationService
    {
        Task<List<LocationDto>> GetLocations(TokenPlusId tokenPlusId, BaseFilter baseFilter);
        Task<List<LocationListDto>> GetAllLocations(TokenPlusId tokenPlusId);
        Task<LocationDto> GetSingleLocation(TokenPlusId tokenPlusId, Guid locationId);
        Task<Guid> SendLocationDto(TokenPlusId tokenPlusId, LocationDto locationDto);
        Task<bool> DeleteRange(TokenPlusId tokenPlusId, IEnumerable<Guid> locationIds);
        
    }
}
