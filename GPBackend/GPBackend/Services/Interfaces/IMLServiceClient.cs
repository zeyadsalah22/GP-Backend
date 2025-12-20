namespace GPBackend.Services.Interfaces
{
    public interface IMLServiceClient
    {
        Task<List<string>> GenerateQuestionsAsync(string description, int numQuestions = 3);
        Task<ResumeMatchingResponse> MatchResumeAsync(string base64Resume, string jobDescription);
        Task<bool> HealthCheckAsync();
    }

    public class ResumeMatchingResponse
    {
        public List<string> MatchedSkills { get; set; } = new();
        public List<string> MissingSkills { get; set; } = new();
        public double ResumeScore { get; set; }
    }
}

