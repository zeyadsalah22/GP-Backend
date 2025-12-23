using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Gmail
{
    /// <summary>
    /// DTO for receiving OAuth callback code from frontend
    /// </summary>
    public class OAuthCallbackDto
    {
        [Required]
        public string Code { get; set; } = null!;
    }
}

