using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class InterviewVideoFeedbackRepository : IInterviewVideoFeedbackRepository
    {
        private readonly GPDBContext _context;

        public InterviewVideoFeedbackRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<InterviewVideoFeedback?> GetByInterviewIdAsync(int interviewId)
        {
            return await _context.InterviewVideoFeedbacks
                .FirstOrDefaultAsync(v => v.InterviewId == interviewId && !v.IsDeleted);
        }

        public async Task<InterviewVideoFeedback> CreateAsync(InterviewVideoFeedback feedback)
        {
            _context.InterviewVideoFeedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }

        public async Task<bool> UpdateAsync(InterviewVideoFeedback feedback)
        {
            feedback.UpdatedAt = DateTime.UtcNow;
            _context.InterviewVideoFeedbacks.Update(feedback);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}


