using AutoMapper;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.Notification;
using GPBackend.Hubs;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace GPBackend.Services.Implements
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationSignalRService _signalR;
        private readonly IEmailService _emailService;
        private readonly INotificationRepository _notificationRepo;
        private readonly IMapper _mapper;

        public NotificationService(
                INotificationSignalRService signalR,
                IEmailService emailService,
                INotificationRepository notificationRepo,
                IMapper mapper)
        {
            _signalR = signalR;
            _emailService = emailService;
            _notificationRepo = notificationRepo;
            _mapper = mapper;
        }
        public async Task<List<NotificationResponseDto>> CreateBulkNotificationsAsync(List<NotificationCreateDto> notificationDtos)
        {
            var notifications = _mapper.Map<List<NotificationCreateDto>, List<Notification>>(notificationDtos);
            notifications = await _notificationRepo.BulkCreateAsync(notifications);

            if (notifications == null)
                return new List<NotificationResponseDto>();

            // TODO: Send notification to users using SignalR
            await _signalR.SendNotificationToUsersAsync(notificationDtos);

            var notificationResponseDtos = _mapper.Map<List<Notification>, List<NotificationResponseDto>>(notifications);
            return notificationResponseDtos;
        }

        public async Task<NotificationResponseDto> CreateNotificationAsync(NotificationCreateDto notificationDto)
        {
            var Notification = _mapper.Map<NotificationCreateDto, Notification>(notificationDto);
            Notification = await _notificationRepo.CreateAsync(Notification);

            
            await _signalR.SendNotificationToUserAsync(Notification.UserId, "You have new unread notifications");

            var NotificationResponseDto = _mapper.Map<Notification, NotificationResponseDto>(Notification);

            return NotificationResponseDto;
        }

        public async Task<bool> DeleteMultipleNotificationsAsync(int userId, List<int> notificationIds)
        {
            int count = await _notificationRepo.BulkDeleteAsync(userId, notificationIds);
            return count == notificationIds.Count;
        }

        public async Task<bool> DeleteNotificationAsync(int userId, int notificationId)
        {
            bool result = await _notificationRepo.DeleteAsync(userId, notificationId);
            return result;
        }

        public async Task<NotificationResponseDto?> GetNotificationByIdAsync(int userId, int notificationId)
        {
            var notification = await _notificationRepo.GetByIdAsync(userId, notificationId);
            if (notification == null)
            {
                return null;
            }
            var notificationResponse = _mapper.Map<Notification, NotificationResponseDto>(notification);
            return notificationResponse;
        }

        public async Task<PagedResult<NotificationResponseDto>> GetPagedNotificationsAsync(int userId, NotificationQueryDto queryDto)
        {
            var pagedNotifications = await _notificationRepo.GetPagedAsync(userId, queryDto);
            
            var notificationDtos = _mapper.Map<List<Notification>, List<NotificationResponseDto>>(pagedNotifications.Items);
            
            return new PagedResult<NotificationResponseDto>
            {
                Items = notificationDtos,
                PageNumber = pagedNotifications.PageNumber,
                PageSize = pagedNotifications.PageSize,
                TotalCount = pagedNotifications.TotalCount
            };
        }

        public async Task<List<NotificationResponseDto>> GetUnreadNotificationsAsync(int userId)
        {
            var unreadNotifications = await _notificationRepo.GetUnreadAsync(userId);
            var notificationDtos = _mapper.Map<List<Notification>, List<NotificationResponseDto>>(unreadNotifications);
            return notificationDtos;
        }

        public async Task<int> GetUnreadCountAsync(int userId)
        {
            var count = await _notificationRepo.GetUnreadCountAsync(userId);
            return count;
        }

        public async Task<bool> MarkAllAsReadAsync(int userId)
        {
            var unreadNotifications = await _notificationRepo.GetUnreadAsync(userId);
            
            if (!unreadNotifications.Any())
                return true;

            foreach (var notification in unreadNotifications)
            {
                notification.IsRead = true;
            }

            var result = await _notificationRepo.BulkUpdateAsync(unreadNotifications);
            
            if (result)
            {
                // Broadcast unread count update via SignalR
                await _signalR.SendNotificationToUserAsync(userId, "All notifications marked as read");
            }
            
            return result;
        }

        public async Task<bool> MarkAsReadAsync(int userId, int notificationId)
        {
            var notification = await _notificationRepo.GetByIdAsync(userId, notificationId);
            
            if (notification == null)
                return false;

            notification.IsRead = true;
            var result = await _notificationRepo.UpdateAsync(notification);
            
            if (result)
            {
                // Broadcast notification update via SignalR
                await _signalR.SendNotificationToUserAsync(userId, $"Notification {notificationId} marked as read");
            }
            
            return result;
        }

        public async Task NotifySystemAnnouncementAsync(string title, string message)
        {
            // This would need a list of all active users
            // For now, sending to all via SignalR
            await _signalR.SendNotificationToAllAsync($"{title}: {message}");
        }

        public async Task NotifyWelcomeMessageAsync(int userId, string userName)
        {
            var notificationDto = new NotificationCreateDto
            {
                UserId = userId,
                ActorId = userId,
                Type = Models.Enums.NotificationType.SystemAnnouncement,
                EntityTargetedId = null,
                Message = $"Welcome to the platform, {userName}! We're excited to have you here."
            };

            await CreateNotificationAsync(notificationDto);
        }

        public async Task SendNotificationToAllAsync(NotificationResponseDto notification)
        {
            var message = $"{notification.Message}";
            await _signalR.SendNotificationToAllAsync(message);
        }

        public async Task SendNotificationToUserAsync(int userId, NotificationResponseDto notification)
        {
            var message = $"{notification.Message}";
            await _signalR.SendNotificationToUserAsync(userId, message);
        }

        public async Task SendNotificationToUsersAsync(List<int> userIds, NotificationResponseDto notification)
        {
            var message = $"{notification.Message}";
            
            foreach (var userId in userIds)
            {
                await _signalR.SendNotificationToUserAsync(userId, message);
            }
        }
    }
}