using BCNPortal.Dto.Content;
using BCNPortal.Dto.Service;
using BCNPortal.Models;

namespace BCNPortal.Services.Content
{
    public interface IContentService
    {
        Task<List<ContentTableDto>> GetAllContents(TokenPlusId tokenPlusId);
        Task<List<ServiceDto>> GetAllServices(TokenPlusId tokenPlusId);
        Task<ContentDto> GetSingleContent(TokenPlusId tokenPlusId, Guid contentId);
        Task<Guid> SendContentDto(TokenPlusId tokenPlusId, ContentDto contentDto);
        Task<bool> DeleteRange(TokenPlusId tokenPlusId, IEnumerable<Guid> contentIds);
    }
}
