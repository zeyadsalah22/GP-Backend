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
        private readonly IResumeRepository _resumeRepository;
        private readonly IResumeTestMissingSkillsService _resumeTestMissingSkillsService;
        private readonly IMapper _mapper;

        public ResumeTestService(IResumeTestRepository resumeTestRepository,
                                IResumeRepository resumeRepository,
                                IResumeTestMissingSkillsService resumeTestMissingSkillsService,
                                IMapper mapper)
        {
            _resumeTestRepository = resumeTestRepository;
            _resumeRepository = resumeRepository;
            _resumeTestMissingSkillsService = resumeTestMissingSkillsService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResumeTestResponseDto>> GetAllResumeTestsAsync(int userId)
        {
            var resumeTests = await _resumeTestRepository.GetAllResumeTestsAsync(userId);
            var responseDtos = _mapper.Map<IEnumerable<ResumeTestResponseDto>>(resumeTests);
            /*
            new List<ResumeTestResponseDto>();
            foreach (var resumeTest in resumeTests)
            {
                var responseDto = _mapper.Map<ResumeTestResponseDto>(resumeTest);
                responseDto.MissingSkills = resumeTest.Skills.Select(s => s.Skill1).ToList();
                responseDtos.Add(responseDto);
            }
            */
            return responseDtos;
        }

        public async Task<ResumeTestResponseDto?> GetResumeTestByIdAsync(int userId, int testId)
        {
            var resumeTest = await _resumeTestRepository.GetResumeTestByIdAsync(testId, userId);
            if (resumeTest == null)
            {
                return null;
            }

            var responseDto = _mapper.Map<ResumeTestResponseDto>(resumeTest);
            responseDto.MissingSkills = resumeTest.Skills.Select(s => s.Skill1).ToList();
            
            return responseDto;
        }

        public async Task<PagedResult<ResumeTestResponseDto>> GetFilteredResumeTestsAsync(int userId, ResumeTestQueryDto resumeTestQueryDto)
        {
            var pagedResult = await _resumeTestRepository.GetFilteredResumeTestAsync(userId, resumeTestQueryDto);
            
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

        public async Task<ResumeTestResponseDto?> CreateResumeTestAsync(int userId, ResumeTestCreateDto resumeTestCreateDto)
        {
            // Verify that the resume belongs to the user
            var resume = await _resumeRepository.GetByIdAsync(resumeTestCreateDto.ResumeId);
            if (resume == null || resume.UserId != userId)
            {
                return null; // Resume not found or doesn't belong to the user
            }

            // Validate resume file size (10MB limit)
            if (resume.ResumeFile.Length > 10 * 1024 * 1024)
            {
                return null; // File too large
            }

            // Create the resume test
            var resumeTest = new ResumeTest
            {
                ResumeId = resumeTestCreateDto.ResumeId,
                AtsScore = 0, // Will be updated by AI service
                TestDate = DateTime.UtcNow,
                JobDescription = resumeTestCreateDto.JobDescription
            };

            var createdResumeTest = await _resumeTestRepository.CreateResumeTestAsync(resumeTest);

            // Call AI analysis to get ATS score and missing skills
            var aiAnalysis = await _resumeTestMissingSkillsService.AnalyzeResumeAsync(
                resume.ResumeFile, 
                resumeTestCreateDto.JobDescription
            );

            if (aiAnalysis != null)
            {
                // Update the ATS score, no need to call database again
                createdResumeTest.AtsScore = (int)aiAnalysis.ResumeScore;

                // Store missing skills
                if (aiAnalysis.MissingSkills != null && aiAnalysis.MissingSkills.Count > 0)
                {
                    await _resumeTestMissingSkillsService.StoreMissingSkillsAsync(createdResumeTest.TestId, aiAnalysis.MissingSkills);
                }
            }

            // Get the complete resume test with skills
            // var completeResumeTest = await _resumeTestRepository.GetResumeTestByIdAsync(createdResumeTest.TestId, userId);
            // if (completeResumeTest == null)
            // {
            //     return null;
            // }

            // Map to response DTO
            var responseDto = _mapper.Map<ResumeTestResponseDto>(createdResumeTest);
            responseDto.MissingSkills = createdResumeTest.Skills.Select(s => s.Skill1).ToList();

            return responseDto;
        }

        public async Task<bool> DeleteResumeTestAsync(int userId, int testId)
        {    
            return await _resumeTestRepository.DeleteResumeTestAsync(testId, userId);
        }
    }
} 