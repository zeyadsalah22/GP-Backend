using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(InterviewMetaData))]
    public partial class Interview
    {
    }

    public class InterviewMetaData
    {
        [Key]
        public int InterviewId { get; set; }

        [Required]
        [ForeignKey("Application")]
        public int ApplicationId { get; set; }

        [Required]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [StringLength(100)]
        public string? Position { get; set; }

        public string? Feedback { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int Duration { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Application Application { get; set; } = null!;

        public virtual Company Company { get; set; } = null!;

        public virtual ICollection<InterviewQuestion> InterviewQuestion { get; set; } = new List<InterviewQuestion>();

        public virtual User User { get; set; } = null!;
    }
} 