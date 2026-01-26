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
                                .Where(q => q.UserId == userId &&
                                        q.NotificationId == notificationId &&
                                        !q.IsDeleted)
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
        public async Task<bool> DeleteAsync(int userId, int id)
        {
            var notification = _context.Notifications.FirstOrDefault(q => q.UserId == userId &&
                                                                     q.NotificationId == id &&
                                                                     !q.IsDeleted);
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
                                .OrderByDescending(q => q.CreatedAt)
                                .Skip((PageNumber - 1) * pageSize)
                                .Take(pageSize)
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
                    .Where(q => q.UserId == userId &&
                            !q.IsRead &&
                            !q.IsDeleted)
                    .OrderByDescending(q => q.CreatedAt)
                    .ToListAsync();
        }
        public async Task<int> GetUnreadCountAsync(int userId)
        {
            return await _context.Notifications
                        .Where(q => q.UserId == userId &&
                                !q.IsRead &&
                                !q.IsDeleted)
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

        public async Task<int> BulkDeleteAsync(int userId, List<int> ids)
        {
            if (ids.Count == 0)
            {
                return 0;
            }
            ids = ids.Distinct().ToList();
            var notificationsToDelete = _context.Notifications
                                        .Where(q => q.UserId == userId &&
                                                ids.Contains(q.NotificationId))
                                        .AsEnumerable();

            foreach (var notification in notificationsToDelete)
                notification.IsDeleted = true;

            return await _context.SaveChangesAsync();
        }

        public async Task<List<Interview>> GetInterviewsInDueDaysAsync(int dueDays)
        {
            var now = DateTime.Now;
            var futureDate = now.AddDays(dueDays);
            return await _context.Interviews
                            .Where(a => !a.IsDeleted &&
                                   a.StartDate >= now &&
                                   a.StartDate <= futureDate)
                            .AsNoTracking()
                            .ToListAsync();                   
        }

        public async Task<List<TodoList>> GetApplicationsInDueDaysAsync(int dueDays)
        {
            var now = DateTime.Now;
            var futureDate = now.AddDays(dueDays);
            return await _context.TodoLists
                                 .Where(a => !a.IsDeleted &&
                                        a.Deadline.HasValue &&
                                        a.Deadline.Value >= now &&
                                        a.Deadline.Value <= futureDate)
                                .AsNoTracking()
                                .ToListAsync();
                                
        }

        public async Task<bool> NotificationExistsAsync(int userId, int? entityTargetedId, Models.Enums.NotificationType type, int hoursWindow, string? messageContains = null)
        {
            var cutoffTime = DateTime.Now.AddHours(-hoursWindow);
            
            var query = _context.Notifications
                .Where(n => n.UserId == userId &&
                           n.Type == type &&
                           n.CreatedAt >= cutoffTime &&
                           !n.IsDeleted);
            
            // If entityTargetedId is provided, match on it
            if (entityTargetedId.HasValue)
            {
                query = query.Where(n => n.EntityTargetedId == entityTargetedId.Value);
            }
            
            // Check for exact message match to prevent duplicate spam
            if (!string.IsNullOrEmpty(messageContains))
            {
                query = query.Where(n => n.Message == messageContains);
            }
            
            return await query.AnyAsync();
        }

        public async Task<Notification?> GetBatchedNotificationAsync(int userId, int? entityTargetedId, Models.Enums.NotificationType type, int hoursWindow, string? messageContains = null)
        {
            var cutoffTime = DateTime.UtcNow.AddHours(-hoursWindow);
            
            var query = _context.Notifications
                .Where(n => n.UserId == userId &&
                           n.Type == type &&
                           n.CreatedAt >= cutoffTime &&
                           !n.IsDeleted &&
                           !n.IsRead);
            
            // If entityTargetedId is provided, match on it
            if (entityTargetedId.HasValue)
            {
                query = query.Where(n => n.EntityTargetedId == entityTargetedId.Value);
            }
            
            // Check for message pattern match (contains first word)
            if (!string.IsNullOrEmpty(messageContains))
            {
                var firstWord = messageContains.Split(' ').First();
                query = query.Where(n => n.Message.Contains(firstWord));
            }
            
            return await query.OrderByDescending(n => n.CreatedAt).FirstOrDefaultAsync();
        }
    }
}