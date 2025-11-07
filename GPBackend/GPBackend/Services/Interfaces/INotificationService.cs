using GPBackend.DTOs.Common;
using GPBackend.DTOs.Notification;
using GPBackend.Models;
using GPBackend.Models.Enums;

namespace GPBackend.Services.Interfaces
{
    public interface INotificationService
    {
        /// <summary>
        /// Creates a new notification and sends it to the user in real-time via SignalR
        /// </summary>
        Task<NotificationResponseDto> CreateNotificationAsync(NotificationCreateDto notificationDto);

        /// <summary>
        /// Creates multiple notifications at once (bulk operation)
        /// </summary>
        Task<List<NotificationResponseDto>> CreateBulkNotificationsAsync(List<NotificationCreateDto> notificationDtos);

        /// <summary>
        /// Gets a single notification by ID
        /// </summary>
        Task<NotificationResponseDto?> GetNotificationByIdAsync(int userId, int notificationId);

        /// <summary>
        /// Gets paginated list of notifications for a user
        /// </summary>
        Task<PagedResult<NotificationResponseDto>> GetPagedNotificationsAsync(int userId, NotificationQueryDto queryDto);

        /// <summary>
        /// Gets all unread notifications for a user
        /// </summary>
        Task<List<NotificationResponseDto>> GetUnreadNotificationsAsync(int userId);

        /// <summary>
        /// Gets count of unread notifications for a user
        /// </summary>
        Task<int> GetUnreadCountAsync(int userId);
        
        /// <summary>
        /// Marks a single notification as read and broadcasts update via SignalR
        /// </summary>
        Task<bool> MarkAsReadAsync(int userId, int notificationId);

        /// <summary>
        /// Marks all notifications as read for a user and broadcasts update via SignalR
        /// </summary>
        Task<bool> MarkAllAsReadAsync(int userId);

        /// <summary>
        /// Deletes (soft delete) a notification and broadcasts update via SignalR
        /// </summary>
        Task<bool> DeleteNotificationAsync(int userId, int notificationId);

        /// <summary>
        /// Deletes multiple notifications
        /// </summary>
        Task<bool> DeleteMultipleNotificationsAsync(int userId, List<int> notificationIds);

        // ============================================
        // Real-Time Notification Delivery via SignalR
        // ============================================

        /// <summary>
        /// Sends a notification to a specific user via SignalR
        /// </summary>
        Task SendNotificationToUserAsync(int userId, NotificationResponseDto notification);

        /// <summary>
        /// Sends a notification to multiple users via SignalR
        /// </summary>
        Task SendNotificationToUsersAsync(List<int> userIds, NotificationResponseDto notification);

        /// <summary>
        /// Sends a notification to all connected users via SignalR
        /// </summary>
        Task SendNotificationToAllAsync(NotificationResponseDto notification);

        /// <summary>
        /// Broadcasts unread count update to user via SignalR
        /// </summary>
        
        /// <summary>
        /// Creates and sends application deadline reminder notification
        /// </summary>
        Task NotifyApplicationDeadlineAsync(int userId, int applicationId, string companyName, DateTime deadline);

        /// <summary>
        /// Creates and sends todo task reminder notification
        /// </summary>
        Task NotifyTodoDeadlineAsync(int userId, int todoId, string taskTitle, DateTime deadline);

        /// <summary>
        /// Creates and sends application status change notification
        /// </summary>
        Task NotifyInterviewScheduledAsync(int userId, int interviewId, string companyName, DateTime interviewDate);

        /// <summary>
        /// Creates and sends system announcement notification to all users
        /// </summary>
        Task NotifySystemAnnouncementAsync(string title, string message);

        /// <summary>
        /// Creates and sends welcome notification to new user
        /// </summary>
        Task NotifyWelcomeMessageAsync(int userId, string userName);
    }
}
