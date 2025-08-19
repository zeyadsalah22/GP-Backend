using GPBackend.DTOs.Application;
using GPBackend.DTOs.Common;
using GPBackend.Models;

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
    }
} 