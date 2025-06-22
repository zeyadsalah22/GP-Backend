using GPBackend.DTOs.Common;
using GPBackend.DTOs.ResumeTest;

namespace GPBackend.Services.Interfaces
{
    public interface IResumeTestService
    {
        /// <summary>
        /// Get all resume tests for a user
        /// </summary>
        Task<IEnumerable<ResumeTestResponseDto>> GetAllResumeTestsAsync(int userId);

        /// <summary>
        /// Get a specific resume test by ID for a specific user
        /// </summary>
        Task<ResumeTestResponseDto?> GetResumeTestByIdAsync(int userId, int testId);

        /// <summary>
        /// Get filtered and paginated resume tests for a user
        /// </summary>
        Task<PagedResult<ResumeTestResponseDto>> GetFilteredResumeTestsAsync(int userId, ResumeTestQueryDto ResumeTestQueryDto);

        /// <summary>
        /// Create a new resume test for a user
        /// </summary>
        Task<ResumeTestResponseDto?> CreateResumeTestAsync(int userId, ResumeTestCreateDto ResumeTestCreateDto);

        /// <summary>
        /// Delete a resume test for a specific user
        /// </summary>
        Task<bool> DeleteResumeTestAsync(int userId, int testId);
    }
} 
