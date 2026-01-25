namespace GPBackend.DTOs.InterviewFeedback
{
    public class InterviewQuestionFeedbackResponseDto
    {
        public int InterviewId { get; set; }
        public int InterviewQuestionId { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;

        public double Score { get; set; }
        public string Feedback { get; set; } = string.Empty;
        public List<string> Strengths { get; set; } = new();
        public List<string> Improvements { get; set; } = new();

        public string? Context { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}


