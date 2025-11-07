using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPBackend.Models
{
    public class UserConnection
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(255)]
        public required string ConnectionId { get; set; }
        
        public DateTime ConnectedAt { get; set; }
        
        // Navigation property
        public virtual User? User { get; set; }
    }
}