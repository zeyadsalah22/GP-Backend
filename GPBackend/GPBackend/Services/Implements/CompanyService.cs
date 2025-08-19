using AutoMapper;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Company;
using GPBackend.DTOs.Common;

namespace GPBackend.Services.Implements
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<CompanyResponseDto>> GetFilteredCompaniesAsync(CompanyQueryDto queryDto)
        {
            var pagedCompanies = await _companyRepository.GetFilteredAsync(queryDto);

            // Map the result items to DTOs
            var dtoItems = _mapper.Map<List<CompanyResponseDto>>(pagedCompanies.Items);

            // Create a new paged result with the mapped items
            return new PagedResult<CompanyResponseDto>
            {
                Items = dtoItems,
                PageNumber = pagedCompanies.PageNumber,
                PageSize = pagedCompanies.PageSize,
                TotalCount = pagedCompanies.TotalCount
            };
        }

        public async Task<IEnumerable<CompanyResponseDto>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyResponseDto>>(companies);
        }

        public async Task<CompanyResponseDto?> GetCompanyByIdAsync(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            return company != null ? _mapper.Map<CompanyResponseDto>(company) : null;
        }

        public async Task<CompanyResponseDto> CreateCompanyAsync(CompanyCreateDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            var createdCompany = await _companyRepository.CreateAsync(company);
            return _mapper.Map<CompanyResponseDto>(createdCompany);
        }

        public async Task<bool> UpdateCompanyAsync(int id, CompanyUpdateDto companyDto)
        {
            var existingCompany = await _companyRepository.GetByIdAsync(id);
            if (existingCompany == null)
            {
                return false;
            }

            _mapper.Map(companyDto, existingCompany);
            existingCompany.UpdatedAt = DateTime.UtcNow;

            return await _companyRepository.UpdateAsync(existingCompany);
        }

        public async Task<bool> DeleteCompanyAsync(int id)
        {
            return await _companyRepository.DeleteAsync(id);
        }

        public async Task<int> BulkDeleteCompaniesAsync(IEnumerable<int> ids)
        {
            return await _companyRepository.BulkSoftDeleteAsync(ids);
        }
    }
}