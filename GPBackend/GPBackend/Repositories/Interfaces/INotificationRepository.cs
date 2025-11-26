using GPBackend.Models.Enums;
using GPBackend.Models;
using GPBackend.DTOs.Notification;
using GPBackend.DTOs.Common;

namespace GPBackend.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        public Task<Notification?> GetByIdAsync(int userId, int notificationId);
        public Task<Notification> CreateAsync(Notification notification);
        public Task<bool> UpdateAsync(Notification notification);
        public Task<bool> DeleteAsync(int userId, int id);
        public Task<PagedResult<Notification>> GetPagedAsync(int userId, NotificationQueryDto notificationQueryDto);
        public Task<List<Notification>> GetUnreadAsync(int userId);
        public Task<int> GetUnreadCountAsync(int userId);
        public Task<bool> BulkUpdateAsync(List<Notification> notifications);
        public Task<List<Notification>?> BulkCreateAsync(List<Notification> notifications);
        public Task<int> BulkDeleteAsync(int userId, List<int> ids);
        public Task<List<TodoList>> GetApplicationsInDueDaysAsync(int dueDays);
        public Task<List<Interview>> GetInterviewsInDueDaysAsync(int dueDays);

    }
}