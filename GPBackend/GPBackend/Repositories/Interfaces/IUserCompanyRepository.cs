using GPBackend.Models;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.UserCompany;

namespace GPBackend.Repositories.Interfaces
{
    public interface IUserCompanyRepository
    {
        Task<PagedResult<UserCompany>> GetFilteredAsync(UserCompanyQueryDto queryDto);
        Task<IEnumerable<UserCompany>> GetAllAsync();
        Task<IEnumerable<UserCompany>> GetByUserIdAsync(int userId);
        Task<IEnumerable<UserCompany>> GetByCompanyIdAsync(int companyId);
        Task<UserCompany?> GetByIdAsync(int userId, int companyId);
        Task<UserCompany> CreateAsync(UserCompany userCompany);
        Task<bool> UpdateAsync(UserCompany userCompany);
        Task<bool> DeleteAsync(int userId, int companyId);
        Task<bool> UserCompanyExistsAsync(int userId, int companyId);
    }
} 