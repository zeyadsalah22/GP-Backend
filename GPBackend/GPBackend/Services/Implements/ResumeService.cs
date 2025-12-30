using AutoMapper;
using GPBackend.DTOs.Resume;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using Microsoft.Extensions.Logging;
using GPBackend.BackgoundServices;

namespace GPBackend.Services.Implements
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IMLServiceClient _mlServiceClient;
        private readonly IMapper _mapper;
        private readonly ILogger<ResumeService> _logger;
        private readonly INodeRAGClient _nodeRAGClient;
        private readonly NodeRAGBackgroundService _backgroundService;

        public ResumeService(IResumeRepository resumeRepository,
                            IMLServiceClient mlServiceClient,
                            IMapper mapper,
                            ILogger<ResumeService> logger,
                            INodeRAGClient nodeRAGClient,
                            NodeRAGBackgroundService backgroundService)
        {
            _resumeRepository = resumeRepository;
            _mlServiceClient = mlServiceClient;
            _mapper = mapper;
            _logger = logger;
            _nodeRAGClient = nodeRAGClient;
            _backgroundService = backgroundService;
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
            
            // Upload to NodeRAG asynchronously (don't block on failure)
            _ = Task.Run(async () =>
            {
                try
                {
                    var filename = $"resume_{createdResume.UserId}_{createdResume.ResumeId}.pdf";
                    await _nodeRAGClient.UploadDocumentAsync(
                        createdResume.UserId, 
                        createdResume.ResumeFile, 
                        filename, 
                        "resume");
                    
                    // Queue graph build job
                    var buildJob = new NodeRAGBackgroundJob
                    {
                        JobType = NodeRAGJobType.BuildGraph,
                        UserId = createdResume.UserId
                    };
                    await _backgroundService.QueueJobAsync(buildJob);
                    
                    _logger.LogInformation("Resume {ResumeId} uploaded to NodeRAG and build queued for UserId={UserId}", 
                        createdResume.ResumeId, createdResume.UserId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to upload resume {ResumeId} to NodeRAG for UserId={UserId}", 
                        createdResume.ResumeId, createdResume.UserId);
                }
            });
            
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

            var result = await _resumeRepository.UpdateAsync(existingResume);
            
            // Upload updated resume to NodeRAG asynchronously (don't block on failure)
            if (result)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        var filename = $"resume_{existingResume.UserId}_{existingResume.ResumeId}.pdf";
                        await _nodeRAGClient.UploadDocumentAsync(
                            existingResume.UserId, 
                            existingResume.ResumeFile, 
                            filename, 
                            "resume");
                        
                        // Queue graph build job
                        var buildJob = new NodeRAGBackgroundJob
                        {
                            JobType = NodeRAGJobType.BuildGraph,
                            UserId = existingResume.UserId
                        };
                        await _backgroundService.QueueJobAsync(buildJob);
                        
                        _logger.LogInformation("Updated resume {ResumeId} uploaded to NodeRAG and build queued for UserId={UserId}", 
                            existingResume.ResumeId, existingResume.UserId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to upload updated resume {ResumeId} to NodeRAG for UserId={UserId}", 
                            existingResume.ResumeId, existingResume.UserId);
                    }
                });
            }
            
            return result;
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
