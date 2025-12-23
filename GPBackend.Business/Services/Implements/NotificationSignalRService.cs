using GPBackend.DTOs.Notification;
using GPBackend.hubs;
using GPBackend.Hubs;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace GPBackend.Services.Implements
{
    public class NotificationSignalRService : INotificationSignalRService
    {
        private readonly IHubContext<NotificationHub> _hub;
        private readonly IUserConnectionRepository _userConnectionRepo;

        public NotificationSignalRService(
            IHubContext<NotificationHub> hub,
            IUserConnectionRepository userConnectionRepo)
        {
            _hub = hub;
            _userConnectionRepo = userConnectionRepo;
        }
        
        public async Task SendNotificationToAllAsync(string Message)
        {
            await _hub.Clients.All.SendAsync(HubMethods.SENDTOALL, Message);
        }

        public async Task SendNotificationToUserAsync(int UserId, string Message)
        {
            var Connection = await _userConnectionRepo.GetConnectionByUserIdAsync(UserId);
            if (Connection == null)
            {
                Console.WriteLine($"⚠️  No connection found for user {UserId}");
                return;
            }

            await _hub.Clients.Client(Connection.ConnectionId).SendAsync(HubMethods.SENDTOUSER, Message);

        }

        //TODO: Improve the method used to send each client individually, maybe fetch all connections first then map
        public async Task SendNotificationToUsersAsync(List<NotificationCreateDto> notifications)
        {
            foreach(var notification in notifications)
            {
                await SendNotificationToUserAsync(notification.UserId, notification.Message);
            }
        }

    }
}