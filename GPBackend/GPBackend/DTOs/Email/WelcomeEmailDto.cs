using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Email{
    public class WelcomeEmailDto{
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = null!;

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}