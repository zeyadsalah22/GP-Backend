namespace GPBackend.DTOs.ResumeTest
{
    public class ResumeTestAIDto
    {
        public List<string> MissingSkills { get; set; } = new List<string>();
        public List<string> MatchingSkills { get; set; } = new List<string>();
        public double Score { get; set; }
    }
} 