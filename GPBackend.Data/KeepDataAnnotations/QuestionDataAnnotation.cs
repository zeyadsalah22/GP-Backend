using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(QuestionMetaData))]
    public partial class Question
    {
    }

    public class QuestionMetaData
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        public string Question1 { get; set; } = null!;

        public string? Answer { get; set; }

        [Required]
        [ForeignKey("Application")]
        public int ApplicationId { get; set; }

        public GPBackend.Models.Enums.QuestionType? Type { get; set; }

        public GPBackend.Models.Enums.AnswerStatus? AnswerStatus { get; set; }

        [Range(1,5, ErrorMessage = "Difficulty must be between 1 and 5")] 
        public int? Difficulty { get; set; }

        [StringLength(1000)]
        public string? PreparationNote { get; set; }

        public bool Favorite { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Application Application { get; set; } = null!;

        public virtual ICollection<QuestionTag> Tags { get; set; } = null!;
    }
} 