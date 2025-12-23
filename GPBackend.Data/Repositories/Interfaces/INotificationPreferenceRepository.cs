using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface INotificationPreferenceRepository
    {
        Task<NotificationPreference> GetOrCreateDefaultAsync(int userId);
        Task<NotificationPreference?> GetByUserIdAsync(int userId);
        Task<int> CreateAsync(NotificationPreference preference);
        Task<bool> UpdateAsync(NotificationPreference preference);
        Task<bool> DeleteByUserIdAsync(int userId);
        Task<bool> ExistsForUserAsync(int userId);
    }
}