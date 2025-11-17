using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GPBackend.Models.Enums;

namespace GPBackend.Models
{
    public partial class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        [ForeignKey("Actor")]
        public int ActorId { get; set; }
        
        [Required]
        public NotificationType Type { get; set; }
        
        public int? EntityTargetedId { get; set; }
        
        [Required]
        public required string Message { get; set; }
        
        public bool IsRead { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public bool IsDeleted { get; set; }

        // Navigation Properties
        public virtual User? User { get; set; }
        public virtual User? Actor { get; set; }
    }
}