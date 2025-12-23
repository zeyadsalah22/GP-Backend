using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Notification
{
    public class NotificationPreferenceUpdateDto
    {
        public int UserId { get; set; }
        public bool? EnableReminders { get; set; }
        
        public bool? EnableSystem { get; set; }
        
        public bool? EnableSocial { get; set; }
        
        public bool? GloballyEnabled { get; set; }
    }
}