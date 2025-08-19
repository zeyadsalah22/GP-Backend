using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GPBackend.DTOs.Application;
using GPBackend.DTOs.Common;
using GPBackend.Services.Interfaces;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using GPBackend.DTOs.Common;

namespace GPBackend.Controllers
{
    [Authorize]
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
        private int GetAuthenticatedUserId()
        {
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
        [HttpGet]
        public async Task<ActionResult<PagedResult<ApplicationResponseDto>>> GetApplications([FromQuery] ApplicationQueryDto queryDto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
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
                return Unauthorized();
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
                    return NotFound();
                }
                
                return Ok(application);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // POST: api/applications
        [HttpPost]
        public async Task<ActionResult<ApplicationResponseDto>> CreateApplication(ApplicationCreateDto createDto)
        {
            try
            {
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
                    return BadRequest(new { message = ex.Message });
                }
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // PUT: api/applications/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(int id, ApplicationUpdateDto updateDto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _applicationService.UpdateApplicationAsync(id, userId, updateDto);
                
                if (!result)
                {
                    return NotFound();
                }
                
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // DELETE: api/applications/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _applicationService.DeleteApplicationAsync(id, userId);
                
                if (!result)
                {
                    return NotFound();
                }
                
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // POST: api/applications/bulk-delete
        [HttpPost("bulk-delete")]
        public async Task<IActionResult> BulkDeleteApplications([FromBody][Required] BulkDeleteRequestDto request)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                if (request == null || request.Ids == null || request.Ids.Count == 0)
                {
                    return BadRequest(new { message = "Ids list is required" });
                }
                var deleted = await _applicationService.BulkDeleteApplicationsAsync(request.Ids, userId);
                return Ok(new { deletedCount = deleted });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
} 