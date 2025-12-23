using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GPBackend.Models;
using GPBackend.Models.Enums;
using GPBackend.Services.Interfaces;
using System.Security.Claims;
using GPBackend.DTOs.Auth;
using GPBackend.DTOs.User;
using Microsoft.AspNetCore.RateLimiting;

namespace GPBackend.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/users/me
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "UserOrAdmin")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            // Get the current user's ID and role from the token
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            
            if (string.IsNullOrEmpty(currentUserId) || !int.TryParse(currentUserId, out int userId))
            {
                return Unauthorized();
            }

            // Check authorization: user can update own profile OR admin can update any profile
            bool isAdmin = currentUserRole == UserRole.Admin.ToString();
            bool isUpdatingOwnProfile = userId == id;

            if (!isAdmin && !isUpdatingOwnProfile)
            {
                return StatusCode(403, new { message = "You can only update your own profile unless you are an admin" });
            }

            var result = await _userService.UpdateUserAsync(id, userUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/users/change-password
        [EnableRateLimiting("CustomUserLimiter")]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            // Get the current user's ID from the token
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(currentUserId) || !int.TryParse(currentUserId, out int userId))
            {
                return Unauthorized();
            }
            
            // Validate that new password and confirmation match
            if (model.NewPassword != model.NewPasswordConfirm)
            {
                return BadRequest(new { message = "New password and confirmation do not match" });
            }

            var result = await _userService.ChangePasswordAsync(userId, model.CurrentPassword, model.NewPassword);
            if (!result)
            {
                return BadRequest(new { message = "Current password is incorrect" });
            }

            return NoContent();
        }

        // PUT: api/users/{id}/change-role
        [HttpPut("{id}/change-role")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ChangeUserRole(int id, [FromBody] ChangeUserRoleDto changeRoleDto)
        {
            if (id != changeRoleDto.UserId)
            {
                return BadRequest(new { message = "User ID in URL does not match User ID in request body" });
            }

            var result = await _userService.ChangeUserRoleAsync(changeRoleDto.UserId, changeRoleDto.Role);
            if (!result)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(new { message = "User role updated successfully" });
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "UserOrAdmin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Get the current user's ID and role from the token
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            
            if (string.IsNullOrEmpty(currentUserId) || !int.TryParse(currentUserId, out int userId))
            {
                return Unauthorized();
            }

            // Check authorization: user can delete own account OR admin can delete any account
            bool isAdmin = currentUserRole == UserRole.Admin.ToString();
            bool isDeletingOwnAccount = userId == id;

            if (!isAdmin && !isDeletingOwnAccount)
            {
                return StatusCode(403, new { message = "You can only delete your own account unless you are an admin" });
            }

            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
} 