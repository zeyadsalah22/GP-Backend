using GPBackend.DTOs.Application;
using GPBackend.DTOs.Common;
using GPBackend.Models;
using GPBackend.Models.Enums;

namespace GPBackend.Repositories.Interfaces
{
    public interface IApplicationRepository
    {
        Task<Application?> GetByIdAsync(int id);
        Task<PagedResult<Application>> GetFilteredApplicationsAsync(int userId, ApplicationQueryDto queryDto);
        Task<IEnumerable<Application>> GetAllByUserIdAsync(int userId);

        // TODO: Modify here to return the object, not the ID
        Task<int> CreateAsync(Application application);
        Task<bool> UpdateAsync(Application application);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> BulkSoftDeleteAsync(IEnumerable<int> ids, int userId);

        Task<bool> UpsertStageHistoryAsync(int applicationId, ApplicationStage stage, DateOnly reachedDate, string? note = null);
    }
} 