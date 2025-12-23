using GPBackend.Models;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Notification
{
    public class NotificationUpdateDto
    {        
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public bool IsRead { get; set; }
    }
}