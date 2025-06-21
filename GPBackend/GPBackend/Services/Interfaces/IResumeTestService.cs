using GPBackend.DTOs.Common;
using GPBackend.DTOs.ResumeTest;

namespace GPBackend.Services.Interfaces
{
    public interface IResumeTestService
    {
        Task<ResumeTestResponseDto?> GetResumeTestByIdAsync(int userId, int testId);
        Task<IEnumerable<ResumeTestResponseDto>> GetAllResumeTestsByUserIdAsync(int userId);
        Task<PagedResult<ResumeTestResponseDto>> GetFilteredResumeTestsAsync(int userId, ResumeTestQueryDto queryDto);
        Task<ResumeTestResponseDto?> CreateResumeTestAsync(int userId, ResumeTestCreateDto createDto);
        Task<bool> DeleteResumeTestAsync(int userId, int testId);
    }
} 