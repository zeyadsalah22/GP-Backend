using GPBackend.DTOs.Question;
using GPBackend.DTOs.Common;

namespace GPBackend.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionResponseDto>> GetAllQuestion(int userId);

        Task<PagedResult<QuestionResponseDto>> GetFilteredQuestionBasedOnQuery(int userId, QuestionQueryDto QuestionQueryDto);

        Task<QuestionResponseDto?> GetQuestionById(int userId, int questionId);

        Task<QuestionResponseDto?> CreateNewQuestion(int userId, QuestionCreateDto QuestionCreateDto);

        Task<bool> UpdateQuestionById(int questionId, int userId, QuestionUpdateDto QuestionUpdateDto);

        Task<bool> DeleteQuestionById(int questionId, int userId);
        Task<int> BulkDeleteQuestionsAsync(IEnumerable<int> ids, int userId);
        Task<QuestionBatchResponseDto> CreateQuestionsBatchAsync(int userId, QuestionBatchCreateDto batchCreateDto);
    }
}