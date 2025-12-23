using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Interview;
using GPBackend.DTOs.Common;
using GPBackend.Models;
using System.Security.Claims;
using GPBackend.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.Controllers
{
    [Route("api/mockinterview")]
    [ApiController]
    [Authorize]
    public class InterviewController : ControllerBase
    {
        private readonly IInterviewService _interviewService;

        public InterviewController(IInterviewService interviewService)
        {
            _interviewService = interviewService;
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

        // Define your endpoints here
        [HttpGet()]
        public async Task<ActionResult<PagedResult<InterviewResponseDto>>> GetAllInterviewsAsync([FromQuery] InterviewQueryDto interviewQueryDto)
        {
            int userId = GetAuthenticatedUserId();
            var result = await _interviewService.GetFilteredInterviewsAsync(userId, interviewQueryDto);

            // Add pagination headers
            Response.Headers.Append("X-Pagination-TotalCount", result.TotalCount.ToString());
            Response.Headers.Append("X-Pagination-PageSize", result.PageSize.ToString());
            Response.Headers.Append("X-Pagination-CurrentPage", result.PageNumber.ToString());
            Response.Headers.Append("X-Pagination-TotalPages", result.TotalPages.ToString());
            Response.Headers.Append("X-Pagination-HasNext", result.HasNext.ToString());
            Response.Headers.Append("X-Pagination-HasPrevious", result.HasPrevious.ToString());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InterviewResponseDto>> GetInterviewByIdAsync(int id)
        {
            int userId = GetAuthenticatedUserId();
            var interview = await _interviewService.GetInterviewByIdAsync(userId, id);
            return Ok(interview);
        }


        // [HttpGet()]
        // public async Task<ActionResult<IEnumerable<InterviewResponseDto>>> GetUpcomingInterviews(string JobDescription, string JobTitle)
        // {
        //     try
        //     {
        //         var questions = await _interviewService.GetInterviewQuestionsAsync(JobDescription, JobTitle);
        //         if (questions == null || !questions.Any())
        //         {
        //             return NotFound(new { message = "No questions found." });
        //         }
        //         return Ok(questions);
        //     }
        //     catch (Exception ex)
        //     {
        //         // Log the exception (not shown here for brevity)
        //         return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
        //     }
        // }

        [HttpPost]
        public async Task<ActionResult<InterviewResponseDto>> CreateInterviewAsync(InterviewCreateDto interviewCreateDto)
        {
            int userId = GetAuthenticatedUserId();
            var createdInterview = await _interviewService.CreateInterviewAsync(userId, interviewCreateDto);
            return Created($"/api/mockinterview/{createdInterview.InterviewId}", createdInterview);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateInterviewByIDAsync(int id, InterviewUpdateDto interviewUpdateDto)
        {
            int userId = GetAuthenticatedUserId();
            await _interviewService.UpdateInterviewByIdAsync(userId, id, interviewUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInterviewByIdAsync(int id)
        {
            int userId = GetAuthenticatedUserId();
            await _interviewService.DeleteInterviewByIdAsync(userId, id);
            return NoContent();
        }

        [HttpPost("bulk-delete")]
        public async Task<IActionResult> BulkDelete([FromBody][Required] BulkDeleteRequestDto request)
        {
            int userId = GetAuthenticatedUserId();
            if (request == null || request.Ids == null || request.Ids.Count == 0)
            {
                return BadRequest(new { message = "Ids list is required" });
            }
            var deleted = await _interviewService.BulkDeleteInterviewsAsync(userId, request.Ids);
            return Ok(new { deletedCount = deleted });
        }
    }
}