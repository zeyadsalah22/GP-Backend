namespace GPBackend.DTOs.ResumeTest
{
    public class ResumeTestAIDto
    {
        public int Score { get; set; } // ATS score as integer (0-100)
        public List<string> MissingSkills { get; set; } = new();
        public List<string> MatchingSkills { get; set; } = new();
    }
} 