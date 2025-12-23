using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPBackend.Models;

public partial class GmailConnection
{
    [Key]
    public int GmailConnectionId { get; set; }

    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Encrypted OAuth access token for Gmail API
    /// </summary>
    [Required]
    public string EncryptedAccessToken { get; set; } = null!;

    /// <summary>
    /// Encrypted OAuth refresh token for Gmail API
    /// </summary>
    [Required]
    public string EncryptedRefreshToken { get; set; } = null!;

    /// <summary>
    /// When the access token expires (UTC)
    /// </summary>
    [Required]
    public DateTime TokenExpiresAt { get; set; }

    /// <summary>
    /// User's Gmail address (e.g., user@gmail.com)
    /// </summary>
    [Required]
    [StringLength(255)]
    public string GmailAddress { get; set; } = null!;

    /// <summary>
    /// Last time we checked this user's Gmail for new emails (UTC)
    /// </summary>
    public DateTime? LastCheckedAt { get; set; }

    /// <summary>
    /// Gmail History ID for tracking incremental changes (used with Gmail Push notifications)
    /// </summary>
    [StringLength(50)]
    public string? HistoryId { get; set; }

    /// <summary>
    /// When the Gmail watch expires (UTC) - watches last 7 days
    /// </summary>
    public DateTime? WatchExpiresAt { get; set; }

    /// <summary>
    /// Whether this connection is active and should be monitored
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// When the user connected their Gmail account (UTC)
    /// </summary>
    public DateTime ConnectedAt { get; set; }

    /// <summary>
    /// Last time this record was updated (UTC)
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Soft delete flag
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Optimistic concurrency control
    /// </summary>
    [Timestamp]
    public byte[] Rowversion { get; set; } = null!;

    // Navigation property
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    // Helper property
    public bool IsTokenExpired => DateTime.UtcNow >= TokenExpiresAt;
}

