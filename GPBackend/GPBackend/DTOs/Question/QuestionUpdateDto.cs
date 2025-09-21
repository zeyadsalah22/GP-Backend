using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Question
{
    public class QuestionUpdateDto
    {
        [Required(ErrorMessage = "Application ID field is required")]
        public int ApplicationId { get; set; }

        [StringLength(255, ErrorMessage ="Question length cannot exceed 255 character")]
        public string? Question1 { get; set; } = null!;

        [StringLength(4000, ErrorMessage = "Answer Length cannot exceed 4000 characters")]
        public string? Answer { get; set; }

        public QuestionType? Type { get; set; }

        public AnswerStatus? AnswerStatus { get; set; }

        [Range(1,5)]
        public int? Difficulty { get; set; }

        [StringLength(1000)]
        public string? PreparationNote { get; set; }

        public bool? Favorite { get; set; }

        public List<string>? Tags { get; set; }
    }
}