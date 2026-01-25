using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(InterviewQuestionFeedbackMetaData))]
    public partial class InterviewQuestionFeedback
    {
    }

    public class InterviewQuestionFeedbackMetaData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("InterviewQuestion")]
        public int InterviewQuestionId { get; set; }

        [Range(0, 5)]
        public double Score { get; set; }

        [Required]
        public string Feedback { get; set; } = string.Empty;

        public string? StrengthsJson { get; set; }

        public string? ImprovementsJson { get; set; }

        public string? Context { get; set; }

        public string? RawResponseJson { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public virtual InterviewQuestion InterviewQuestion { get; set; } = null!;
    }
}


