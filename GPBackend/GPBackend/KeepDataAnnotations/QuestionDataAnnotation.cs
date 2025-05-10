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

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Application Application { get; set; } = null!;
    }
} 