using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.CompanyRequest
{
    public class CompanyRequestCreateDto
    {
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
        public string CompanyName { get; set; } = null!;

        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        public string? Location { get; set; }

        public int? IndustryId { get; set; }

        [Url(ErrorMessage = "Please provide a valid LinkedIn URL")]
        [StringLength(255, ErrorMessage = "LinkedIn link cannot exceed 255 characters")]
        public string? LinkedinLink { get; set; }

        [Url(ErrorMessage = "Please provide a valid careers page URL")]
        [StringLength(255, ErrorMessage = "Careers link cannot exceed 255 characters")]
        public string? CareersLink { get; set; }

        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        public string? Description { get; set; }
    }
}

