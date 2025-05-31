using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Interview;
using GPBackend.DTOs.Common;
using GPBackend.Models;
using System.Security.Claims;

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
            try
            {
                var result = await _interviewService.GetAllInterviewsAsync(userId, interviewQueryDto);

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
                return Unauthorized(new { message = "User is not authenticated properly." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InterviewResponseDto>> GetInterviewByIdAsync(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var interview = await _interviewService.GetInterviewByIdAsync(userId, id);
                if (interview == null)
                {
                    return NotFound(new { message = "Interview not found." });
                }
                return Ok(interview);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "User is not authenticated properly." });
            }
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
            try
            {
                int userId = GetAuthenticatedUserId();
                var createdInterview = await _interviewService.CreateInterviewAsync(userId, interviewCreateDto);
                return CreatedAtAction(nameof(GetAllInterviewsAsync), new { id = createdInterview.Id }, createdInterview);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "User is not authenticated properly." });
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateInterviewByIDAsync(int id, InterviewUpdateDto interviewUpdateDto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var updatedInterview = await _interviewService.UpdateInterviewByIdAsync(userId, id, interviewUpdateDto);
                if (updatedInterview == null)
                {
                    return NotFound(new { message = "Interview not found." });
                }
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "User is not authenticated properly." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInterviewByIdAsync(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _interviewService.DeleteInterviewByIdAsync(userId, id);
                if (!result)
                {
                    return NotFound(new { message = "Interview not found." });
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