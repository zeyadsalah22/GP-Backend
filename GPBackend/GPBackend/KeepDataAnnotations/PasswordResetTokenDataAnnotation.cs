using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using GPBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Models{
    [ModelMetadataType(typeof(PasswordResetTokenMetaData))]
    public partial class PasswordResetToken
    {
    }

    public class PasswordResetTokenMetaData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public byte[] TokenHash { get; set; } = Array.Empty<byte>();

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpiresAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? UsedAt { get; set; }

        [Required]
        public string? CreatedIp { get; set; }

        [Required]
        public string? CreatedUserAgent { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public User User { get; set; } = null!;
    }
}