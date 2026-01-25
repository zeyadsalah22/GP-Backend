using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GPBackend.DTOs.InterviewFeedback
{
    // FastAPI request DTO
    public class GradeAnswerRequestDto
    {
        [Required]
        [JsonPropertyName("question")]
        public string Question { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("answer")]
        public string Answer { get; set; } = string.Empty;

        [JsonPropertyName("context")]
        public string? Context { get; set; }
    }
}


