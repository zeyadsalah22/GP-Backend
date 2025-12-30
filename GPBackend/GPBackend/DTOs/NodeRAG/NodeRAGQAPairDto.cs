namespace GPBackend.DTOs.NodeRAG
{
    public class NodeRAGQAPairCreateDto
    {
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public string QuestionId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? JobTitle { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? SubmissionDate { get; set; }
    }

    public class NodeRAGQAPairResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public string QuestionHashId { get; set; } = null!;
        public string AnswerHashId { get; set; } = null!;
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public bool AddedToGraph { get; set; }
    }
}

