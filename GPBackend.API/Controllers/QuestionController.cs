using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GPBackend.DTOs.Question;
using GPBackend.DTOs.Common;
using GPBackend.Services.Interfaces;
using System.Security.Claims;
using GPBackend.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using GPBackend.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.Controllers
{
    [Authorize]
    [Route("api/questions")]
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

        // TODO: Check nullability 
        [HttpPost]
        public async Task<ActionResult<QuestionResponseDto>> CreateNewQuestion([FromBody][Required] QuestionCreateDto questionCreateDto)
        {
            if (questionCreateDto == null)
            {
                return BadRequest(new { Message = "Request body is required" });
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            int userId = GetAuthenticatedUserId();
            var createdQuestion = await _QuestionService.CreateNewQuestion(userId, questionCreateDto);
            return CreatedAtAction(
                nameof(GetQuestionById),
                new { id = createdQuestion.QuestionId },
                createdQuestion
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionResponseDto>> GetQuestionById(int id)
        {
            int userId = GetAuthenticatedUserId();
            var question = await _QuestionService.GetQuestionById(userId, id);
            return Ok(question);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateQuestionById(int id, [FromBody][Required] QuestionUpdateDto questionUpdateDto)
        {
            if (questionUpdateDto == null)
            {
                return BadRequest(new { Message = "Request body is required" });
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            int userID = GetAuthenticatedUserId();
            await _QuestionService.UpdateQuestionById(id, userID, questionUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionById(int id)
        {
            int userID = GetAuthenticatedUserId();
            await _QuestionService.DeleteQuestionById(id, userID);
            return NoContent();
        }

        [HttpPost("bulk-delete")]
        public async Task<IActionResult> BulkDeleteQuestions([FromBody][Required] BulkDeleteRequestDto request)
        {
            int userID = GetAuthenticatedUserId();
            if (request == null || request.Ids == null || request.Ids.Count == 0)
            {
                return BadRequest(new { message = "Ids list is required" });
            }
            var deleted = await _QuestionService.BulkDeleteQuestionsAsync(request.Ids, userID);
            return Ok(new { deletedCount = deleted });
        }

        [HttpPost("batch")]
        public async Task<ActionResult<QuestionBatchResponseDto>> CreateQuestionsBatch([FromBody][Required] QuestionBatchCreateDto batchCreateDto)
        {
            if (batchCreateDto == null)
            {
                return BadRequest(new { Message = "Request body is required" });
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (batchCreateDto.Questions == null || batchCreateDto.Questions.Count == 0)
            {
                return BadRequest(new { Message = "At least one question is required" });
            }

            int userId = GetAuthenticatedUserId();
            var result = await _QuestionService.CreateQuestionsBatchAsync(userId, batchCreateDto);

            // Return 207 Multi-Status if there were partial failures, 201 if all succeeded
            if (result.Failed > 0 && result.SuccessfullyCreated > 0)
            {
                return StatusCode(207, result); // Multi-Status
            }
            else if (result.Failed > 0 && result.SuccessfullyCreated == 0)
            {
                return BadRequest(result);
            }
            else
            {
                return CreatedAtAction(nameof(GetQuestion), result);
            }
        }

        
    }
}