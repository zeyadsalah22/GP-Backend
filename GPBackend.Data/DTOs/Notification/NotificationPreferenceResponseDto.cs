namespace GPBackend.DTOs.Notification
{
    public class NotificationPreferenceResponseDto
    {
        public int NotificationPreferenceId { get; set; }
        public int UserId { get; set; }
        public bool EnableReminders { get; set; }
        public bool EnableSystem { get; set; }
        public bool EnableSocial { get; set; }
        public bool GloballyEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}