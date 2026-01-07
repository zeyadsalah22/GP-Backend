using System.Security.Claims;
using GPBackend.DTOs.Notification;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;

namespace GPBackend.BackgoundServices
{
    public class NotificationTriggeringService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificationTriggeringService> _logger;
        private readonly TimeSpan _period = TimeSpan.FromHours(24); // Run daily
        private const int DUEDAYSFORAPPLICATIONS = 2;
        private const int DUEDAYSFORINTERVIEWS = 2;

        public NotificationTriggeringService(
            IServiceProvider serviceProvider, 
            ILogger<NotificationTriggeringService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Start notification triggering service excution");

                try
                {
                    // check applications deadline -- create notification in case time less than 2 days
                    // check todo items deadline -- in the next 24 hours due
                    // check scheduled mock interview -- in the next 24 hours due
                    using var scope = _serviceProvider.CreateScope();
                    var _notificationRepo = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
                    var _notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                    
                    // Create a fresh list for each execution to prevent accumulation
                    var notifications = new List<NotificationCreateDto>();
                    
                    await CreateApplicationsNotification(_notificationRepo, notifications);
                    await CreateInterviewsNotification(_notificationRepo, notifications);
                    
                    if (notifications.Any())
                    {
                        await _notificationService.CreateBulkNotificationsAsync(notifications);
                        _logger.LogInformation($"Sent {notifications.Count} notification(s)");
                    }
                    else
                    {
                        _logger.LogInformation("No notifications to send");
                    }
                    
                    _logger.LogInformation("Finish notification triggering service excution");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while running Notification triggering srvice");
                }

                await Task.Delay(_period, stoppingToken);
            }
        }

        private async Task CreateInterviewsNotification(INotificationRepository _notificationRepo, List<NotificationCreateDto> notifications)
        {
            List<Interview> dueInterviews = await _notificationRepo.GetInterviewsInDueDaysAsync(DUEDAYSFORINTERVIEWS);
            int addedCount = 0;
            
            foreach (var item in dueInterviews)
            {
                var daysUntil = (int)Math.Ceiling((item.StartDate - DateTime.Now).TotalDays);
                var timeDescription = daysUntil <= 1 ? $"in {daysUntil} day" : $"in {daysUntil} days";
                var message = $"Your mock interview for position {item.Position} is scheduled {timeDescription} at {item.StartDate:g}";
                
                // Check if this exact notification was already sent within the last 24 hours
                bool exists = await _notificationRepo.NotificationExistsAsync(
                    item.UserId, 
                    item.InterviewId, 
                    Models.Enums.NotificationType.Interview, 
                    24,
                    message); // Check for exact message match
                
                if (!exists)
                {
                    var NotificationCreateDto = new NotificationCreateDto
                    {
                        UserId = item.UserId,
                        ActorId = 2,
                        EntityTargetedId = item.InterviewId,
                        Type = Models.Enums.NotificationType.Interview,
                        Message = message
                    };
                    notifications.Add(NotificationCreateDto);
                    addedCount++;
                }
            }
            _logger.LogInformation($"Added {addedCount} new interview notification reminders (skipped {dueInterviews.Count - addedCount} duplicates)");
        }

        private async Task CreateApplicationsNotification(INotificationRepository _notificationRepo, List<NotificationCreateDto> notifications)
        {
            List<TodoList> dueApplications = await _notificationRepo.GetApplicationsInDueDaysAsync(DUEDAYSFORAPPLICATIONS);
            int addedCount = 0;
            
            foreach (var item in dueApplications)
            {
                var daysUntil = (int)Math.Ceiling((item.Deadline!.Value - DateTime.Now).TotalDays);
                var timeDescription = daysUntil <= 1 ? $"{daysUntil} day" : $"{daysUntil} days";
                var message = $"Application '{item.ApplicationTitle}' is due in {timeDescription}. Hurry up and submit! {item.ApplicationLink}";
                
                // Check if this exact notification was already sent within the last 24 hours
                bool exists = await _notificationRepo.NotificationExistsAsync(
                    item.UserId, 
                    null, 
                    Models.Enums.NotificationType.TodoItem, 
                    24,
                    message); // Check for exact message match
                
                if (!exists)
                {
                    var NotificationCreateDto = new NotificationCreateDto
                    {
                        UserId = item.UserId,
                        ActorId = 2,
                        Type = Models.Enums.NotificationType.TodoItem,
                        Message = message
                    };
                    notifications.Add(NotificationCreateDto);
                    addedCount++;
                }
            }
            _logger.LogInformation($"Added {addedCount} new application notification reminders (skipped {dueApplications.Count - addedCount} duplicates)");
        }
    }
} 