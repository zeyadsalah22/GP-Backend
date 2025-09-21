using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Company
{
    public class CompanyUpdateDto
    {
        [Required]
        [StringLength(255, MinimumLength = 2)]
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
        public Models.Enums.CompanySize CompanySize { get; set; }

        public string? Description { get; set; }

        public byte[]? Logo { get; set; }
    }
} 