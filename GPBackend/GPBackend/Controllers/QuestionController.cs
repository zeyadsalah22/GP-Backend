using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GPBackend.DTOs.Question;
using GPBackend.DTOs.Common;
using GPBackend.Services.Interfaces;
using System.Security.Claims;
using GPBackend.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace GPBackend.Controllers
{
    [Authorize]
    [Route("api/Question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _QuestionService;

        public QuestionController(IQuestionService QuestionService)
        {
            _QuestionService = QuestionService;
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

        [HttpGet]
        public async Task<ActionResult<PagedResult<QuestionResponseDto>>> GetQuestion([FromQuery] QuestionQueryDto queryDto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _QuestionService.GetFilteredQuestionBasedOnQuery(userId, queryDto);

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
                return Unauthorized();
            }
        }

        // TODO: Check nullability 
        [HttpPost]
        public async Task<ActionResult<QuestionResponseDto>> CreateNewQuestion(QuestionCreateDto questionCreateDto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                try
                {
                    var createdQuestion = await _QuestionService.CreateNewQuestion(userId, questionCreateDto);
                    try
                    {
                        return CreatedAtAction(
                            nameof(GetQuestionById),
                            new { id = createdQuestion.QuestionId },
                            createdQuestion
                        );
                    }
                    catch (NullReferenceException ex)
                    {
                        return BadRequest(new { Message = ex.Message });
                    }
                }
                    catch (InvalidOperationException ex)
                    {
                        return BadRequest(new { Message = ex.Message });
                    }
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionResponseDto>> GetQuestionById(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var question = await _QuestionService.GetQuestionById(userId, id);

                if (question == null)
                {
                    return NotFound();
                }
                return Ok(question);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateQuestionById(int id, QuestionUpdateDto questionUpdateDto)
        {
            try
            {
                int userID = GetAuthenticatedUserId();
                var result = await _QuestionService.UpdateQuestionById(id, userID, questionUpdateDto);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionById(int id)
        {
            try
            {
                int userID = GetAuthenticatedUserId();
                var result = await _QuestionService.DeleteQuestionById(id, userID);
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

        
    }
}