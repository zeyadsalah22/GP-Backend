using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GPBackend.Models;
using GPBackend.Services.Interfaces;
using System.Security.Claims;
using GPBackend.DTOs.Auth;
using GPBackend.DTOs.User;

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
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/users/current
        [HttpGet("current")]
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
        public async Task<IActionResult> GetUserById(int id)
        {
            // Get the current user's ID from the token
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(currentUserId) || !int.TryParse(currentUserId, out int userId))
            {
                return Unauthorized();
            }

            // Only allow users to access their own data
            if (userId != id)
            {
                return Forbid();
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            // Get the current user's ID from the token
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(currentUserId) || !int.TryParse(currentUserId, out int userId))
            {
                return Unauthorized();
            }

            // Only allow users to update their own data
            if (userId != id)
            {
                return Forbid();
            }

            var result = await _userService.UpdateUserAsync(id, userUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/users/{id}/change-password
        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto model)
        {
            // Get the current user's ID from the token
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(currentUserId) || !int.TryParse(currentUserId, out int userId))
            {
                return Unauthorized();
            }

            // Only allow users to change their own password
            if (userId != id)
            {
                return Forbid();
            }
            
            // Validate that new password and confirmation match
            if (model.NewPassword != model.NewPasswordConfirm)
            {
                return BadRequest(new { message = "New password and confirmation do not match" });
            }

            var result = await _userService.ChangePasswordAsync(id, model.CurrentPassword, model.NewPassword);
            if (!result)
            {
                return BadRequest(new { message = "Current password is incorrect" });
            }

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Get the current user's ID from the token
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(currentUserId) || !int.TryParse(currentUserId, out int userId))
            {
                return Unauthorized();
            }

            // Only allow users to delete their own account
            if (userId != id)
            {
                return Forbid();
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