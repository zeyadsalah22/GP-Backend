using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Auth
{
    public class RefreshTokenDto
    {
        [Required(ErrorMessage = "Refresh token is required.")]
        public string RefreshToken { get; set; } = null!;
    }
} 