using GPBackend.DTOs.Industry;

namespace GPBackend.DTOs.Company
{
    public class CompanyResponseDto
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public string? CareersLink { get; set; }
        public string? LinkedinLink { get; set; }
        public int IndustryId { get; set; }
        public Models.Enums.CompanySize CompanySize { get; set; }
        public string? Description { get; set; }
        public byte[]? Logo { get; set; }
        public IndustryResponseDto? Industry { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 