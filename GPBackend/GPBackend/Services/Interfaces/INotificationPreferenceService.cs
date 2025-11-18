using GPBackend.DTOs.Notification;
using GPBackend.Models.Enums;

namespace GPBackend.Services.Interfaces
{
    public interface INotificationPreferenceService
    {
        Task<NotificationPreferenceResponseDto> GetUserPreferencesAsync(int userId);
        Task<NotificationPreferenceResponseDto> UpdateUserPreferencesAsync(int userId, NotificationPreferenceUpdateDto updateDto);
        Task<bool> ResetToDefaultAsync(int userId);
        Task<bool> DisableAllNotificationsAsync(int userId);
        Task<bool> EnableAllNotificationsAsync(int userId);
        Task<bool> ShouldSendNotificationAsync(int userId, NotificationCategory notificationCategory);
    }
}