namespace GPBackend.DTOs.ResumeTest
{
    public class ResumeTestAIDto
    {
        public double ResumeScore { get; set; } // ATS score as double (0-1)
        public List<string> MissingSkills { get; set; } = new();
        public List<string> MatchingSkills { get; set; } = new();
    }
} 