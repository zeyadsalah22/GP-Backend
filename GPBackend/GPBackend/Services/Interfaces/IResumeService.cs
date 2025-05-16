using GPBackend.DTOs.Resume;

namespace GPBackend.Services.Interfaces
{
    public interface IResumeService
    {
        Task<IEnumerable<ResumeResponseDto>> GetAllResumesAsync(int userId);
        Task<ResumeResponseDto?> GetResumeByIdAsync(int id);
        Task<ResumeResponseDto> CreateResumeAsync(ResumeCreateDto resumeDto);
        Task<bool> UpdateResumeAsync(int id, ResumeUpdateDto resumeDto);
        Task<bool> DeleteResumeAsync(int id);
    }
}
