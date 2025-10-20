using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Notification
{
    public class NotificationResponseDto
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int ActorId { get; set; }
        public NotificationType Type { get; set; }
        public int? EntityTargetedId { get; set; }
        public required string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}