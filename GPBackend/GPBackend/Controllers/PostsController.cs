using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Post;
using GPBackend.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ITagService _tagService;

        public PostsController(IPostService postService, ITagService tagService)
        {
            _postService = postService;
            _tagService = tagService;
        }

        // get the authenticated user's ID
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
        /// Get all posts with filtering and pagination
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResult<PostResponseDto>>> GetAllPosts([FromQuery] PostQueryDto queryDto)
        {
            var result = await _postService.GetFilteredPostsAsync(queryDto);

            // Add pagination headers
            Response.Headers.Add("X-Pagination-TotalCount", result.TotalCount.ToString());
            Response.Headers.Add("X-Pagination-PageSize", result.PageSize.ToString());
            Response.Headers.Add("X-Pagination-CurrentPage", result.PageNumber.ToString());
            Response.Headers.Add("X-Pagination-TotalPages", result.TotalPages.ToString());
            Response.Headers.Add("X-Pagination-HasNext", result.HasNext.ToString());
            Response.Headers.Add("X-Pagination-HasPrevious", result.HasPrevious.ToString());

            return Ok(result);
        }

        /// <summary>
        /// Get a specific post by ID
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PostResponseDto>> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound(new { message = "Post not found" });
            }
            return Ok(post);
        }

        /// <summary>
        /// Get all published posts (community feed)
        /// </summary>
        [HttpGet("published")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetPublishedPosts()
        {
            var posts = await _postService.GetPublishedPostsAsync();
            return Ok(posts);
        }

        /// <summary>
        /// Get posts by the authenticated user
        /// </summary>
        [HttpGet("my-posts")]
        public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetMyPosts()
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var posts = await _postService.GetPostsByUserIdAsync(userId);
                return Ok(posts);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Get draft posts by the authenticated user
        /// </summary>
        [HttpGet("my-drafts")]
        public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetMyDrafts()
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var posts = await _postService.GetDraftsByUserIdAsync(userId);
                return Ok(posts);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Create a new post (draft or published)
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PostResponseDto>> CreatePost([FromBody][Required] PostCreateDto postDto)
        {
            if (postDto == null)
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
                var createdPost = await _postService.CreatePostAsync(userId, postDto);

                string message = postDto.Status == Models.Enums.PostStatus.DRAFT
                    ? "Post saved as draft successfully"
                    : "Post published successfully";

                return CreatedAtAction(
                    nameof(GetPostById),
                    new { id = createdPost.PostId },
                    new { message, post = createdPost }
                );
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Update an existing post
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody][Required] PostUpdateDto postDto)
        {
            if (postDto == null)
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
                var result = await _postService.UpdatePostAsync(id, userId, postDto);

                if (!result)
                {
                    return NotFound(new { message = "Post not found or you don't have permission to update it" });
                }

                return Ok(new { message = "Post updated successfully" });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Delete a post (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _postService.DeletePostAsync(id, userId);

                if (!result)
                {
                    return NotFound(new { message = "Post not found or you don't have permission to delete it" });
                }

                return Ok(new { message = "Post deleted successfully" });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Bulk delete posts
        /// </summary>
        [HttpPost("bulk-delete")]
        public async Task<IActionResult> BulkDeletePosts([FromBody][Required] BulkDeleteRequestDto request)
        {
            if (request == null || request.Ids == null || request.Ids.Count == 0)
            {
                return BadRequest(new { message = "Ids list is required" });
            }

            try
            {
                int userId = GetAuthenticatedUserId();
                var deleted = await _postService.BulkDeletePostsAsync(request.Ids, userId);
                return Ok(new { deletedCount = deleted });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Get all available tags
        /// </summary>
        [HttpGet("tags")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetAllTags()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        /// <summary>
        /// Search tags for auto-suggest
        /// </summary>
        [HttpGet("tags/search")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TagDto>>> SearchTags([FromQuery] string? searchTerm)
        {
            var tags = await _tagService.SearchTagsAsync(searchTerm ?? "");
            return Ok(tags);
        }
    }
}

