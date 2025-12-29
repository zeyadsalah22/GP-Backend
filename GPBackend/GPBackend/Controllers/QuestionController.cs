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
        private readonly IConfiguration _configuration;

        public QuestionController(IQuestionService QuestionService, IConfiguration configuration)
        {
            _QuestionService = QuestionService;
            _configuration = configuration;
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
        public async Task<ActionResult<QuestionResponseDto>> CreateNewQuestion([FromBody][Required] QuestionCreateDto questionCreateDto)
        {
            try
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
        public async Task<IActionResult> UpdateQuestionById(int id, [FromBody][Required] QuestionUpdateDto questionUpdateDto)
        {
            try
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

        [HttpPost("bulk-delete")]
        public async Task<IActionResult> BulkDeleteQuestions([FromBody][Required] BulkDeleteRequestDto request)
        {
            try
            {
                int userID = GetAuthenticatedUserId();
                if (request == null || request.Ids == null || request.Ids.Count == 0)
                {
                    return BadRequest(new { message = "Ids list is required" });
                }
                var deleted = await _QuestionService.BulkDeleteQuestionsAsync(request.Ids, userID);
                return Ok(new { deletedCount = deleted });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost("batch")]
        public async Task<ActionResult<QuestionBatchResponseDto>> CreateQuestionsBatch([FromBody][Required] QuestionBatchCreateDto batchCreateDto)
        {
            try
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
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Get user's Q&A history for NodeRAG (authenticated via Bearer token)
        /// </summary>
        [AllowAnonymous]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<QuestionHistoryDto>>> GetUserQuestionHistory(int userId)
        {
            try
            {
                // Validate Bearer token from NodeRAG
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return Unauthorized(new { message = "Authorization header missing or invalid" });
                }

                var token = authHeader.Substring("Bearer ".Length).Trim();
                var expectedToken = _configuration["NodeRAG:BackendAuthToken"];
                
                if (string.IsNullOrEmpty(expectedToken) || token != expectedToken)
                {
                    return Unauthorized(new { message = "Invalid authentication token" });
                }

                // Fetch Q&A history
                var history = await _QuestionService.GetUserQuestionHistoryAsync(userId);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving question history", details = ex.Message });
            }
        }

        
    }
}