using BCNPortal.DTO.BcNode;
using BCNPortal.DTO.Filter;
using BCNPortal.Models;

namespace BCNPortal.Services.BcNodeRqst
{
    public interface IBcNodeService
    {
        Task<List<BcNodeDto>> GetBcNodes(TokenPlusId tokenPlusId, BaseFilter baseFilter);
    }
}
