using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GPBackend.Models.Enums;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(ApplicationMetaData))]
    public partial class Application
    {
    }

    public class ApplicationMetaData
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("UserCompany")]
        public int UserCompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string JobTitle { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string JobType { get; set; } = null!;

        public string? Description { get; set; }

        [StringLength(255)]
        public string? Link { get; set; }

        [ForeignKey("Resume")]
        public int? SubmittedCvId { get; set; }

        [Range(0, 100)]
        public int? AtsScore { get; set; }

        [Required]
        public ApplicationStage Stage { get; set; }

        [Required]
        public ApplicationDecisionStatus Status { get; set; }

        public DateOnly SubmissionDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] Rowversion { get; set; } = null!;

        public virtual ICollection<ApplicationEmployee> ApplicationEmployees { get; set; } = new List<ApplicationEmployee>();

        public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

        public virtual Resume? Resume { get; set; }

        public virtual UserCompany UserCompany { get; set; } = null!;
    }
} 