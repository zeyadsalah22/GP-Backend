using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Question
{
    public class QuestionResponseDto
    {
        public int QuestionId { get; set; }

        public string Question1 { get; set; } = null!;

        public string? Answer { get; set; }

        public int ApplicationId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


    }
}