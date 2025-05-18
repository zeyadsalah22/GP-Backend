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
        Task<int> CreateAsync(Application application);
        Task<bool> UpdateAsync(Application application);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
} 