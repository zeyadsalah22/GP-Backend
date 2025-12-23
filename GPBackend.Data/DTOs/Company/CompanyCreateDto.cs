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

        [Required]
        public int IndustryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanySize { get; set; } = null!;
        //public Models.Enums.CompanySize CompanySize { get; set; }

        public string? Description { get; set; }

        //public byte[]? Logo { get; set; }
        [StringLength(255)]
        [Url]
        public string? LogoUrl { get; set; }
    }
} 