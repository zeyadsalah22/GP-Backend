using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IInterviewVideoFeedbackRepository
    {
        Task<InterviewVideoFeedback?> GetByInterviewIdAsync(int interviewId);
        Task<InterviewVideoFeedback> CreateAsync(InterviewVideoFeedback feedback);
        Task<bool> UpdateAsync(InterviewVideoFeedback feedback);
    }
}


