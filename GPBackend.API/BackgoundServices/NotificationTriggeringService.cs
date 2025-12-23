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
        private readonly List<NotificationCreateDto> notifications = new List<NotificationCreateDto>();
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
                    
                    await CreateApplicationsNotification(_notificationRepo);
                    await CreateInterviewsNotification(_notificationRepo);
                    await _notificationService.CreateBulkNotificationsAsync(notifications);
                    _logger.LogInformation("Finish notification triggering service excution");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while running Notification triggering srvice");
                }

                await Task.Delay(_period, stoppingToken);
            }
        }

        private async Task CreateInterviewsNotification(INotificationRepository _notificationRepo)
        {
            List<Interview> dueInterviews = await _notificationRepo.GetInterviewsInDueDaysAsync(DUEDAYSFORINTERVIEWS);
            foreach (var item in dueInterviews)
            {
                var NotificationCreateDto = new NotificationCreateDto
                {
                    UserId = item.UserId,
                    ActorId = 2,
                    EntityTargetedId = item.InterviewId,
                    Type = Models.Enums.NotificationType.Interview,
                    Message = $"Your mock interview for position {item.Position} scheduled at {item.StartDate} is due {DUEDAYSFORINTERVIEWS} days"
                };
                notifications.Add(NotificationCreateDto);
                _logger.LogInformation($"Added {notifications.Count} interview notification reminders");
            }
        }

        private async Task CreateApplicationsNotification(INotificationRepository _notificationRepo)
        {
            List<TodoList> dueApplications = await _notificationRepo.GetApplicationsInDueDaysAsync(DUEDAYSFORAPPLICATIONS);
            foreach (var item in dueApplications)
            {
                var NotificationCreateDto = new NotificationCreateDto
                {
                    UserId = item.UserId,
                    ActorId = 2,
                    Type = Models.Enums.NotificationType.TodoItem,
                    Message = $"Application Titled {item.ApplicationTitle} is due in {DUEDAYSFORAPPLICATIONS}, hurry up and submit the application. Here is the link {item.ApplicationLink}"
                };
                notifications.Add(NotificationCreateDto);
                _logger.LogInformation($"Added {notifications.Count} Applications notification reminders");
            }
        }
    }
} 