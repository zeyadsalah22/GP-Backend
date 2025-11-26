using System;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.User
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public DateOnly? BirthDate { get; set; }
        public UserRole Role { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public int ReputationPoints { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 