using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GPBackend.DTOs.Application;
using GPBackend.DTOs.Common;
using GPBackend.Services.Interfaces;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using GPBackend.Exceptions;

namespace GPBackend.Controllers
{
    [Authorize(Policy = "JwtOrApiKey")] // Allow both JWT and n8n API Key
    [Route("api/applications")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // Helper method to get the authenticated user's ID
        // Supports both JWT authentication and n8n API Key authentication
        private int GetAuthenticatedUserId(int? userIdFromQuery = null)
        {
            
            // Check if this is an n8n API Key authenticated request
            var isN8nAuth = User.HasClaim("N8N", "true");

            if (isN8nAuth)
            {
                // For n8n requests, userId must be provided in query params
                if (!userIdFromQuery.HasValue)
                {
                    throw new UnauthorizedAccessException("userId is required in query parameters for n8n API key authentication");
                }
                return userIdFromQuery.Value;
            }

            // For JWT authentication, extract from claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated properly");
            }
            return userId;
        }

        // GET: api/applications
        // Can be filtered with query params:
        // - ?companyId={companyId} - Filter by company ID
        // - ?companyName={name} - Filter by company name (partial match)
        // - ?jobTitle={title} - Filter by job title (partial match)
        // - ?jobType={type} - Filter by job type
        // - ?stage={stage} - Filter by application stage
        // - ?status={status} - Filter by application status
        // - ?fromDate={date} - Filter applications submitted after this date
        // - ?toDate={date} - Filter applications submitted before this date
        // - ?searchTerm={term} - Search in job title, description, company name
        // - ?sortBy={field}&sortDescending={true|false} - Sort results
        // - ?pageNumber={num}&pageSize={size} - Paginate results
        // - ?userId={id} - Required for n8n API key authentication
        [HttpGet]
        public async Task<ActionResult<PagedResult<ApplicationResponseDto>>> GetApplications([FromQuery] ApplicationQueryDto queryDto)
        {
            try
            {
                // For n8n requests, userId comes from query param. For JWT, it comes from claims.
                int userId = GetAuthenticatedUserId(queryDto.UserId);
                var result = await _applicationService.GetFilteredApplicationsAsync(userId, queryDto);
                
                // Add pagination headers
                Response.Headers.Add("X-Pagination-TotalCount", result.TotalCount.ToString());
                Response.Headers.Add("X-Pagination-PageSize", result.PageSize.ToString());
                Response.Headers.Add("X-Pagination-CurrentPage", result.PageNumber.ToString());
                Response.Headers.Add("X-Pagination-TotalPages", result.TotalPages.ToString());
                Response.Headers.Add("X-Pagination-HasNext", result.HasNext.ToString());
                Response.Headers.Add("X-Pagination-HasPrevious", result.HasPrevious.ToString());

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
        }

        // GET: api/applications/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationResponseDto>> GetApplicationById(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var application = await _applicationService.GetApplicationByIdAsync(id, userId);
                
                if (application == null)
                {
                    throw new NotFoundException("No application exists");
                }
                
                return Ok(application);
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
        }

        // POST: api/applications
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ApplicationResponseDto>> CreateApplication([FromBody][Required] ApplicationCreateDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    throw new BadHttpRequestException("Request body is required");
                }
                if (!ModelState.IsValid)
                {
                    throw new ValidationException(ModelState.ToString());
                }
                int userId = GetAuthenticatedUserId();
                
                try
                {
                    var createdApplication = await _applicationService.CreateApplicationAsync(userId, createDto);
                    return CreatedAtAction(
                        nameof(GetApplicationById),
                        new { id = createdApplication.ApplicationId },
                        createdApplication
                    );
                }
                catch (InvalidOperationException ex)
                {
                    throw new BadRequestException(ex.Message);
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
        }

        // PUT: api/applications/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateApplication(int id, [FromBody][Required] ApplicationUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null)
                {
                    throw new BadRequestException("Request body is required");
                }
                if (!ModelState.IsValid)
                {
                    throw new ValidationException(ModelState.ToString());
                }
                int userId = GetAuthenticatedUserId();
                var result = await _applicationService.UpdateApplicationAsync(id, userId, updateDto);
                
                if (!result)
                {
                    throw new NotFoundException("No application exists");
                }
                
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
        }

        // DELETE: api/applications/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _applicationService.DeleteApplicationAsync(id, userId);
                
                if (!result)
                {
                    throw new NotFoundException("No application exists");
                }
                
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
        }

        // POST: api/applications/bulk-delete
        [HttpPost("bulk-delete")]
        [Authorize]
        public async Task<IActionResult> BulkDeleteApplications([FromBody][Required] BulkDeleteRequestDto request)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                if (request == null || request.Ids == null || request.Ids.Count == 0)
                {
                    throw new BadRequestException("Ids list is required");
                }
                var deleted = await _applicationService.BulkDeleteApplicationsAsync(request.Ids, userId);
                return Ok(new { deletedCount = deleted });
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
        }
    }
} 