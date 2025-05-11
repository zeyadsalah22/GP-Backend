using GPBackend.Models;

namespace GPBackend.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(int id);
        Task<Company> CreateCompanyAsync(Company company);
        Task<bool> UpdateCompanyAsync(Company company);
        Task<bool> DeleteCompanyAsync(int id);
    }
} 