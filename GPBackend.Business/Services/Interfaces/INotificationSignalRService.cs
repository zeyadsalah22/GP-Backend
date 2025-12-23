using GPBackend.DTOs.Notification;

namespace GPBackend.Services.Interfaces
{
    public interface INotificationSignalRService
    {
        public Task SendNotificationToAllAsync(string Message);
        public Task SendNotificationToUserAsync(int UserId, string Message);
        public Task SendNotificationToUsersAsync(List<NotificationCreateDto> notifications);

    }
}