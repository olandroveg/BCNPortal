using System;
using BCNPortal.Dto.BcNodeContent;
using BCNPortal.Dto.Service;
using BCNPortal.DTO;
using BCNPortal.DTO.Filter;
using BCNPortal.Models;

namespace BCNPortal.Services.BcNodeContent
{
    public interface IBcNodeContentService
    {
        Task<List<BcNodeContentDto>> GetBcNodeContentDto(TokenPlusId tokenPlusId, BaseFilter baseFilter);
        Task<List<BaseDTO>> GetAllContents(TokenPlusId tokenPlusId, string bcNodeId);
        Task<Guid> SendBcNodeContentDto(TokenPlusId tokenPlusId, BcNodeContentDto bcNodeContentDto);
        Task<List<ServiceDto>> GetAllServicesDto(TokenPlusId tokenPlusId);
        Task<BcNodeContentDto> GetBcNodeContentDto(TokenPlusId tokenPlusId, Guid bcNodeContentId);
    }
}

