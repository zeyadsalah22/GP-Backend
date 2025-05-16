using AutoMapper;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.UserCompany;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Services.Implements
{
    public class UserCompanyService : IUserCompanyService
    {
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly IMapper _mapper;

        public UserCompanyService(IUserCompanyRepository userCompanyRepository, IMapper mapper)
        {
            _userCompanyRepository = userCompanyRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserCompanyResponseDto>> GetFilteredUserCompaniesAsync(UserCompanyQueryDto queryDto)
        {
            var pagedUserCompanies = await _userCompanyRepository.GetFilteredAsync(queryDto);

            // Map the result items to DTOs
            var dtoItems = _mapper.Map<List<UserCompanyResponseDto>>(pagedUserCompanies.Items);

            // Create a new paged result with the mapped items
            return new PagedResult<UserCompanyResponseDto>
            {
                Items = dtoItems,
                PageNumber = pagedUserCompanies.PageNumber,
                PageSize = pagedUserCompanies.PageSize,
                TotalCount = pagedUserCompanies.TotalCount
            };
        }

        public async Task<IEnumerable<UserCompanyResponseDto>> GetAllUserCompaniesAsync()
        {
            var userCompanies = await _userCompanyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserCompanyResponseDto>>(userCompanies);
        }

        public async Task<IEnumerable<UserCompanyResponseDto>> GetUserCompaniesByUserIdAsync(int userId)
        {
            var userCompanies = await _userCompanyRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<UserCompanyResponseDto>>(userCompanies);
        }

        public async Task<IEnumerable<UserCompanyResponseDto>> GetUserCompaniesByCompanyIdAsync(int companyId)
        {
            var userCompanies = await _userCompanyRepository.GetByCompanyIdAsync(companyId);
            return _mapper.Map<IEnumerable<UserCompanyResponseDto>>(userCompanies);
        }

        public async Task<UserCompanyResponseDto?> GetUserCompanyByIdAsync(int userId, int companyId)
        {
            var userCompany = await _userCompanyRepository.GetByIdAsync(userId, companyId);
            return userCompany != null ? _mapper.Map<UserCompanyResponseDto>(userCompany) : null;
        }

        public async Task<UserCompanyResponseDto> CreateUserCompanyAsync(UserCompanyCreateDto userCompanyDto)
        {
            var userCompany = _mapper.Map<UserCompany>(userCompanyDto);
            var createdUserCompany = await _userCompanyRepository.CreateAsync(userCompany);
            
            // Load company details for the response
            await _userCompanyRepository.GetByIdAsync(createdUserCompany.UserId, createdUserCompany.CompanyId);
            
            return _mapper.Map<UserCompanyResponseDto>(createdUserCompany);
        }

        public async Task<bool> UpdateUserCompanyAsync(int userId, int companyId, UserCompanyUpdateDto userCompanyDto)
        {
            var existingUserCompany = await _userCompanyRepository.GetByIdAsync(userId, companyId);
            if (existingUserCompany == null)
            {
                return false;
            }

            _mapper.Map(userCompanyDto, existingUserCompany);
            existingUserCompany.UpdatedAt = DateTime.UtcNow;

            return await _userCompanyRepository.UpdateAsync(existingUserCompany);
        }

        public async Task<bool> DeleteUserCompanyAsync(int userId, int companyId)
        {
            return await _userCompanyRepository.DeleteAsync(userId, companyId);
        }
    }
} 