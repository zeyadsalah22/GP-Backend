using AutoMapper;
using GPBackend.DTOs.Resume;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GPBackend.Services.Implements
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IMLServiceClient _mlServiceClient;
        private readonly IMapper _mapper;
        private readonly ILogger<ResumeService> _logger;

        public ResumeService(IResumeRepository resumeRepository,
                            IMLServiceClient mlServiceClient,
                            IMapper mapper,
                            ILogger<ResumeService> logger)
        {
            _resumeRepository = resumeRepository;
            _mlServiceClient = mlServiceClient;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<ResumeResponseDto>> GetAllResumesAsync(int userId)
        {
            var resumes = await _resumeRepository.GetAllAsync(userId);
            return _mapper.Map<IEnumerable<ResumeResponseDto>>(resumes);
        }

        public async Task<ResumeResponseDto?> GetResumeByIdAsync(int id, int userId)
        {
            var resume = await _resumeRepository.GetByIdAsync(id);

            // Validate that the resume belongs to the user
            if (resume == null || resume.UserId != userId)
            {
                return null;
            }

            return _mapper.Map<ResumeResponseDto>(resume);
        }

        public async Task<ResumeResponseDto> CreateResumeAsync(ResumeCreateDto resumeDto)
        {
            var resume = _mapper.Map<Resume>(resumeDto);
            var createdResume = await _resumeRepository.CreateAsync(resume);
            return _mapper.Map<ResumeResponseDto>(createdResume);
        }

        public async Task<bool> UpdateResumeAsync(int id, ResumeUpdateDto resumeDto, int userId)
        {
            var existingResume = await _resumeRepository.GetByIdAsync(id);

            // Validate that the resume belongs to the user
            if (existingResume == null || existingResume.UserId != userId)
            {
                return false;
            }

            _mapper.Map(resumeDto, existingResume);
            existingResume.UpdatedAt = DateTime.UtcNow; // Update the timestamp

            return await _resumeRepository.UpdateAsync(existingResume);
        }

        public async Task<bool> DeleteResumeAsync(int id, int userId)
        {
            var existingResume = await _resumeRepository.GetByIdAsync(id);

            // Validate that the resume belongs to the user
            if (existingResume == null || existingResume.UserId != userId)
            {
                return false;
            }

            return await _resumeRepository.DeleteAsync(id);
        }

        public async Task<ResumeMatchingResponse> MatchResumeAsync(int resumeId, string jobDescription, int userId)
        {
            try
            {
                // Validate that the resume exists and belongs to the user
                var resume = await _resumeRepository.GetByIdAsync(resumeId);
                if (resume == null || resume.UserId != userId)
                {
                    throw new UnauthorizedAccessException("Resume not found or does not belong to the user.");
                }

                if (string.IsNullOrWhiteSpace(jobDescription))
                {
                    throw new ArgumentException("Job description cannot be null or empty.");
                }

                // Convert resume file to base64
                string base64Resume = Convert.ToBase64String(resume.ResumeFile);

                _logger.LogInformation("Matching resume {ResumeId} against job description (length: {Length})",
                    resumeId, jobDescription.Length);

                // Call ML service to match resume
                var matchingResult = await _mlServiceClient.MatchResumeAsync(base64Resume, jobDescription);

                _logger.LogInformation("Resume matching completed. Score: {Score}, MatchedSkills: {MatchedCount}, MissingSkills: {MissingCount}",
                    matchingResult.ResumeScore, matchingResult.MatchedSkills.Count, matchingResult.MissingSkills.Count);

                return matchingResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error matching resume {ResumeId}", resumeId);
                throw;
            }
        }
    }
}
