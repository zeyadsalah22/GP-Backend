using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Question
{
    public class QuestionResponseDto
    {
        public int QuestionId { get; set; }

        public string Question1 { get; set; } = null!;

        public string? Answer { get; set; }

        public int ApplicationId { get; set; }

        public QuestionType? Type { get; set; }

        public AnswerStatus? AnswerStatus { get; set; }

        public int? Difficulty { get; set; }

        public string? PreparationNote { get; set; }

        public bool Favorite { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<string> Tags { get; set; } = new();
    }
}