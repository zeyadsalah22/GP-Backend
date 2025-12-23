using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Gmail
{
    /// <summary>
    /// DTO for n8n to update last checked timestamp
    /// </summary>
    public class UpdateLastCheckedDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string LastCheckedAt { get; set; } = null!;  // ISO 8601 format from n8n
    }
}

