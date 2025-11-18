using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class NotificationPreferenceRepository : INotificationPreferenceRepository
    {
        private readonly GPDBContext _context;

        public NotificationPreferenceRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<NotificationPreference> GetOrCreateDefaultAsync(int userId)
        {
            var existing = await GetByUserIdAsync(userId);
            if (existing != null)
                return existing;

            // Create default preferences for the user
            var defaultPreference = new NotificationPreference
            {
                UserId = userId,
                EnableReminders = true,
                EnableSystem = true,
                EnableSocial = true,
                GloballyEnabled = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await CreateAsync(defaultPreference);
            return defaultPreference;
        }
        public async Task<NotificationPreference?> GetByUserIdAsync(int userId)
        {
            return await _context.NotificationPreferences
                .Include(np => np.User)
                .FirstOrDefaultAsync(np => np.UserId == userId);
        }

        public async Task<int> CreateAsync(NotificationPreference preference)
        {
            _context.NotificationPreferences.Add(preference);
            await _context.SaveChangesAsync();
            return preference.UserId;
        }

        public async Task<bool> UpdateAsync(NotificationPreference preference)
        {
            _context.Update(preference);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByUserIdAsync(int userId)
        {
            var preference = await _context.NotificationPreferences
                .FirstOrDefaultAsync(np => np.UserId == userId);
            
            if (preference == null)
                return false;

            _context.NotificationPreferences.Remove(preference);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsForUserAsync(int userId)
        {
            return await _context.NotificationPreferences
                .AnyAsync(np => np.UserId == userId);
        }
    }
}