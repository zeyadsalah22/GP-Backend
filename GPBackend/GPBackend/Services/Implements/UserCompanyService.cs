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
        private readonly ICompanyRepository _companyRepository;

        public UserCompanyService(IUserCompanyRepository userCompanyRepository, ICompanyRepository companyRepository, IMapper mapper)
        {
            _userCompanyRepository = userCompanyRepository;
            _companyRepository = companyRepository;
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

        public async Task<IEnumerable<UserCompanyResponseDto>> GetUserCompaniesByCompanyIdAsync(int companyId, int userId)
        {
            // Filtering to only include companies that belong to the authenticated user
            var userCompanies = await _userCompanyRepository.GetByCompanyIdAsync(companyId);
            var userCompaniesForAuthenticatedUser = userCompanies.Where(uc => uc.UserId == userId);
            return _mapper.Map<IEnumerable<UserCompanyResponseDto>>(userCompaniesForAuthenticatedUser);
        }

        public async Task<UserCompanyResponseDto?> GetUserCompanyByIdAsync(int userId, int companyId)
        {
            var userCompany = await _userCompanyRepository.GetByIdAsync(userId, companyId);
            return userCompany != null ? _mapper.Map<UserCompanyResponseDto>(userCompany) : null;
        }

        public async Task<UserCompanyResponseDto> CreateUserCompanyAsync(UserCompanyCreateDto userCompanyDto)
        {
            // Validate target company exists and is not deleted
            var company = await _companyRepository.GetByIdAsync(userCompanyDto.CompanyId);
            if (company == null || company.IsDeleted)
            {
                throw new InvalidOperationException("Invalid companyId. Company does not exist or is deleted.");
            }

            // Ensure not duplicate
            var existingAny = await _userCompanyRepository.GetIncludingDeletedAsync(userCompanyDto.UserId, userCompanyDto.CompanyId);
            if (existingAny != null)
            {
                if (!existingAny.IsDeleted)
                {
                    throw new InvalidOperationException("UserCompany already exists for this user and company.");
                }
                // Revive soft-deleted record
                existingAny.PersonalNotes = userCompanyDto.PersonalNotes;
                existingAny.InterestLevel = userCompanyDto.InterestLevel;
                existingAny.Favorite = userCompanyDto.Favorite;
                existingAny.IsDeleted = false;
                existingAny.UpdatedAt = DateTime.UtcNow;
                await _userCompanyRepository.UpdateAsync(existingAny);
                if (userCompanyDto.Tags != null)
                {
                    await _userCompanyRepository.ReplaceTagsAsync(existingAny.UserId, existingAny.CompanyId, userCompanyDto.Tags);
                }
                var revived = await _userCompanyRepository.GetByIdAsync(existingAny.UserId, existingAny.CompanyId);
                return _mapper.Map<UserCompanyResponseDto>(revived!);
            }

            var userCompany = _mapper.Map<UserCompany>(userCompanyDto);
            var createdUserCompany = await _userCompanyRepository.CreateAsync(userCompany);
            // Replace tags if provided (ensure tag rows exist)
            if (userCompanyDto.Tags != null)
            {
                await _userCompanyRepository.ReplaceTagsAsync(createdUserCompany.UserId, createdUserCompany.CompanyId, userCompanyDto.Tags);
            }

            // Reload with relations for accurate response
            var reloaded = await _userCompanyRepository.GetByIdAsync(createdUserCompany.UserId, createdUserCompany.CompanyId);
            return _mapper.Map<UserCompanyResponseDto>(reloaded!);
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
            var updated = await _userCompanyRepository.UpdateAsync(existingUserCompany);
            if (!updated) return false;

            if (userCompanyDto.Tags != null)
            {
                await _userCompanyRepository.ReplaceTagsAsync(userId, companyId, userCompanyDto.Tags);
            }
            return true;
        }

        public async Task<bool> DeleteUserCompanyAsync(int userId, int companyId)
        {
            return await _userCompanyRepository.DeleteAsync(userId, companyId);
        }
    }
} 