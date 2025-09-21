using GPBackend.DTOs.Common;
using GPBackend.DTOs.ResumeTest;

namespace GPBackend.Services.Interfaces
{
    public interface IResumeTestService
    {


        /*

        Task<List<ResumeTestResponseDto>> CreateResumeTestAsync(List<ResumeTestCreateDto> ResumeTestCreateDto);
        Task<bool> DeleteResumeTestAsync(int testId);
        Task<ResumeTestResponseDto> GetResumeTestByIdAsync(int testId);
        Task<PagedResult<ResumeTestResponseDto>> GetFilteredResumeTestsAsync(int userId, ResumeTestQueryDto queryDto);
        Task<List<ResumeTestResponseDto>> GetResumeTestsFromModelAsync(resume.ResumeFile, createDto.JobDescription);

        ///////

        Task<List<InterviewQuestionResponseDto>> GetAllByInterviewIdAsync(int interviewId);

        ///////

        Task<IEnumerable<ResumeTest>> GetAllResumeTestsAsync(int userId); 

        Task<PagedResult<ResumeTest>> GetFilteredResumeTestAsync(int userId, ResumeTestQueryDto resumeTestQueryDto); 

        Task<ResumeTest?> GetResumeTestByIdAsync(int testId, int userId); // more restricted users only

        // Task<ResumeTest?> GetResumeTestByIdAsync(int testId); // more general access (admin users)

        Task<ResumeTest> CreateResumeTestAsync(ResumeTest resumeTest);
        
        Task<bool> DeleteResumeTestAsync(int testId, int userId);


        -----------------------------------------------

    }

    */

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

        /// <summary>
        /// Bulk delete resume tests for a specific user
        /// </summary>
        Task<int> BulkDeleteResumeTestsAsync(int userId, IEnumerable<int> ids);

        /// <summary>
        /// Get score distribution buckets for resume tests
        /// </summary>
        Task<GPBackend.DTOs.ResumeTest.ResumeTestScoresDistributionDto> GetScoresDistributionAsync(int userId);

        /// <summary>
        /// Get statistics: total tests, average score, best score, tests this month
        /// </summary>
        Task<GPBackend.DTOs.ResumeTest.ResumeTestStatsDto> GetStatsAsync(int userId);
    }
} 