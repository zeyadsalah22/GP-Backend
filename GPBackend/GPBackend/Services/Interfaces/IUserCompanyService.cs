using GPBackend.DTOs.Common;
using GPBackend.DTOs.UserCompany;

namespace GPBackend.Services.Interfaces
{
    public interface IUserCompanyService
    {
        Task<PagedResult<UserCompanyResponseDto>> GetFilteredUserCompaniesAsync(UserCompanyQueryDto queryDto);
        Task<IEnumerable<UserCompanyResponseDto>> GetAllUserCompaniesAsync();
        Task<IEnumerable<UserCompanyResponseDto>> GetUserCompaniesByUserIdAsync(int userId);
        Task<IEnumerable<UserCompanyResponseDto>> GetUserCompaniesByCompanyIdAsync(int companyId);
        Task<UserCompanyResponseDto?> GetUserCompanyByIdAsync(int userId, int companyId);
        Task<UserCompanyResponseDto> CreateUserCompanyAsync(UserCompanyCreateDto userCompanyDto);
        Task<bool> UpdateUserCompanyAsync(int userId, int companyId, UserCompanyUpdateDto userCompanyDto);
        Task<bool> DeleteUserCompanyAsync(int userId, int companyId);
    }
} 