using GPBackend.Models;
using GPBackend.DTOs.Company;
using GPBackend.DTOs.Common;

namespace GPBackend.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<PagedResult<CompanyResponseDto>> GetFilteredCompaniesAsync(CompanyQueryDto queryDto);
        Task<IEnumerable<CompanyResponseDto>> GetAllCompaniesAsync();
        Task<CompanyResponseDto?> GetCompanyByIdAsync(int id);
        Task<CompanyResponseDto> CreateCompanyAsync(CompanyCreateDto companyDto);
        Task<bool> UpdateCompanyAsync(int id, CompanyUpdateDto companyDto);
        Task<bool> DeleteCompanyAsync(int id);
        Task<int> BulkDeleteCompaniesAsync(IEnumerable<int> ids);
    }
} 