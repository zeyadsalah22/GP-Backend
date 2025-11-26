using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.CommunityInterviewQuestion;
using GPBackend.DTOs.InterviewAnswer;
using GPBackend.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/community/interview-questions")]
    public class CommunityInterviewQuestionsController : ControllerBase
    {
        private readonly ICommunityInterviewQuestionService _questionService;
        private readonly IInterviewAnswerService _answerService;

        public CommunityInterviewQuestionsController(
            ICommunityInterviewQuestionService questionService,
            IInterviewAnswerService answerService)
        {
            _questionService = questionService;
            _answerService = answerService;
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
        private int? GetAuthenticatedUserIdOrNull()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return null;
            }
            return userId;
        }

        /// <summary>
        /// Get all interview questions with filtering, searching, and pagination
        /// Browse Interview Questions - main page
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<CommunityInterviewQuestionResponseDto>>> GetQuestions(
            [FromQuery] CommunityInterviewQuestionQueryDto queryDto)
        {
            int? currentUserId = GetAuthenticatedUserIdOrNull();
            var result = await _questionService.GetFilteredQuestionsAsync(queryDto, currentUserId);

            // add pagination headers
            Response.Headers.Add("X-Pagination-TotalCount", result.TotalCount.ToString());
            Response.Headers.Add("X-Pagination-PageSize", result.PageSize.ToString());
            Response.Headers.Add("X-Pagination-CurrentPage", result.PageNumber.ToString());
            Response.Headers.Add("X-Pagination-TotalPages", result.TotalPages.ToString());
            Response.Headers.Add("X-Pagination-HasNext", result.HasNext.ToString());
            Response.Headers.Add("X-Pagination-HasPrevious", result.HasPrevious.ToString());

            return Ok(result);
        }

        /// <summary>
        /// Get a specific interview question by ID with all answers
        /// Browse Interview Questions - Details Mode On
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CommunityInterviewQuestionDetailDto>> GetQuestionById(int id)
        {
            int? currentUserId = GetAuthenticatedUserIdOrNull();
            var question = await _questionService.GetQuestionByIdAsync(id, currentUserId);

            if (question == null)
            {
                return NotFound(new { message = "Interview question not found" });
            }

            return Ok(question);
        }

        /// <summary>
        /// Submit a new interview question
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CommunityInterviewQuestionResponseDto>> CreateQuestion(
            [FromBody][Required] CommunityInterviewQuestionCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest(new { message = "Request body is required" });
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                int userId = GetAuthenticatedUserId();
                var createdQuestion = await _questionService.CreateQuestionAsync(createDto, userId);

                return CreatedAtAction(
                    nameof(GetQuestionById),
                    new { id = createdQuestion.QuestionId },
                    new { message = "Interview question submitted successfully", question = createdQuestion }
                );
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the question", error = ex.Message });
            }
        }

        /// <summary>
        /// Mark "I was asked this too" on a question
        /// </summary>
        [HttpPost("{id}/mark-asked")]
        public async Task<ActionResult> MarkAskedThisToo(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var success = await _questionService.MarkAskedThisTooAsync(id, userId);

                if (!success)
                {
                    return BadRequest(new { message = "You have already marked this question as asked" });
                }

                return Ok(new { message = "Successfully marked as asked" });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        /// <summary>
        /// Unmark "I was asked this too" on a question
        /// </summary>
        [HttpDelete("{id}/mark-asked")]
        public async Task<ActionResult> UnmarkAskedThisToo(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var success = await _questionService.UnmarkAskedThisTooAsync(id, userId);

                if (!success)
                {
                    return BadRequest(new { message = "You have not marked this question as asked" });
                }

                return Ok(new { message = "Successfully unmarked as asked" });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        /// <summary>
        /// Submit an answer to an interview question
        /// </summary>
        [HttpPost("{id}/answers")]
        public async Task<ActionResult<InterviewAnswerResponseDto>> CreateAnswer(
            int id,
            [FromBody][Required] InterviewAnswerCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest(new { message = "Request body is required" });
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                int userId = GetAuthenticatedUserId();
                var createdAnswer = await _answerService.CreateAnswerAsync(id, createDto, userId);

                return CreatedAtAction(
                    nameof(GetQuestionById),
                    new { id },
                    new { message = "Answer submitted successfully", answer = createdAnswer }
                );
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the answer", error = ex.Message });
            }
        }

        /// <summary>
        /// Mark an answer as helpful
        /// </summary>
        [HttpPost("answers/{answerId}/helpful")]
        public async Task<ActionResult> MarkAnswerAsHelpful(int answerId)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var success = await _answerService.MarkAnswerAsHelpfulAsync(answerId, userId);

                if (!success)
                {
                    return BadRequest(new { message = "You have already marked this answer as helpful or answer not found" });
                }

                return Ok(new { message = "Answer marked as helpful" });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        /// <summary>
        /// Unmark an answer as helpful
        /// </summary>
        [HttpDelete("answers/{answerId}/helpful")]
        public async Task<ActionResult> UnmarkAnswerAsHelpful(int answerId)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var success = await _answerService.UnmarkAnswerAsHelpfulAsync(answerId, userId);

                if (!success)
                {
                    return BadRequest(new { message = "You have not marked this answer as helpful or answer not found" });
                }

                return Ok(new { message = "Helpful vote removed" });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }
    }
}

