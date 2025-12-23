using GPBackend.DTOs.Reaction;
using GPBackend.Models.Enums;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostReactionsController : ControllerBase
    {
        private readonly IPostReactionService _postReactionService;

        public PostReactionsController(IPostReactionService postReactionService)
        {
            _postReactionService = postReactionService;
        }

        /// <summary>
        /// Add or update a reaction to a post. If the same reaction exists, it will be removed (toggle).
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateReaction([FromBody] PostReactionCreateDto reactionDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId == 0)
                    return Unauthorized(new { message = "User not authenticated." });

                var result = await _postReactionService.AddOrUpdateReactionAsync(userId, reactionDto);
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
        /// Remove a reaction from a post.
        /// </summary>
        [HttpDelete("{postId}")]
        public async Task<IActionResult> RemoveReaction(int postId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (userId == 0)
                    return Unauthorized(new { message = "User not authenticated." });

                await _postReactionService.RemoveReactionAsync(userId, postId);
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
        /// Get reaction summary for a post (counts for each reaction type).
        /// </summary>
        [HttpGet("{postId}/summary")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReactionSummary(int postId)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int? currentUserId = userIdClaim != null ? int.Parse(userIdClaim) : null;

                var summary = await _postReactionService.GetReactionSummaryAsync(postId, currentUserId);
                return Ok(new { data = summary });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get who reacted to a post, grouped by reaction type.
        /// </summary>
        [HttpGet("{postId}/who-reacted")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWhoReactedGrouped(int postId)
        {
            try
            {
                var result = await _postReactionService.GetWhoReactedGroupedAsync(postId);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get who reacted to a post filtered by reaction type, with pagination.
        /// </summary>
        [HttpGet("{postId}/who-reacted/{reactionType}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWhoReactedByType(
            int postId,
            ReactionType reactionType,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 50)
        {
            try
            {
                if (pageSize > 100)
                    pageSize = 100; // Max 100 per page

                var result = await _postReactionService.GetWhoReactedByTypeAsync(postId, reactionType, pageNumber, pageSize);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }
    }
}

