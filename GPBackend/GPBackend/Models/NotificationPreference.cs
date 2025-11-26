using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPBackend.Models
{
    public partial class NotificationPreference
    {
        [Key]
        public int NotificationPreferenceId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public bool EnableReminders { get; set; } = true;
        public bool EnableSystem { get; set; } = true;
        public bool EnableSocial { get; set; } = true;
        public bool GloballyEnabled { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User? User { get; set; }
    }
}