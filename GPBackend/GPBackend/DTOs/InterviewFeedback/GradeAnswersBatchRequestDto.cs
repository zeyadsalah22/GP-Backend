using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GPBackend.DTOs.InterviewFeedback
{
    // FastAPI request DTO
    public class GradeAnswersBatchRequestDto
    {
        [Required]
        [JsonPropertyName("items")]
        public List<GradeAnswersBatchItemDto> Items { get; set; } = new();

        [JsonPropertyName("context")]
        public string? Context { get; set; }
    }
}


