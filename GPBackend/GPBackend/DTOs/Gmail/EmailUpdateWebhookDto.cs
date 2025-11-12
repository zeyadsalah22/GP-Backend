using System.ComponentModel.DataAnnotations;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Gmail
{
    /// <summary>
    /// DTO for receiving email update webhook from n8n
    /// </summary>
    public class EmailUpdateWebhookDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public EmailDataDto EmailData { get; set; } = null!;

        public string? DetectedStatus { get; set; }  // String from n8n, will convert to enum

        public string? DetectedStage { get; set; }  // String from n8n, will convert to enum

        [Range(0.0, 1.0)]
        public decimal Confidence { get; set; }

        public string? CompanyNameHint { get; set; }

        /// <summary>
        /// ID of the matched application from pending applications (if found)
        /// </summary>
        public int? MatchedApplicationId { get; set; }

        /// <summary>
        /// Human-readable explanation of why this email was matched
        /// </summary>
        public string? MatchReasons { get; set; }
    }

    public class EmailDataDto
    {
        [Required]
        public string EmailId { get; set; } = null!;

        [Required]
        public string Subject { get; set; } = null!;

        [Required]
        public string From { get; set; } = null!;

        [Required]
        public string Date { get; set; } = null!;  // ISO 8601 string from n8n

        public string? Snippet { get; set; }
    }
}

