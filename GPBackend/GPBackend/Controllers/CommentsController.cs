using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Comment;
using GPBackend.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
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

        /// <summary>
        /// Get comments for a specific post with pagination
        /// </summary>
        [HttpGet("post/{postId}")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<CommentResponseDto>>> GetCommentsByPostId(
            int postId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? level = null,
            [FromQuery] string? sortBy = "CreatedAt",
            [FromQuery] string? sortOrder = "DESC")
        {
            var queryDto = new CommentQueryDto
            {
                PostId = postId,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Level = level,
                SortBy = sortBy,
                SortOrder = sortOrder
            };

            var result = await _commentService.GetCommentsByPostIdAsync(queryDto);

            Response.Headers.Add("X-Pagination-TotalCount", result.TotalCount.ToString());
            Response.Headers.Add("X-Pagination-PageSize", result.PageSize.ToString());
            Response.Headers.Add("X-Pagination-CurrentPage", result.PageNumber.ToString());
            Response.Headers.Add("X-Pagination-TotalPages", result.TotalPages.ToString());
            Response.Headers.Add("X-Pagination-HasNext", result.HasNext.ToString());
            Response.Headers.Add("X-Pagination-HasPrevious", result.HasPrevious.ToString());

            return Ok(result);
        }

        /// <summary>
        /// Get a specific comment by ID
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CommentResponseDto>> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound(new { message = "Comment not found" });
            }
            return Ok(comment);
        }

        /// <summary>
        /// Get top comment previews for a post (for post feed)
        /// </summary>
        [HttpGet("post/{postId}/preview")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CommentPreviewDto>>> GetCommentPreviews(
            int postId,
            [FromQuery] int count = 2)
        {
            var previews = await _commentService.GetTopCommentPreviewsAsync(postId, count);
            return Ok(previews);
        }

        /// <summary>
        /// Create a new comment or reply
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CommentResponseDto>> CreateComment([FromBody][Required] CommentCreateDto commentDto)
        {
            if (commentDto == null)
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
                var createdComment = await _commentService.CreateCommentAsync(userId, commentDto);

                string message = commentDto.ParentCommentId.HasValue
                    ? "Reply posted successfully"
                    : "Comment posted successfully";

                return CreatedAtAction(
                    nameof(GetCommentById),
                    new { id = createdComment.CommentId },
                    new { message, comment = createdComment }
                );
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the comment", error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing comment
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<CommentResponseDto>> UpdateComment(
            int id,
            [FromBody][Required] CommentUpdateDto commentDto)
        {
            if (commentDto == null)
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
                var updatedComment = await _commentService.UpdateCommentAsync(id, userId, commentDto);

                return Ok(new
                {
                    message = "Comment updated successfully",
                    comment = updatedComment
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The comment was modified by another user. Please refresh and try again." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the comment", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a comment (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                bool result = await _commentService.DeleteCommentAsync(id, userId);

                if (!result)
                {
                    return NotFound(new { message = "Comment not found" });
                }

                return Ok(new { message = "Comment deleted successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the comment", error = ex.Message });
            }
        }

        /// <summary>
        /// Search users for mentions (autocomplete)
        /// </summary>
        [HttpGet("search-users")]
        public async Task<ActionResult<List<string>>> SearchUsersForMention(
            [FromQuery][Required] string searchTerm,
            [FromQuery] int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest(new { message = "Search term is required" });
            }

            try
            {
                var users = await _commentService.SearchUsersForMentionAsync(searchTerm, limit);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching users", error = ex.Message });
            }
        }
    }
}


