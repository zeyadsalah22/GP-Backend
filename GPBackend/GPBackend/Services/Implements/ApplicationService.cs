using AutoMapper;
using GPBackend.DTOs.Application;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.Employee;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using GPBackend.Models.Enums;

namespace GPBackend.Services.Implements
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public ApplicationService(
            IApplicationRepository applicationRepository,
            IUserCompanyRepository userCompanyRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _userCompanyRepository = userCompanyRepository;
            _employeeRepository = employeeRepository;
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
            applicationDto.Company = _mapper.Map<DTOs.Company.CompanyResponseDto>(application.UserCompany?.Company);
            
            // Map contacted employees
            if (application.ApplicationEmployees != null && application.ApplicationEmployees.Any())
            {
                applicationDto.ContactedEmployees = _mapper.Map<List<EmployeeDto>>(
                    application.ApplicationEmployees.Select(ae => ae.Employee).ToList()
                );
            }

            // Sort timeline ascending by date
            if (applicationDto.Timeline != null && applicationDto.Timeline.Count > 0)
            {
                applicationDto.Timeline = applicationDto.Timeline
                    .OrderBy(t => t.Date)
                    .ToList();
            }
            
            return applicationDto;
        }

        public async Task<PagedResult<ApplicationResponseDto>> GetFilteredApplicationsAsync(int userId, ApplicationQueryDto queryDto)
        {
            var pagedResult = await _applicationRepository.GetFilteredApplicationsAsync(userId, queryDto);
            
            var applicationDtos = _mapper.Map<List<ApplicationResponseDto>>(pagedResult.Items);
            
            // Set company details and contacted employees
            foreach (var dto in applicationDtos)
            {
                var application = pagedResult.Items.FirstOrDefault(a => a.ApplicationId == dto.ApplicationId);
                if (application != null)
                {
                    dto.CompanyName = application.UserCompany?.Company?.Name ?? "Unknown Company";
                    dto.Company = _mapper.Map<DTOs.Company.CompanyResponseDto>(application.UserCompany?.Company);
                    
                    // Map contacted employees
                    if (application.ApplicationEmployees != null && application.ApplicationEmployees.Any())
                    {
                        dto.ContactedEmployees = _mapper.Map<List<EmployeeDto>>(
                            application.ApplicationEmployees.Select(ae => ae.Employee).ToList()
                        );
                    }
                }
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
            
            // Set company details and contacted employees
            foreach (var dto in applicationDtos)
            {
                var application = applications.FirstOrDefault(a => a.ApplicationId == dto.ApplicationId);
                if (application != null)
                {
                    dto.CompanyName = application.UserCompany?.Company?.Name ?? "Unknown Company";
                    dto.Company = _mapper.Map<DTOs.Company.CompanyResponseDto>(application.UserCompany?.Company);
                    
                    // Map contacted employees
                    if (application.ApplicationEmployees != null && application.ApplicationEmployees.Any())
                    {
                        dto.ContactedEmployees = _mapper.Map<List<EmployeeDto>>(
                            application.ApplicationEmployees.Select(ae => ae.Employee).ToList()
                        );
                    }
                }
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
            
            // Validate contacted employees if provided
            if (createDto.ContactedEmployeeIds != null && createDto.ContactedEmployeeIds.Any())
            {
                var areEmployeesValid = await _employeeRepository.ValidateEmployeeIdsAsync(
                    createDto.ContactedEmployeeIds, userId, createDto.CompanyId);
                
                if (!areEmployeesValid)
                {
                    return null;
                }
            }
            
            // Create new application
            var application = _mapper.Map<Application>(createDto);
            application.UserId = userId;
            application.SubmissionDate = createDto.SubmissionDate ?? DateOnly.FromDateTime(DateTime.UtcNow);
            application.CreatedAt = DateTime.UtcNow;
            application.UpdatedAt = DateTime.UtcNow;
            
            // Save to database
            var applicationId = await _applicationRepository.CreateAsync(application);
            // Record initial stage history
            await _applicationRepository.UpsertStageHistoryAsync(applicationId, application.Stage, application.SubmissionDate, null);
            
            // Handle contacted employees
            if (createDto.ContactedEmployeeIds != null && createDto.ContactedEmployeeIds.Any())
            {
                var success = await _employeeRepository.AddApplicationEmployeesAsync(applicationId, createDto.ContactedEmployeeIds);
                if (!success)
                {
                    return null;
                }
            }
            
            // Retrieve the created application
            var createdApplication = await _applicationRepository.GetByIdAsync(applicationId);
            if (createdApplication == null)
            {
                throw new InvalidOperationException("Failed to retrieve the created application");
            }
            
            // Map to DTO
            var applicationDto = _mapper.Map<ApplicationResponseDto>(createdApplication);
            applicationDto.CompanyName = createdApplication.UserCompany?.Company?.Name ?? "Unknown Company";
            applicationDto.Company = _mapper.Map<DTOs.Company.CompanyResponseDto>(createdApplication.UserCompany?.Company);
            
            // Map contacted employees
            if (createdApplication.ApplicationEmployees != null && createdApplication.ApplicationEmployees.Any())
            {
                applicationDto.ContactedEmployees = _mapper.Map<List<EmployeeDto>>(
                    createdApplication.ApplicationEmployees.Select(ae => ae.Employee).ToList()
                );
            }
            
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
            
            // Validate contacted employees if provided (null means don't update)
            if (updateDto.ContactedEmployeeIds != null)
            {
                var areEmployeesValid = await _employeeRepository.ValidateEmployeeIdsAsync(
                    updateDto.ContactedEmployeeIds, userId, application.CompanyId);
                
                if (!areEmployeesValid)
                {
                    return false;
                }
            }
            
            // Update properties
            var originalStage = application.Stage;
            _mapper.Map(updateDto, application);
            application.UpdatedAt = DateTime.UtcNow;
            
            // Handle contacted employees if provided (null means don't update)
            if (updateDto.ContactedEmployeeIds != null)
            {
                var success = await _employeeRepository.UpdateApplicationEmployeesAsync(id, updateDto.ContactedEmployeeIds);
                if (!success)
                {
                    return false;
                }
            }
            
            // Save changes
            var updated = await _applicationRepository.UpdateAsync(application);

            // If stage changed, upsert stage history with provided SubmissionDate (or today if not provided in update)
            if (updated && updateDto.Stage.HasValue && updateDto.Stage.Value != originalStage)
            {
                var date = updateDto.SubmissionDate ?? DateOnly.FromDateTime(DateTime.UtcNow);
                await _applicationRepository.UpsertStageHistoryAsync(id, application.Stage, date, null);
            }

            return updated;
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

        public async Task<int> BulkDeleteApplicationsAsync(IEnumerable<int> ids, int userId)
        {
            return await _applicationRepository.BulkSoftDeleteAsync(ids, userId);
        }

        public async Task<bool> RecordStageAsync(int applicationId, int userId, ApplicationStage stage, DateOnly date, string? note = null)
        {
            var application = await _applicationRepository.GetByIdAsync(applicationId);
            if (application == null || application.UserId != userId)
            {
                return false;
            }

            // Update current stage and history
            application.Stage = stage;
            application.UpdatedAt = DateTime.UtcNow;
            var updated = await _applicationRepository.UpdateAsync(application);
            if (!updated) return false;

            return await _applicationRepository.UpsertStageHistoryAsync(applicationId, stage, date, note);
        }
    }
} 