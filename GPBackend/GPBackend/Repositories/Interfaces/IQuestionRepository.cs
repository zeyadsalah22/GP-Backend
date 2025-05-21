using GPBackend.DTOs.Question;
using GPBackend.DTOs.Common;
using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IQuestionRepository
    {

        Task<IEnumerable<Question>> GetAllQuestionAsync(int userID);

        Task<PagedResult<Question>> GetFilteredQuestionAsync(int userId, QuestionQueryDto questionQueryDto);

        Task<Question?> GetQuestionByIdAsync(int questionId);

        Task<Question> CreateNewQuestionAsync(Question question);

        Task<bool> UpdateQuestionAsync(Question question);

        Task<bool> DeleteQuestionByIdAsync(int questionId);

        Task<bool> ExistsAsync(int questionId);

    }
}