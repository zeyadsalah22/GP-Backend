using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.Models;

public partial class PasswordResetToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public byte[] TokenHash { get; set; } = Array.Empty<byte>(); // store SHA-256 (32 bytes)
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedAt { get; set; }
    public string? CreatedIp { get; set; }
    public string? CreatedUserAgent { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public User User { get; set; } = null!;
}