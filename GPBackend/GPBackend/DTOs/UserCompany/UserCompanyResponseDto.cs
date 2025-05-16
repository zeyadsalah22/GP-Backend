
namespace GPBackend.DTOs.UserCompany
{
    public class UserCompanyResponseDto
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Include company details
        public string CompanyName { get; set; } = null!;
        public string? CompanyLocation { get; set; }
        public string? CompanyCareersLink { get; set; }
        public string? CompanyLinkedinLink { get; set; }
    }
} 