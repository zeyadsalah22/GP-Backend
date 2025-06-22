using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.ResumeTest;
using GPBackend.Services.Interfaces;
using GPBackend.Models;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [ApiController]
    [Route("api/resumetest")]
    [Authorize]
    public class ResumeTestController : ControllerBase
    {
        private readonly IResumeTestService _resumeTestService;

        public ResumeTestController(IResumeTestService resumeTestService)
        {
            _resumeTestService = resumeTestService;
        }

        private int GetAuthenticatedUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated properly");
            }
            return userId;
        }

        // Endpoints Definitions
        [HttpGet]
        public async Task<ActionResult<PagedResult<ResumeTestResponseDto>>> GetAll([FromQuery] ResumeTestQueryDto? ResumeTestQueryDto)
        {
            int userId = GetAuthenticatedUserId();
            try
            {
                // Use default pagination if no query parameters provided
                /*
                if (queryDto == null)
                {
                    queryDto = new ResumeTestQueryDto 
                    { 
                        PageNumber = 1, 
                        PageSize = 20 
                    };
                }
                */
                
                var result = await _resumeTestService.GetFilteredResumeTestsAsync(userId, ResumeTestQueryDto);

                // Add pagination headers
                Response.Headers.Append("X-Pagination-TotalCount", result.TotalCount.ToString());
                Response.Headers.Append("X-Pagination-PageSize", result.PageSize.ToString());
                Response.Headers.Append("X-Pagination-CurrentPage", result.PageNumber.ToString());
                Response.Headers.Append("X-Pagination-TotalPages", result.TotalPages.ToString());
                Response.Headers.Append("X-Pagination-HasNext", result.HasNext.ToString());
                Response.Headers.Append("X-Pagination-HasPrevious", result.HasPrevious.ToString());

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "User is not authenticated properly." });
            }
        }

        [HttpGet("{testId}")]
        public async Task<ActionResult<ResumeTestResponseDto>> GetResumeTestByIdAsync(int testId)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _resumeTestService.GetResumeTestByIdAsync(userId, testId);
                if (result == null)
                {
                    return NotFound(new { message = "Resume test not found." });
                }
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "User is not authenticated properly." });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResumeTestResponseDto>> CreateResumeTestAsync(ResumeTestCreateDto ResumeTestCreateDto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                try
                {
                    var createdResumeTest = await _resumeTestService.CreateResumeTestAsync(userId, ResumeTestCreateDto);
                    if (createdResumeTest == null)
                    {
                        return StatusCode(500, new { message = "Failed to Create the Resume Test." });    
                    }
                    return Created($"/api/resumetest/{createdResumeTest.TestId}", createdResumeTest);
                }
                catch (InvalidOperationException ex)
                    {
                        return BadRequest(new { message = ex.Message });
                    }
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "User is not authenticated properly." });
            }
        }

        [HttpDelete("{testId}")]
        public async Task<IActionResult> Delete(int testId)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _resumeTestService.DeleteResumeTestAsync(userId, testId);
                if (!result)
                {
                    return NotFound(new { message = "Resume Test not found." });
                }
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "User is not authenticated properly." });
            }
        }
    }
}