using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(InterviewVideoFeedbackMetaData))]
    public partial class InterviewVideoFeedback
    {
    }

    public class InterviewVideoFeedbackMetaData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Interview")]
        public int InterviewId { get; set; }

        [Range(0, 100)]
        public double Confidence { get; set; }

        [Range(0, 100)]
        public double Engagement { get; set; }

        [Range(0, 100)]
        public double Stress { get; set; }

        [Range(0, 100)]
        public double Authenticity { get; set; }

        public string? Summary { get; set; }

        public string? StrengthsJson { get; set; }
        public string? ImprovementsJson { get; set; }
        public string? RecommendationsJson { get; set; }

        [StringLength(512)]
        public string? ReportPath { get; set; }

        public string? RawResponseJson { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Interview Interview { get; set; } = null!;
    }
}


