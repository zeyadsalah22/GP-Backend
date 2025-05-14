using GPBackend.Models;
using GPBackend.DTOs.Company;
using GPBackend.DTOs.Common;

namespace GPBackend.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<PagedResult<Company>> GetFilteredAsync(CompanyQueryDto queryDto);
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);
        Task<Company> CreateAsync(Company company);
        Task<bool> UpdateAsync(Company company);
        Task<bool> DeleteAsync(int id);
    }
} 