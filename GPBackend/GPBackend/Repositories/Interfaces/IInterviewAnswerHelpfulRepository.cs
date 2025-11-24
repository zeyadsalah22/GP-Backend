using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IInterviewAnswerHelpfulRepository
    {
        Task<InterviewAnswerHelpful?> GetByAnswerAndUserAsync(int answerId, int userId);
        Task<InterviewAnswerHelpful> CreateAsync(InterviewAnswerHelpful helpful);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int answerId, int userId);
    }
}

