using GPBackend.DTOs.Common;
using GPBackend.DTOs.Notification;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get paginated notifications for the current user
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedResult<NotificationResponseDto>>> GetNotifications([FromQuery] NotificationQueryDto queryDto)
        {
            var userId = GetCurrentUserId();
            var notifications = await _notificationService.GetPagedNotificationsAsync(userId, queryDto);
            return Ok(notifications);
        }

        /// <summary>
        /// Get a specific notification by ID
        /// </summary>
        [HttpGet("{notificationId}")]
        public async Task<ActionResult<NotificationResponseDto>> GetNotificationById(int notificationId)
        {
            var userId = GetCurrentUserId();
            var notification = await _notificationService.GetNotificationByIdAsync(userId, notificationId);
            
            if (notification == null)
                return NotFound(new { message = "Notification not found" });

            return Ok(notification);
        }

        /// <summary>
        /// Get all unread notifications for the current user
        /// </summary>
        [HttpGet("unread")]
        public async Task<ActionResult<List<NotificationResponseDto>>> GetUnreadNotifications()
        {
            var userId = GetCurrentUserId();
            var notifications = await _notificationService.GetUnreadNotificationsAsync(userId);
            return Ok(notifications);
        }

        /// <summary>
        /// Get unread notification count
        /// </summary>
        [HttpGet("unread/count")]
        public async Task<ActionResult<int>> GetUnreadCount()
        {
            var userId = GetCurrentUserId();
            var count = await _notificationService.GetUnreadCountAsync(userId);
            return Ok(new { unreadCount = count });
        }

        /// <summary>
        /// Create a new notification (Admin/Testing only)
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<NotificationResponseDto>> CreateNotification([FromBody] NotificationCreateDto notificationDto)
        {
            var notification = await _notificationService.CreateNotificationAsync(notificationDto);
            if(notification == null){
                return NoContent();
            }
            return CreatedAtAction(nameof(GetNotificationById), new { notificationId = notification.NotificationId }, notification);
        }

        /// <summary>
        /// Mark a notification as read
        /// </summary>
        [HttpPatch("{notificationId}/read")]
        public async Task<ActionResult> MarkAsRead(int notificationId)
        {
            var userId = GetCurrentUserId();
            var result = await _notificationService.MarkAsReadAsync(userId, notificationId);
            
            if (!result)
                return NotFound(new { message = "Notification not found" });

            return Ok(new { message = "Notification marked as read" });
        }

        /// <summary>
        /// Mark all notifications as read
        /// </summary>
        [HttpPatch("read-all")]
        public async Task<ActionResult> MarkAllAsRead()
        {
            var userId = GetCurrentUserId();
            var result = await _notificationService.MarkAllAsReadAsync(userId);
            return Ok(new { message = "All notifications marked as read" });
        }

        /// <summary>
        /// Delete a notification
        /// </summary>
        [HttpDelete("{notificationId}")]
        public async Task<ActionResult> DeleteNotification(int notificationId)
        {
            var userId = GetCurrentUserId();
            var result = await _notificationService.DeleteNotificationAsync(userId, notificationId);
            
            if (!result)
                return NotFound(new { message = "Notification not found" });

            return Ok(new { message = "Notification deleted successfully" });
        }

        /// <summary>
        /// Delete multiple notifications
        /// </summary>
        [HttpDelete("bulk")]
        public async Task<ActionResult> DeleteMultipleNotifications([FromBody] List<int> notificationIds)
        {
            var userId = GetCurrentUserId();
            var result = await _notificationService.DeleteMultipleNotificationsAsync(userId, notificationIds);
            
            if (!result)
                return BadRequest(new { message = "Failed to delete some notifications" });

            return Ok(new { message = "Notifications deleted successfully" });
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
