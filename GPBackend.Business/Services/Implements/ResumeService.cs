using AutoMapper;
using GPBackend.DTOs.Resume;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class ResumeService: IResumeService
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IMapper _mapper;
        public ResumeService(IResumeRepository resumeRepository, IMapper mapper)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
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
            if (resume == null)
            {
                throw new GPBackend.Exceptions.NotFoundException($"Resume with Id {id} not found");
            }
            if (resume.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resume");
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
            if (existingResume == null)
            {
                throw new GPBackend.Exceptions.NotFoundException($"Resume with Id {id} not found");
            }
            if (existingResume.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to update this resume");
            }

            _mapper.Map(resumeDto, existingResume);
            existingResume.UpdatedAt = DateTime.UtcNow; // Update the timestamp

            return await _resumeRepository.UpdateAsync(existingResume);
        }

        public async Task<bool> DeleteResumeAsync(int id, int userId)
        {
            var existingResume = await _resumeRepository.GetByIdAsync(id);
            
            // Validate that the resume belongs to the user
            if (existingResume == null)
            {
                throw new GPBackend.Exceptions.NotFoundException($"Resume with Id {id} not found");
            }
            if (existingResume.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this resume");
            }
            
            return await _resumeRepository.DeleteAsync(id);
        }
    }
}
