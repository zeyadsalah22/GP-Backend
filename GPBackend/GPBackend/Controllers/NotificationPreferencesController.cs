using GPBackend.DTOs.Notification;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationPreferencesController : ControllerBase
    {
        private readonly INotificationPreferenceService _preferenceService;
        private readonly ILogger<NotificationPreferencesController> _logger;

        public NotificationPreferencesController(
            INotificationPreferenceService preferenceService,
            ILogger<NotificationPreferencesController> logger)
        {
            _preferenceService = preferenceService;
            _logger = logger;
        }

        /// <summary>
        /// Get notification preferences for the current user
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<NotificationPreferenceResponseDto>> GetNotificationPreferences()
        {
            try
            {
                var userId = GetCurrentUserId();
                var preferences = await _preferenceService.GetUserPreferencesAsync(userId);
                return Ok(preferences);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = "Unauthorized access" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving notification preferences");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Update notification preferences for the current user
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<NotificationPreferenceResponseDto>> UpdateNotificationPreferences(
            [FromBody] NotificationPreferenceUpdateDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = GetCurrentUserId();
                
                // Ensure the DTO has the correct user ID
                updateDto.UserId = userId;

                var updatedPreferences = await _preferenceService.UpdateUserPreferencesAsync(userId, updateDto);
                
                if (updatedPreferences == null)
                    return BadRequest(new { message = "Failed to update notification preferences" });

                return Ok(updatedPreferences);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = "Unauthorized access" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating notification preferences for user");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Reset notification preferences to default values
        /// </summary>
        [HttpPost("reset-to-default")]
        public async Task<ActionResult> ResetToDefault()
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _preferenceService.ResetToDefaultAsync(userId);
                
                if (!result)
                    return BadRequest(new { message = "Failed to reset notification preferences" });

                return Ok(new { message = "Notification preferences reset to default successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = "Unauthorized access" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting notification preferences for user");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Disable all notifications for the current user
        /// </summary>
        [HttpPost("disable-all")]
        public async Task<ActionResult> DisableAllNotifications()
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _preferenceService.DisableAllNotificationsAsync(userId);
                
                if (!result)
                    return BadRequest(new { message = "Failed to disable all notifications" });

                return Ok(new { message = "All notifications disabled successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = "Unauthorized access" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disabling all notifications for user");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Enable all notifications for the current user
        /// </summary>
        [HttpPost("enable-all")]
        public async Task<ActionResult> EnableAllNotifications()
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _preferenceService.EnableAllNotificationsAsync(userId);
                
                if (!result)
                    return BadRequest(new { message = "Failed to enable all notifications" });

                return Ok(new { message = "All notifications enabled successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = "Unauthorized access" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enabling all notifications for user");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get notification preferences for a specific user (Admin only)
        /// </summary>
        [HttpGet("user/{userId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<NotificationPreferenceResponseDto>> GetUserNotificationPreferences(int userId)
        {
            try
            {
                var preferences = await _preferenceService.GetUserPreferencesAsync(userId);
                return Ok(preferences);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving notification preferences for user {UserId}", userId);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Update notification preferences for a specific user (Admin only)
        /// </summary>
        [HttpPut("user/{userId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<NotificationPreferenceResponseDto>> UpdateUserNotificationPreferences(
            int userId, [FromBody] NotificationPreferenceUpdateDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Ensure the DTO has the correct user ID
                updateDto.UserId = userId;

                var updatedPreferences = await _preferenceService.UpdateUserPreferencesAsync(userId, updateDto);
                
                if (updatedPreferences == null)
                    return BadRequest(new { message = "Failed to update notification preferences" });

                return Ok(updatedPreferences);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating notification preferences for user {UserId}", userId);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        // Helper method to get current user ID from JWT claims
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }

            return userId;
        }
    }
}