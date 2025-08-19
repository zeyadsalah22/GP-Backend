using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.UserCompany
{
    public class UserCompanyCreateDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyId { get; set; }

        public string? PersonalNotes { get; set; }

        [Required]
        public GPBackend.Models.Enums.InterestLevel InterestLevel { get; set; }

        public bool Favorite { get; set; }
    }
} 