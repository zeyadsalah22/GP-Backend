using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IInterviewQuestionFeedbackRepository
    {
        Task<InterviewQuestionFeedback?> GetByInterviewQuestionIdAsync(int interviewQuestionId);
        Task<List<InterviewQuestionFeedback>> GetByInterviewQuestionIdsAsync(IEnumerable<int> interviewQuestionIds);

        Task<InterviewQuestionFeedback> CreateAsync(InterviewQuestionFeedback feedback);
        Task<bool> UpdateAsync(InterviewQuestionFeedback feedback);
    }
}


