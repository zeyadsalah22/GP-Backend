using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GPBackend.Models.Enums;

namespace GPBackend.Models;

/// <summary>
/// Audit trail for all email-triggered application updates
/// Provides transparency and debugging capability
/// </summary>
public partial class EmailApplicationUpdate
{
    [Key]
    public int EmailApplicationUpdateId { get; set; }

    /// <summary>
    /// The application that was (or should be) updated
    /// </summary>
    [Required]
    public int ApplicationId { get; set; }

    /// <summary>
    /// The user who owns the application
    /// </summary>
    [Required]
    public int UserId { get; set; }

    // Email metadata
    
    /// <summary>
    /// Gmail message ID (unique identifier from Gmail API)
    /// </summary>
    [Required]
    [StringLength(255)]
    public string EmailId { get; set; } = null!;

    /// <summary>
    /// Email subject line
    /// </summary>
    [Required]
    [StringLength(500)]
    public string EmailSubject { get; set; } = null!;

    /// <summary>
    /// Email sender address
    /// </summary>
    [Required]
    [StringLength(255)]
    public string EmailFrom { get; set; } = null!;

    /// <summary>
    /// When the email was received (from Gmail)
    /// </summary>
    [Required]
    public DateTime EmailDate { get; set; }

    /// <summary>
    /// Brief snippet/preview of email content
    /// </summary>
    [StringLength(1000)]
    public string? EmailSnippet { get; set; }

    // Detection metadata
    
    /// <summary>
    /// Status detected by n8n parsing logic
    /// </summary>
    public ApplicationDecisionStatus? DetectedStatus { get; set; }

    /// <summary>
    /// Stage detected by n8n parsing logic
    /// </summary>
    public ApplicationStage? DetectedStage { get; set; }

    /// <summary>
    /// Confidence level of detection (0.0 - 1.0)
    /// </summary>
    [Range(0.0, 1.0)]
    public decimal Confidence { get; set; }

    /// <summary>
    /// Company name extracted from email sender domain
    /// </summary>
    [StringLength(255)]
    public string? CompanyNameHint { get; set; }

    /// <summary>
    /// Human-readable explanation of why this email was matched
    /// Used for debugging and transparency
    /// </summary>
    [StringLength(1000)]
    public string? MatchReasons { get; set; }

    // Update status
    
    /// <summary>
    /// Whether the application was successfully updated
    /// False if we couldn't match to an application
    /// </summary>
    public bool WasApplied { get; set; } = false;

    /// <summary>
    /// If not applied, reason why (e.g., "No matching application found")
    /// </summary>
    [StringLength(500)]
    public string? FailureReason { get; set; }

    /// <summary>
    /// When this update record was created (UTC)
    /// </summary>
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    [ForeignKey("ApplicationId")]
    public virtual Application Application { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}

