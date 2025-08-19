
namespace GPBackend.DTOs.UserCompany
{
    public class UserCompanyResponseDto
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string? PersonalNotes { get; set; }
        public GPBackend.Models.Enums.InterestLevel? InterestLevel { get; set; }
        public bool Favorite { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string>? Tags { get; set; }
        
        // Include company details
        public string CompanyName { get; set; } = null!;
        public string? CompanyLocation { get; set; }
        public string? CompanyCareersLink { get; set; }
        public string? CompanyLinkedinLink { get; set; }
    }
} 