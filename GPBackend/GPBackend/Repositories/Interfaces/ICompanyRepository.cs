using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int id);
        Task<Company> CreateAsync(Company company);
        Task<bool> UpdateAsync(Company company);
        Task<bool> DeleteAsync(int id);
    }
} 