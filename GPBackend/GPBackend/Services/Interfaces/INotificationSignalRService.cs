namespace GPBackend.Services.Interfaces
{
    public interface INotificationSignalRService
    {
        public Task SendNotificationToAllAsync(string Message);
        public Task SendNotificationToUserAsync(int UserId, string Message);

    }
}