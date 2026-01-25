namespace GPBackend.DTOs.InterviewFeedback
{
    // FastAPI response result item DTO
    public class GradeAnswersBatchResultDto
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public double Score { get; set; }
        public string Feedback { get; set; } = string.Empty;
    }
}


