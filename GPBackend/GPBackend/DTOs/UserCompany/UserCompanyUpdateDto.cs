using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.UserCompany
{
    public class UserCompanyUpdateDto
    {
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = null!;
    }
} 