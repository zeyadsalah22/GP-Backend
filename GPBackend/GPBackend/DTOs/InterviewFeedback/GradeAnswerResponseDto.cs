namespace GPBackend.DTOs.InterviewFeedback
{
    // FastAPI response DTO
    public class GradeAnswerResponseDto
    {
        public double Score { get; set; }
        public string Feedback { get; set; } = string.Empty;
        public List<string> Strengths { get; set; } = new();
        public List<string> Improvements { get; set; } = new();
    }
}


