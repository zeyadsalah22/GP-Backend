using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GPBackend.DTOs.InterviewFeedback
{
    public class GradeInterviewQuestionsBatchRequestDto
    {
        [Required]
        [JsonPropertyName("interviewQuestionIds")]
        public List<int> InterviewQuestionIds { get; set; } = new();

        [JsonPropertyName("context")]
        public string? Context { get; set; }
    }
}


