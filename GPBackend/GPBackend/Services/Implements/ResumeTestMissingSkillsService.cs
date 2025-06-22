using GPBackend.DTOs.ResumeTest;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using System.Text.Json;

namespace GPBackend.Services.Implements
{
    public class ResumeTestMissingSkillsService : IResumeTestMissingSkillsService
    {
        private readonly IResumeTestRepository _resumeTestRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly string ModelApiURL = "http://localhost:8000/resume-matching";

        public ResumeTestMissingSkillsService(IResumeTestRepository resumeTestRepository,
                                             ISkillRepository skillRepository)
        {
            _resumeTestRepository = resumeTestRepository;
            _skillRepository = skillRepository;
        }

        public async Task<ResumeTestAIDto?> AnalyzeResumeAsync(byte[] resumeFile, string jobDescription)
        {
            try
            {
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(2); // 2 minute timeout

                // Convert resume file to base64 string
                var resumeBase64 = Convert.ToBase64String(resumeFile);

                var requestData = new
                {
                    resume_file = resumeBase64,
                    job_description = jobDescription
                };

                var response = await client.PostAsJsonAsync(ModelApiURL, requestData);
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to fetch resume matching from the model API. Status: {response.StatusCode}");
                }

                var result = await response.Content.ReadFromJsonAsync<ResumeTestAIDto>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                if (result == null)
                {
                    throw new Exception("No result was returned from the model API.");
                }

                // Convert double score to integer (0-100 range)
                result.Score = ConvertDoubleToInteger(result.Score);

                return result;
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("AI model request timed out.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling AI model: {ex.Message}");
                return null;
            }
        }

        public async Task<int> GetAtsScoreAsync(int testId)
        {
            var resumeTest = await _resumeTestRepository.GetResumeTestByIdAsync(testId, 0); // 0 for admin access
            return resumeTest?.AtsScore ?? 0;
        }

        public async Task<bool> UpdateAtsScoreAsync(int testId, double score)
        {
            var resumeTest = await _resumeTestRepository.GetResumeTestByIdAsync(testId, 0); // 0 for admin access
            if (resumeTest == null)
            {
                return false;
            }

            // Convert double to integer (0-100 range)
            resumeTest.AtsScore = ConvertDoubleToInteger(score);

            return await _resumeTestRepository.UpdateResumeTestAsync(resumeTest);
        }

        public async Task<List<string>> GetMissingSkillsAsync(int testId)
        {
            var skills = await _skillRepository.GetByTestIdAsync(testId);
            return skills.Select(s => s.Skill1).ToList();
        }

        public async Task<bool> StoreMissingSkillsAsync(int testId, List<string> missingSkills)
        {
            try
            {
                // Delete existing skills for this test
                await DeleteSkillsAsync(testId);

                // Create new skills
                if (missingSkills != null && missingSkills.Count > 0)
                {
                    var skills = missingSkills.Select(skill => new Skill
                    {
                        TestId = testId,
                        Skill1 = skill
                    }).ToList();

                    await _skillRepository.CreateAsync(skills);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error storing missing skills: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSkillsAsync(int testId)
        {
            try
            {
                var existingSkills = await _skillRepository.GetByTestIdAsync(testId);
                foreach (var skill in existingSkills)
                {
                    await _skillRepository.DeleteAsync(skill.SkillId);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting skills: {ex.Message}");
                return false;
            }
        }

        public async Task<ResumeTestAIDto?> GetResumeTestAnalysisAsync(int testId)
        {
            var resumeTest = await _resumeTestRepository.GetResumeTestByIdAsync(testId, 0); // 0 for admin access
            if (resumeTest == null)
            {
                return null;
            }

            var missingSkills = await GetMissingSkillsAsync(testId);

            return new ResumeTestAIDto
            {
                Score = resumeTest.AtsScore,
                MissingSkills = missingSkills,
                MatchingSkills = new List<string>() // Not stored in database, would need to be calculated
            };
        }

        private int ConvertDoubleToInteger(double score)
        {
            // Round to nearest integer and ensure 0-100 range
            return Math.Max(0, Math.Min(100, (int)Math.Round(score)));
        }
    }
} 