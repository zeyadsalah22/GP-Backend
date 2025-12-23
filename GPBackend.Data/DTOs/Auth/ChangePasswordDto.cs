using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Auth
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; } = null!;

        [Required(ErrorMessage = "New password is required")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Please confirm your new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match")]
        public string NewPasswordConfirm { get; set; } = null!;
    }
} 