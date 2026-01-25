using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GPBackend.DTOs.InterviewFeedback
{
    // FastAPI request item DTO
    public class GradeAnswersBatchItemDto
    {
        [Required]
        [JsonPropertyName("question")]
        public string Question { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("answer")]
        public string Answer { get; set; } = string.Empty;
    }
}


