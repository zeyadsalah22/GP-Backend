using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Company
{
    public class CompanyCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        public string? Location { get; set; }

        [StringLength(255)]
        [Url]
        public string? CareersLink { get; set; }

        [StringLength(255)]
        [Url]
        public string? LinkedinLink { get; set; }
    }
} 