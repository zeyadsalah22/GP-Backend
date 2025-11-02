namespace GPBackend.DTOs.Gmail
{
    /// <summary>
    /// DTO for returning result of email processing to n8n
    /// </summary>
    public class EmailProcessingResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public int? ApplicationId { get; set; }
        public bool WasApplied { get; set; }
        public string? FailureReason { get; set; }
    }
}

