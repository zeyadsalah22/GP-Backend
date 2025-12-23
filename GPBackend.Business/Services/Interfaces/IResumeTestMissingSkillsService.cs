using GPBackend.DTOs.ResumeTest;

namespace GPBackend.Services.Interfaces
{
    public interface IResumeTestMissingSkillsService
    {
        /// <summary>
        /// Analyze resume against job description using AI model
        /// Returns ATS score (as integer) and skills analysis
        /// </summary>
        Task<ResumeTestAIDto?> AnalyzeResumeAsync(byte[] resumeFile, string jobDescription);

        /// <summary>
        /// Get ATS score for a specific resume test
        /// </summary>
        Task<int> GetAtsScoreAsync(int testId);

        /// <summary>
        /// Update ATS score for a resume test (converts double to integer)
        /// </summary>
        Task<bool> UpdateAtsScoreAsync(int testId, double score);

        /// <summary>
        /// Get missing skills for a specific resume test from Skills table
        /// </summary>
        Task<List<string>> GetMissingSkillsAsync(int testId);

        /// <summary>
        /// Store missing skills in Skills table for a resume test
        /// </summary>
        Task<bool> StoreMissingSkillsAsync(int testId, List<string> missingSkills);

        /// <summary>
        /// Delete all skills for a specific resume test
        /// </summary>
        Task<bool> DeleteSkillsAsync(int testId);

        /// <summary>
        /// Get complete AI analysis results for a resume test
        /// </summary>
        Task<ResumeTestAIDto?> GetResumeTestAnalysisAsync(int testId);
    }
}