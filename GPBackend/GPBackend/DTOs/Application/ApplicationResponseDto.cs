using System;

namespace GPBackend.DTOs.Application
{
    public class ApplicationResponseDto
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public string JobType { get; set; } = null!;
        public string? Description { get; set; }
        public string? Link { get; set; }
        public int? SubmittedCvId { get; set; }
        public int? AtsScore { get; set; }
        public string Stage { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateOnly SubmissionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 