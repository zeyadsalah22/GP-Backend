using System.Text.Json.Serialization;

namespace GPBackend.DTOs.InterviewFeedback
{
    // FastAPI response DTO
    public class GradeAnswersBatchResponseDto
    {
        public List<GradeAnswersBatchResultDto> Results { get; set; } = new();

        [JsonPropertyName("total_graded")]
        public int TotalGraded { get; set; }
    }
}


