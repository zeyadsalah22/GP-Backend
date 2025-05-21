using AutoMapper;
using GPBackend.DTOs.Application;
using GPBackend.DTOs.Common;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly IMapper _mapper;

        public ApplicationService(
            IApplicationRepository applicationRepository,
            IUserCompanyRepository userCompanyRepository,
            IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _userCompanyRepository = userCompanyRepository;
            _mapper = mapper;
        }

        public async Task<ApplicationResponseDto?> GetApplicationByIdAsync(int id, int userId)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            
            // Check if application exists and belongs to user
            if (application == null || application.UserId != userId)
            {
                return null;
            }
            
            var applicationDto = _mapper.Map<ApplicationResponseDto>(application);
            applicationDto.CompanyName = application.UserCompany?.Company?.Name ?? "Unknown Company";
            
            return applicationDto;
        }

        public async Task<PagedResult<ApplicationResponseDto>> GetFilteredApplicationsAsync(int userId, ApplicationQueryDto queryDto)
        {
            var pagedResult = await _applicationRepository.GetFilteredApplicationsAsync(userId, queryDto);
            
            var applicationDtos = _mapper.Map<List<ApplicationResponseDto>>(pagedResult.Items);
            
            // Set company names
            foreach (var dto in applicationDtos)
            {
                var application = pagedResult.Items.FirstOrDefault(a => a.ApplicationId == dto.ApplicationId);
                dto.CompanyName = application?.UserCompany?.Company?.Name ?? "Unknown Company";
            }
            
            return new PagedResult<ApplicationResponseDto>
            {
                Items = applicationDtos,
                TotalCount = pagedResult.TotalCount,
                PageSize = pagedResult.PageSize,
                PageNumber = pagedResult.PageNumber
            };
        }

        public async Task<IEnumerable<ApplicationResponseDto>> GetAllApplicationsByUserIdAsync(int userId)
        {
            var applications = await _applicationRepository.GetAllByUserIdAsync(userId);
            var applicationDtos = _mapper.Map<IEnumerable<ApplicationResponseDto>>(applications);
            
            // Set company names
            foreach (var dto in applicationDtos)
            {
                var application = applications.FirstOrDefault(a => a.ApplicationId == dto.ApplicationId);
                dto.CompanyName = application?.UserCompany?.Company?.Name ?? "Unknown Company";
            }
            
            return applicationDtos;
        }

        public async Task<ApplicationResponseDto> CreateApplicationAsync(int userId, ApplicationCreateDto createDto)
        {
            // Verify the UserCompany relation exists
            var userCompanyExists = await _userCompanyRepository.UserCompanyExistsAsync(userId, createDto.CompanyId);
            if (!userCompanyExists)
            {
                throw new InvalidOperationException("The specified user-company relationship does not exist");
            }
            
            // Create new application
            var application = _mapper.Map<Application>(createDto);
            application.UserId = userId;
            application.SubmissionDate = createDto.SubmissionDate ?? DateOnly.FromDateTime(DateTime.UtcNow);
            application.CreatedAt = DateTime.UtcNow;
            application.UpdatedAt = DateTime.UtcNow;
            
            // TODO: modify here to return the created object at once, not id

            // Save to database
            var applicationId = await _applicationRepository.CreateAsync(application);
            
            // Retrieve the created application
            var createdApplication = await _applicationRepository.GetByIdAsync(applicationId);
            if (createdApplication == null)
            {
                throw new InvalidOperationException("Failed to retrieve the created application");
            }
            
            // Map to DTO
            var applicationDto = _mapper.Map<ApplicationResponseDto>(createdApplication);
            applicationDto.CompanyName = createdApplication.UserCompany?.Company?.Name ?? "Unknown Company";
            
            return applicationDto;
        }

        public async Task<bool> UpdateApplicationAsync(int id, int userId, ApplicationUpdateDto updateDto)
        {
            // Get the existing application
            var application = await _applicationRepository.GetByIdAsync(id);
            if (application == null || application.UserId != userId)
            {
                return false;
            }
            
            // Update properties
            _mapper.Map(updateDto, application);
            application.UpdatedAt = DateTime.UtcNow;
            
            // Save changes
            return await _applicationRepository.UpdateAsync(application);
        }

        public async Task<bool> DeleteApplicationAsync(int id, int userId)
        {
            // Get the existing application
            var application = await _applicationRepository.GetByIdAsync(id);
            if (application == null || application.UserId != userId)
            {
                return false;
            }
            
            // Delete application
            return await _applicationRepository.DeleteAsync(id);
        }

        public async Task<bool> ApplicationExistsAsync(int id)
        {
            return await _applicationRepository.ExistsAsync(id);
        }
    }
} 