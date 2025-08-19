using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.UserCompany
{
    public class UserCompanyUpdateDto
    {
        public string? PersonalNotes { get; set; }
        public GPBackend.Models.Enums.InterestLevel? InterestLevel { get; set; }
        public bool? Favorite { get; set; }
    }
} 