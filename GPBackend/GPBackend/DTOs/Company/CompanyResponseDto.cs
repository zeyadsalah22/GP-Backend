namespace GPBackend.DTOs.Company
{
    public class CompanyResponseDto
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public string? CareersLink { get; set; }
        public string? LinkedinLink { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 