using GPBackend.DTOs.Common;
using GPBackend.DTOs.CompanyRequest;
using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface ICompanyRequestRepository
    {
        Task<int> CreateRequestAsync(CompanyRequest request);
        Task<CompanyRequest?> GetRequestByIdAsync(int requestId);
        Task<PagedResult<CompanyRequest>> GetFilteredRequestsAsync(CompanyRequestQueryDto queryDto);
        Task<IEnumerable<CompanyRequest>> GetUserRequestsAsync(int userId);
        Task<bool> UpdateRequestStatusAsync(int requestId, Models.Enums.CompanyRequestStatus status, int adminId, string? reason);
        Task<CompanyRequest?> CheckDuplicateRequestAsync(string companyName);
        Task<bool> RequestExistsAsync(int requestId);
    }
}

