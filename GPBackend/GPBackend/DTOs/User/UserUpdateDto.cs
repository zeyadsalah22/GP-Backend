using System;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.User
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        public string Fname { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
        public string Lname { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters")]
        public string Email { get; set; } = null!;
        
        public string? Address { get; set; }
        
        public DateOnly? BirthDate { get; set; }
    }
} 