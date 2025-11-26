using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.SavedPost;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/community")]
    public class SavedPostsController : ControllerBase
    {
        private readonly ISavedPostService _savedPostService;

        public SavedPostsController(ISavedPostService savedPostService)
        {
            _savedPostService = savedPostService;
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

        /// <summary>
        /// Save/bookmark a post
        /// </summary>
        [HttpPost("posts/{id}/save")]
        public async Task<ActionResult<SavedPostResponseDto>> SavePost(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var savedPost = await _savedPostService.SavePostAsync(userId, id);
                return Ok(new { message = "Post saved successfully", savedPost });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Remove a saved/bookmarked post
        /// </summary>
        [HttpDelete("posts/{id}/save")]
        public async Task<IActionResult> UnsavePost(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _savedPostService.UnsavePostAsync(userId, id);

                if (!result)
                {
                    return NotFound(new { message = "Saved post not found" });
                }

                return Ok(new { message = "Post unsaved successfully" });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get all saved posts for the authenticated user
        /// </summary>
        [HttpGet("users/me/saved-posts")]
        public async Task<ActionResult<IEnumerable<SavedPostResponseDto>>> GetMySavedPosts()
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var savedPosts = await _savedPostService.GetUserSavedPostsAsync(userId);
                return Ok(savedPosts);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

