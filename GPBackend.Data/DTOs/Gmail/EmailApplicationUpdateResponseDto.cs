using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Gmail
{
    /// <summary>
    /// DTO for returning email application update details (audit trail)
    /// </summary>
    public class EmailApplicationUpdateResponseDto
    {
        public int EmailApplicationUpdateId { get; set; }
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        
        // Email metadata
        public string EmailId { get; set; } = null!;
        public string EmailSubject { get; set; } = null!;
        public string EmailFrom { get; set; } = null!;
        public DateTime EmailDate { get; set; }
        public string? EmailSnippet { get; set; }
        
        // Detection metadata
        public ApplicationDecisionStatus? DetectedStatus { get; set; }
        public ApplicationStage? DetectedStage { get; set; }
        public decimal Confidence { get; set; }
        public string? CompanyNameHint { get; set; }
        public string? MatchReasons { get; set; }
        
        // Update status
        public bool WasApplied { get; set; }
        public string? FailureReason { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        // Include application details for context
        public string? ApplicationJobTitle { get; set; }
        public string? ApplicationCompanyName { get; set; }
    }
}

