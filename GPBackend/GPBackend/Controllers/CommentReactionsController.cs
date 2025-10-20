using GPBackend.DTOs.Reaction;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentReactionsController : ControllerBase
    {
        private readonly ICommentReactionService _commentReactionService;

        public CommentReactionsController(ICommentReactionService commentReactionService)
        {
            _commentReactionService = commentReactionService;
        }

        /// <summary>
        /// Add or update a reaction to a comment. If the same reaction exists, it will be removed (toggle).
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateReaction([FromBody] CommentReactionCreateDto reactionDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId == 0)
                    return Unauthorized(new { message = "User not authenticated." });

                var result = await _commentReactionService.AddOrUpdateReactionAsync(userId, reactionDto);
                return Ok(new { message = "Reaction added/updated successfully.", data = result });
            }
            catch (InvalidOperationException ex)
            {
                return Ok(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        /// <summary>
        /// Remove a reaction from a comment.
        /// </summary>
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> RemoveReaction(int commentId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId == 0)
                    return Unauthorized(new { message = "User not authenticated." });

                await _commentReactionService.RemoveReactionAsync(userId, commentId);
                return Ok(new { message = "Reaction removed successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get reaction summary for a comment (upvote/downvote counts and score).
        /// </summary>
        [HttpGet("{commentId}/summary")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReactionSummary(int commentId)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int? currentUserId = userIdClaim != null ? int.Parse(userIdClaim) : null;

                var summary = await _commentReactionService.GetReactionSummaryAsync(commentId, currentUserId);
                return Ok(new { data = summary });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }
    }
}

