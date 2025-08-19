using GPBackend.DTOs.Common;
using GPBackend.DTOs.ResumeTest;
using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IResumeTestRepository
    {
        Task<IEnumerable<ResumeTest>> GetAllResumeTestsAsync(int userId); 

        Task<PagedResult<ResumeTest>> GetFilteredResumeTestAsync(int userId, ResumeTestQueryDto resumeTestQueryDto); 

        Task<ResumeTest?> GetResumeTestByIdAsync(int testId, int userId); // more restricted users only

        // Task<ResumeTest?> GetResumeTestByIdAsync(int testId); // more general access (admin users)

        Task<ResumeTest> CreateResumeTestAsync(ResumeTest resumeTest);
        
        Task<bool> DeleteResumeTestAsync(int testId, int userId);

        Task<bool> UpdateResumeTestAsync(ResumeTest resumeTest);
        Task<int> BulkDeleteAsync(int userId, IEnumerable<int> ids);
    }
} 