using GPBackend.DTOs.Common;
using GPBackend.DTOs.ResumeTest;
using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IResumeTestRepository
    {
        Task<ResumeTest?> GetByIdAsync(int testId, int userId); // choose a specific test
        Task<IEnumerable<ResumeTest>> GetAllByUserIdAsync(int userId); // get all tests for a user
        Task<PagedResult<ResumeTest>> GetFilteredResumeTestsAsync(int userId, ResumeTestQueryDto queryDto); // get tests with filters
        Task<ResumeTest> CreateAsync(ResumeTest resumeTest); // create a test
        Task<bool> DeleteAsync(int testId, int userId); // userId for validation
    }
} 