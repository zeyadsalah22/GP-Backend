using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _companyRepository.GetAllAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _companyRepository.GetByIdAsync(id);
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            company.CreatedAt = DateTime.UtcNow;
            company.UpdatedAt = DateTime.UtcNow;
            return await _companyRepository.CreateAsync(company);
        }

        public async Task<bool> UpdateCompanyAsync(Company company)
        {
            company.UpdatedAt = DateTime.UtcNow;
            return await _companyRepository.UpdateAsync(company);
        }

        public async Task<bool> DeleteCompanyAsync(int id)
        {
            return await _companyRepository.DeleteAsync(id);
        }
    }
} 