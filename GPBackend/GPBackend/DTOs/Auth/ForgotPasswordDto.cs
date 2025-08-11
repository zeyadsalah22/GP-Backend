using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Auth{

    public class ForgotPasswordDto{
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}