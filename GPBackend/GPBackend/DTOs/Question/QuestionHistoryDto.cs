namespace GPBackend.DTOs.Question
{
    public class QuestionHistoryDto
    {
        public string QuestionId { get; set; } = null!;
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public string? JobTitle { get; set; }
        public string? CompanyName { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}

