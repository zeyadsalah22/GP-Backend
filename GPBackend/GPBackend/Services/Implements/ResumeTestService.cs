using AutoMapper;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.ResumeTest;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using System.Text.Json;

namespace GPBackend.Services.Implements
{
    public class ResumeTestService : IResumeTestService
    {
        private readonly IResumeTestRepository _resumeTestRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IResumeRepository _resumeRepository;
        private readonly IMapper _mapper;
        private readonly string ModelApiURL = "http://localhost:8000/resume-matching"; // change it to the actual AI model endpoint

        public ResumeTestService(IResumeTestRepository resumeTestRepository,
                                ISkillRepository skillRepository,
                                IResumeRepository resumeRepository,
                                IMapper mapper)
        {
            _resumeTestRepository = resumeTestRepository;
            _skillRepository = skillRepository;
            _resumeRepository = resumeRepository;
            _mapper = mapper;
        }

        public async Task<ResumeTestResponseDto?> GetResumeTestByIdAsync(int userId, int testId)
        {
            var resumeTest = await _resumeTestRepository.GetByIdAsync(testId, userId);
            if (resumeTest == null)
            {
                return null;
            }

            var responseDto = _mapper.Map<ResumeTestResponseDto>(resumeTest);
            
            // Add missing skills to the response
            responseDto.MissingSkills = resumeTest.Skills.Select(s => s.Skill1).ToList();
            
            return responseDto;
        }

        public async Task<IEnumerable<ResumeTestResponseDto>> GetAllResumeTestsByUserIdAsync(int userId)
        {
            var resumeTests = await _resumeTestRepository.GetAllByUserIdAsync(userId);
            var responseDtos = new List<ResumeTestResponseDto>();

            foreach (var resumeTest in resumeTests)
            {
                var responseDto = _mapper.Map<ResumeTestResponseDto>(resumeTest);
                responseDto.MissingSkills = resumeTest.Skills.Select(s => s.Skill1).ToList();
                responseDtos.Add(responseDto);
            }

            return responseDtos;
        }

        public async Task<PagedResult<ResumeTestResponseDto>> GetFilteredResumeTestsAsync(int userId, ResumeTestQueryDto queryDto)
        {
            var pagedResult = await _resumeTestRepository.GetFilteredResumeTestsAsync(userId, queryDto);
            var responseDtos = new List<ResumeTestResponseDto>();

            foreach (var resumeTest in pagedResult.Items)
            {
                var responseDto = _mapper.Map<ResumeTestResponseDto>(resumeTest);
                responseDto.MissingSkills = resumeTest.Skills.Select(s => s.Skill1).ToList();
                responseDtos.Add(responseDto);
            }

            return new PagedResult<ResumeTestResponseDto>
            {
                Items = responseDtos,
                TotalCount = pagedResult.TotalCount,
                PageSize = pagedResult.PageSize,
                PageNumber = pagedResult.PageNumber
            };
        }

        public async Task<ResumeTestResponseDto?> CreateResumeTestAsync(int userId, ResumeTestCreateDto createDto)
        {
            // Verify that the resume belongs to the user
            var resume = await _resumeRepository.GetByIdAsync(createDto.ResumeId);
            if (resume == null || resume.UserId != userId)
            {
                return null; // Resume not found or doesn't belong to the user
            }

            // Validate resume file size (10MB limit)
            if (resume.ResumeFile.Length > 10 * 1024 * 1024)
            {
                return null; // File too large
            }

            // Call the AI model to get resume matching results
            var aiResult = await GetResumeMatchingFromModelAsync(resume.ResumeFile, createDto.JobDescription);
            if (aiResult == null)
            {
                return null; // AI model failed
            }

            // Create the resume test
            var resumeTest = new ResumeTest
            {
                ResumeId = createDto.ResumeId,
                AtsScore = aiResult.Score,
                TestDate = DateTime.UtcNow,
                JobDescription = createDto.JobDescription
            };

            var createdResumeTest = await _resumeTestRepository.CreateAsync(resumeTest);

            // Create missing skills
            if (aiResult.MissingSkills != null && aiResult.MissingSkills.Count > 0)
            {
                var skills = aiResult.MissingSkills.Select(skill => new Skill
                {
                    TestId = createdResumeTest.TestId,
                    Skill1 = skill
                }).ToList();

                await _skillRepository.CreateAsync(skills);
            }

            // Get the complete resume test with skills
            var completeResumeTest = await _resumeTestRepository.GetByIdAsync(createdResumeTest.TestId, userId);
            if (completeResumeTest == null)
            {
                return null;
            }

            // Map to response DTO
            var responseDto = _mapper.Map<ResumeTestResponseDto>(completeResumeTest);
            responseDto.MissingSkills = completeResumeTest.Skills.Select(s => s.Skill1).ToList();
            responseDto.MatchingSkills = aiResult.MatchingSkills ?? new List<string>();

            return responseDto;
        }

        public async Task<bool> DeleteResumeTestAsync(int userId, int testId)
        {
            return await _resumeTestRepository.DeleteAsync(testId, userId);
        }

        private async Task<ResumeTestAIDto?> GetResumeMatchingFromModelAsync(byte[] resumeFile, string jobDescription)
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

                return result;
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("AI model request timed out.");
                return null;
            }
            catch (Exception ex)
            {
                // Log the exception here if you have logging configured
                Console.WriteLine($"Error calling AI model: {ex.Message}");
                return null;
            }
        }
    }
} 