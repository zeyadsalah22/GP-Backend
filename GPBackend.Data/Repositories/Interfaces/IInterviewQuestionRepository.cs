using GPBackend.DTOs.InterviewQuestion;
using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IInterviewQuestionRepository
    {
        Task<InterviewQuestion?> GetByIdAsync(int id);
        Task<IEnumerable<InterviewQuestion?>> GetAllAsync(int userId);
        Task<IEnumerable<InterviewQuestion?>> GetByInterviewIdAsync(int userId, int interviewId);
        Task<List<InterviewQuestion>> CreateAsync(List<InterviewQuestion> interviewQuestions);
        Task<List<InterviewQuestion>> UpdateAsync(List<InterviewQuestion> interviewQuestions);
        Task<bool> DeleteAsync(int id);
    }
}