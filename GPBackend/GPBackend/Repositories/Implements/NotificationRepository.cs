using GPBackend.DTOs.Common;
using GPBackend.DTOs.Notification;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly GPDBContext _context;

        public NotificationRepository(GPDBContext context)
        {
            _context = context;
        }
        public async Task<Notification?> GetByIdAsync(int userId, int notificationId)
        {
            var notification = await _context.Notifications
                                .Where(q => q.UserId == userId && q.NotificationId == notificationId)
                                .FirstOrDefaultAsync();
            return notification;
        }
        public async Task<Notification> CreateAsync(Notification notification)
        {
            _context.Add(notification);
            await _context.SaveChangesAsync();

            return _context.Notifications.FirstOrDefault(q => q.NotificationId == notification.NotificationId);
        }
        public async Task<bool> UpdateAsync(Notification notification)
        {
            _context.Update(notification);

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var notification = _context.Notifications.FirstOrDefault(q => q.NotificationId == id);
            if(notification == null)
            {
                return false;
            }
            notification.IsDeleted = true;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<Notification>> GetPagedAsync(int userId, NotificationQueryDto notificationQueryDto)
        {
            var pageSize = notificationQueryDto.PageSize;
            var PageNumber = notificationQueryDto.PageNumber;

            var query = _context.Notifications
                        .Where(q => q.UserId == userId && !q.IsDeleted);

            var Notifications = await query
                                .Skip((PageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .OrderByDescending(q => q.CreatedAt)
                                .ToListAsync();

            return new PagedResult<Notification>
            {
                Items = Notifications,
                PageSize = pageSize,
                PageNumber = PageNumber,
                TotalCount = await query.CountAsync()
            };
        }
        public async Task<List<Notification>> GetUnreadAsync(int userId)
        {
            return await _context.Notifications
                    .Where(q => q.UserId == userId && !q.IsRead)
                    .ToListAsync();
        }
        public async Task<int> GetUnreadCountAsync(int userId)
        {
            return await _context.Notifications
                        .Where(q => q.UserId == userId && !q.IsRead)
                        .CountAsync();
        }

        public async Task<bool> BulkUpdateAsync(List<Notification> notifications)
        {
            _context.UpdateRange(notifications);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Notification>?> BulkCreateAsync(List<Notification> notifications)
        {
            await _context.AddRangeAsync(notifications);

            int RowsAffected = await _context.SaveChangesAsync();

            if (RowsAffected > 0)
            {
                return notifications;
            }
            return null;
        }

        public async Task<int> BulkDeleteAsync(List<int> ids)
        {
            if (ids.Count == 0)
            {
                return 0;
            }
            ids = ids.Distinct().ToList();
            var notificationsToDelete = _context.Notifications
                                        .Where(q => ids.Contains(q.NotificationId));

            foreach (var notification in notificationsToDelete)
                notification.IsDeleted = true;

            return await _context.SaveChangesAsync();
        }
    }
}