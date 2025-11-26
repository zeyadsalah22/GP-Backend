using GPBackend.Models;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Notification
{
    public class NotificationCreateDto
    {        
        public int UserId { get; set; }
        
        public int ActorId { get; set; }
        
        public NotificationType Type { get; set; }
        
        public int? EntityTargetedId { get; set; }
        public NotificationCategory NotificationCategory { get; set; }
        
        public required string Message { get; set; }
    }
}