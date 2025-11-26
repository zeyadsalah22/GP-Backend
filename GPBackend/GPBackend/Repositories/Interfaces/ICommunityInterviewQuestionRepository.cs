using GPBackend.Models;
using GPBackend.DTOs.CommunityInterviewQuestion;
using GPBackend.DTOs.Common;

namespace GPBackend.Repositories.Interfaces
{
    public interface ICommunityInterviewQuestionRepository
    {
        Task<PagedResult<CommunityInterviewQuestion>> GetFilteredAsync(CommunityInterviewQuestionQueryDto queryDto);
        Task<CommunityInterviewQuestion?> GetByIdAsync(int id);
        Task<CommunityInterviewQuestion?> GetByIdWithDetailsAsync(int id);
        Task<CommunityInterviewQuestion> CreateAsync(CommunityInterviewQuestion question);
        Task<bool> UpdateAsync(CommunityInterviewQuestion question);
        Task<bool> DeleteAsync(int id);
        Task<bool> IncrementAskedCountAsync(int questionId);
        Task<bool> DecrementAskedCountAsync(int questionId);
        Task<bool> IncrementAnswerCountAsync(int questionId);
        Task<bool> DecrementAnswerCountAsync(int questionId);
    }
}

