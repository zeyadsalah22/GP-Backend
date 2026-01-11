using System;
using GPBackend.Models.Enums;

namespace GPBackend.Models;

public partial class CompanyRequest
{
    public int RequestId { get; set; }

    public int UserId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Location { get; set; }

    public int? IndustryId { get; set; }

    public string? LinkedinLink { get; set; }

    public string? CareersLink { get; set; }

    public string? Description { get; set; }

    public CompanyRequestStatus RequestStatus { get; set; }

    public DateTime RequestedAt { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public int? ReviewedByAdminId { get; set; }

    public string? RejectionReason { get; set; }

    public bool IsDeleted { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;

    public virtual User? ReviewedByAdmin { get; set; }

    public virtual Industry? Industry { get; set; }
}

