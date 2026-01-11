using AutoMapper;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.Company;
using GPBackend.DTOs.CompanyRequest;
using GPBackend.DTOs.Notification;
using GPBackend.Models;
using GPBackend.Models.Enums;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class CompanyRequestService : ICompanyRequestService
    {
        private readonly ICompanyRequestRepository _companyRequestRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyRequestService> _logger;

        public CompanyRequestService(
            ICompanyRequestRepository companyRequestRepository,
            ICompanyRepository companyRepository,
            INotificationService notificationService,
            IMapper mapper,
            ILogger<CompanyRequestService> logger)
        {
            _companyRequestRepository = companyRequestRepository;
            _companyRepository = companyRepository;
            _notificationService = notificationService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CompanyRequestResponseDto> CreateRequestAsync(int userId, CompanyRequestCreateDto dto)
        {
            // Check for duplicate pending requests
            var existingRequest = await _companyRequestRepository.CheckDuplicateRequestAsync(dto.CompanyName);
            if (existingRequest != null)
            {
                throw new InvalidOperationException($"A pending request for company '{dto.CompanyName}' already exists");
            }

            // Check if company already exists in the global database
            var companies = await _companyRepository.GetAllAsync();
            var existingCompany = companies.FirstOrDefault(c => 
                c.Name.Trim().ToLower() == dto.CompanyName.Trim().ToLower());
            
            if (existingCompany != null)
            {
                throw new InvalidOperationException($"Company '{dto.CompanyName}' already exists in the system");
            }

            // Create the request
            var request = _mapper.Map<CompanyRequest>(dto);
            request.UserId = userId;
            request.RequestStatus = CompanyRequestStatus.Pending;
            request.RequestedAt = DateTime.UtcNow;

            var requestId = await _companyRequestRepository.CreateRequestAsync(request);

            // Retrieve the created request with navigation properties
            var createdRequest = await _companyRequestRepository.GetRequestByIdAsync(requestId);
            
            return MapToResponseDto(createdRequest!);
        }

        public async Task<CompanyRequestResponseDto?> GetRequestByIdAsync(int requestId)
        {
            var request = await _companyRequestRepository.GetRequestByIdAsync(requestId);
            return request != null ? MapToResponseDto(request) : null;
        }

        public async Task<PagedResult<CompanyRequestResponseDto>> GetFilteredRequestsAsync(CompanyRequestQueryDto queryDto)
        {
            var pagedRequests = await _companyRequestRepository.GetFilteredRequestsAsync(queryDto);

            // Map the result items to DTOs
            var dtoItems = pagedRequests.Items.Select(MapToResponseDto).ToList();

            // Create a new paged result with the mapped items
            return new PagedResult<CompanyRequestResponseDto>
            {
                Items = dtoItems,
                PageNumber = pagedRequests.PageNumber,
                PageSize = pagedRequests.PageSize,
                TotalCount = pagedRequests.TotalCount
            };
        }

        public async Task<IEnumerable<CompanyRequestResponseDto>> GetUserRequestsAsync(int userId)
        {
            var requests = await _companyRequestRepository.GetUserRequestsAsync(userId);
            return requests.Select(MapToResponseDto);
        }

        public async Task<bool> ApproveRequestAsync(int requestId, int adminId)
        {
            var request = await _companyRequestRepository.GetRequestByIdAsync(requestId);
            if (request == null)
            {
                return false;
            }

            if (request.RequestStatus != CompanyRequestStatus.Pending)
            {
                throw new InvalidOperationException("Only pending requests can be approved");
            }

            // Create the company in the global database
            var companyDto = new CompanyCreateDto
            {
                Name = request.CompanyName,
                Location = request.Location,
                IndustryId = request.IndustryId ?? 1, // Default industry if not specified
                LinkedinLink = request.LinkedinLink,
                CareersLink = request.CareersLink,
                Description = request.Description,
                CompanySize = "Unknown" // Default company size
            };

            Company createdCompany;
            try
            {
                var company = _mapper.Map<Company>(companyDto);
                company.CreatedAt = DateTime.UtcNow;
                company.UpdatedAt = DateTime.UtcNow;
                createdCompany = await _companyRepository.CreateAsync(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create company from approved request {RequestId}", requestId);
                throw new InvalidOperationException("Failed to create company in global database", ex);
            }

            // Update the request status
            var statusUpdated = await _companyRequestRepository.UpdateRequestStatusAsync(
                requestId, 
                CompanyRequestStatus.Approved, 
                adminId, 
                null);

            if (!statusUpdated)
            {
                return false;
            }

            // Send notification to the user
            try
            {
                var notificationDto = new NotificationCreateDto
                {
                    UserId = request.UserId,
                    Type = Models.Enums.NotificationType.Application,
                    NotificationCategory = NotificationCategory.System,
                    Message = $"Your company request '{request.CompanyName}' has been approved and is now available for all users!",
                    ActorId = adminId,
                    EntityTargetedId = createdCompany.CompanyId
                };

                await _notificationService.CreateNotificationAsync(notificationDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send notification for approved request {RequestId}", requestId);
                // Don't fail the approval if notification fails
            }

            return true;
        }

        public async Task<bool> RejectRequestAsync(int requestId, int adminId, string reason)
        {
            var request = await _companyRequestRepository.GetRequestByIdAsync(requestId);
            if (request == null)
            {
                return false;
            }

            if (request.RequestStatus != CompanyRequestStatus.Pending)
            {
                throw new InvalidOperationException("Only pending requests can be rejected");
            }

            // Update the request status
            var statusUpdated = await _companyRequestRepository.UpdateRequestStatusAsync(
                requestId, 
                CompanyRequestStatus.Rejected, 
                adminId, 
                reason);

            if (!statusUpdated)
            {
                return false;
            }

            // Send notification to the user
            try
            {
                var notificationDto = new NotificationCreateDto
                {
                    UserId = request.UserId,
                    Type = Models.Enums.NotificationType.Application,
                    NotificationCategory = NotificationCategory.System,
                    Message = $"Your company request '{request.CompanyName}' was declined. Reason: {reason}",
                    ActorId = adminId,
                    EntityTargetedId = requestId
                };

                await _notificationService.CreateNotificationAsync(notificationDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send notification for rejected request {RequestId}", requestId);
                // Don't fail the rejection if notification fails
            }

            return true;
        }

        private CompanyRequestResponseDto MapToResponseDto(CompanyRequest request)
        {
            return new CompanyRequestResponseDto
            {
                RequestId = request.RequestId,
                UserId = request.UserId,
                UserName = $"{request.User?.Fname} {request.User?.Lname}",
                CompanyName = request.CompanyName,
                Location = request.Location,
                IndustryId = request.IndustryId,
                IndustryName = request.Industry?.Name,
                LinkedinLink = request.LinkedinLink,
                CareersLink = request.CareersLink,
                Description = request.Description,
                RequestStatus = request.RequestStatus,
                RequestedAt = request.RequestedAt,
                ReviewedAt = request.ReviewedAt,
                ReviewedByAdminId = request.ReviewedByAdminId,
                ReviewedByAdminName = request.ReviewedByAdmin != null ? $"{request.ReviewedByAdmin.Fname} {request.ReviewedByAdmin.Lname}" : null,
                RejectionReason = request.RejectionReason
            };
        }
    }
}

