using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IQuestionAskedByRepository
    {
        Task<QuestionAskedBy?> GetByQuestionAndUserAsync(int questionId, int userId);
        Task<QuestionAskedBy> CreateAsync(QuestionAskedBy questionAskedBy);
        Task<bool> DeleteAsync(int questionId, int userId);
        Task<bool> ExistsAsync(int questionId, int userId);
    }
}

