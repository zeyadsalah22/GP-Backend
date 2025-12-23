namespace GPBackend.DTOs.ResumeTest
{
    public class ResumeTestResponseDto
    {
        public int TestId { get; set; }
        public int ResumeId { get; set; }
        public int AtsScore { get; set; }
        public DateTime TestDate { get; set; }
        public string? JobDescription { get; set; }
        public List<string> MissingSkills { get; set; } = new List<string>();
        
    }
} 