using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.CompanyRequest;
using GPBackend.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/company-requests")]
    public class CompanyRequestsController : ControllerBase
    {
        private readonly ICompanyRequestService _companyRequestService;

        public CompanyRequestsController(ICompanyRequestService companyRequestService)
        {
            _companyRequestService = companyRequestService;
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

        // POST: api/company-requests
        // User submits a request for a new company
        [HttpPost]
        public async Task<ActionResult<CompanyRequestResponseDto>> CreateRequest([FromBody][Required] CompanyRequestCreateDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    return BadRequest(new { message = "Request body is required" });
                }
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }

                int userId = GetAuthenticatedUserId();
                
                try
                {
                    var createdRequest = await _companyRequestService.CreateRequestAsync(userId, createDto);
                    return CreatedAtAction(
                        nameof(GetRequestById),
                        new { id = createdRequest.RequestId },
                        createdRequest
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

        // GET: api/company-requests
        // Admin lists all company requests with filters
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<PagedResult<CompanyRequestResponseDto>>> GetAllRequests([FromQuery] CompanyRequestQueryDto queryDto)
        {
            var result = await _companyRequestService.GetFilteredRequestsAsync(queryDto);
            
            // Add pagination headers
            Response.Headers.Add("X-Pagination-TotalCount", result.TotalCount.ToString());
            Response.Headers.Add("X-Pagination-PageSize", result.PageSize.ToString());
            Response.Headers.Add("X-Pagination-CurrentPage", result.PageNumber.ToString());
            Response.Headers.Add("X-Pagination-TotalPages", result.TotalPages.ToString());
            Response.Headers.Add("X-Pagination-HasNext", result.HasNext.ToString());
            Response.Headers.Add("X-Pagination-HasPrevious", result.HasPrevious.ToString());

            return Ok(result);
        }

        // GET: api/company-requests/my-requests
        // User views their own company requests
        [HttpGet("my-requests")]
        public async Task<ActionResult<IEnumerable<CompanyRequestResponseDto>>> GetMyRequests()
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var requests = await _companyRequestService.GetUserRequestsAsync(userId);
                return Ok(requests);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // GET: api/company-requests/{id}
        // Get single company request (user can only see their own, admin can see all)
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyRequestResponseDto>> GetRequestById(int id)
        {
            try
            {
                var request = await _companyRequestService.GetRequestByIdAsync(id);
                
                if (request == null)
                {
                    return NotFound();
                }

                // Check authorization: user can only see their own requests unless they're admin
                int userId = GetAuthenticatedUserId();
                bool isAdmin = User.IsInRole("Admin");
                
                if (!isAdmin && request.UserId != userId)
                {
                    return Forbid();
                }
                
                return Ok(request);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // POST: api/company-requests/{id}/approve
        // Admin approves a company request
        [HttpPost("{id}/approve")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ApproveRequest(int id)
        {
            try
            {
                int adminId = GetAuthenticatedUserId();
                
                try
                {
                    var result = await _companyRequestService.ApproveRequestAsync(id, adminId);
                    
                    if (!result)
                    {
                        return NotFound();
                    }
                    
                    return Ok(new { message = "Company request approved successfully" });
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

        // POST: api/company-requests/{id}/reject
        // Admin rejects a company request with a reason
        [HttpPost("{id}/reject")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> RejectRequest(int id, [FromBody][Required] CompanyRequestReviewDto reviewDto)
        {
            try
            {
                if (reviewDto == null)
                {
                    return BadRequest(new { message = "Request body is required" });
                }
                
                if (string.IsNullOrWhiteSpace(reviewDto.RejectionReason))
                {
                    return BadRequest(new { message = "Rejection reason is required" });
                }
                
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }

                int adminId = GetAuthenticatedUserId();
                
                try
                {
                    var result = await _companyRequestService.RejectRequestAsync(id, adminId, reviewDto.RejectionReason);
                    
                    if (!result)
                    {
                        return NotFound();
                    }
                    
                    return Ok(new { message = "Company request rejected successfully" });
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
    }
}

