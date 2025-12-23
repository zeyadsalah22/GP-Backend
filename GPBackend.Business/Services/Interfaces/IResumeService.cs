using GPBackend.DTOs.Resume;

namespace GPBackend.Services.Interfaces
{
    public interface IResumeService
    {
        Task<IEnumerable<ResumeResponseDto>> GetAllResumesAsync(int userId);
        Task<ResumeResponseDto?> GetResumeByIdAsync(int id, int userId);
        Task<ResumeResponseDto> CreateResumeAsync(ResumeCreateDto resumeDto);
        Task<bool> UpdateResumeAsync(int id, ResumeUpdateDto resumeDto, int userId);
        Task<bool> DeleteResumeAsync(int id, int userId);
    }
}
