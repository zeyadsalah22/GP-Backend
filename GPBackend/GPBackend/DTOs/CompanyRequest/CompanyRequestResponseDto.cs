using System;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.CompanyRequest
{
    public class CompanyRequestResponseDto
    {
        public int RequestId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string? Location { get; set; }

        public int? IndustryId { get; set; }

        public string? IndustryName { get; set; }

        public string? LinkedinLink { get; set; }

        public string? CareersLink { get; set; }

        public string? Description { get; set; }

        public CompanyRequestStatus RequestStatus { get; set; }

        public DateTime RequestedAt { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public int? ReviewedByAdminId { get; set; }

        public string? ReviewedByAdminName { get; set; }

        public string? RejectionReason { get; set; }
    }
}

