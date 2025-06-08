using System.ComponentModel.DataAnnotations;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.User
{
    public class ChangeUserRoleDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public UserRole Role { get; set; }
    }
} 