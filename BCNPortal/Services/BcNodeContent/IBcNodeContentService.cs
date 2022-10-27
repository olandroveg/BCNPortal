using System;
using BCNPortal.Dto.BcNodeContent;
using BCNPortal.DTO.Filter;
using BCNPortal.Models;

namespace BCNPortal.Services.BcNodeContent
{
    public interface IBcNodeContentService
    {
        Task<List<BcNodeContentDto>> GetBcNodeContentDto(TokenPlusId tokenPlusId, BaseFilter baseFilter);
    }
}

