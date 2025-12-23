using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IInterviewAnswerRepository
    {
        Task<IEnumerable<InterviewAnswer>> GetByQuestionIdAsync(int questionId);
        Task<InterviewAnswer?> GetByIdAsync(int id);
        Task<InterviewAnswer> CreateAsync(InterviewAnswer answer);
        Task<bool> UpdateAsync(InterviewAnswer answer);
        Task<bool> DeleteAsync(int id);
        Task<bool> IncrementHelpfulCountAsync(int answerId);
        Task<bool> DecrementHelpfulCountAsync(int answerId);
    }
}

