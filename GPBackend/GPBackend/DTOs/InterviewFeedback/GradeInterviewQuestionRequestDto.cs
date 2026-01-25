using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GPBackend.DTOs.InterviewFeedback
{
    public class GradeInterviewQuestionRequestDto
    {
        [Required]
        [JsonPropertyName("interviewQuestionId")]
        public int InterviewQuestionId { get; set; }

        [JsonPropertyName("context")]
        public string? Context { get; set; }
    }
}


