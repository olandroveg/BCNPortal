using BCNPortal.Dto.Content;
using BCNPortal.Models;

namespace BCNPortal.Services.Content
{
    public interface IContentService
    {
        Task<List<ContentTableDto>> GetAllContents(TokenPlusId tokenPlusId);
    }
}
