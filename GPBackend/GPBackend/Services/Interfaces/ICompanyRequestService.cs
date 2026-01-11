using GPBackend.DTOs.Common;
using GPBackend.DTOs.CompanyRequest;

namespace GPBackend.Services.Interfaces
{
    public interface ICompanyRequestService
    {
        Task<CompanyRequestResponseDto> CreateRequestAsync(int userId, CompanyRequestCreateDto dto);
        Task<CompanyRequestResponseDto?> GetRequestByIdAsync(int requestId);
        Task<PagedResult<CompanyRequestResponseDto>> GetFilteredRequestsAsync(CompanyRequestQueryDto queryDto);
        Task<IEnumerable<CompanyRequestResponseDto>> GetUserRequestsAsync(int userId);
        Task<bool> ApproveRequestAsync(int requestId, int adminId);
        Task<bool> RejectRequestAsync(int requestId, int adminId, string reason);
    }
}

