using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class InterviewQuestionFeedbackRepository : IInterviewQuestionFeedbackRepository
    {
        private readonly GPDBContext _context;

        public InterviewQuestionFeedbackRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<InterviewQuestionFeedback?> GetByInterviewQuestionIdAsync(int interviewQuestionId)
        {
            return await _context.InterviewQuestionFeedbacks
                .FirstOrDefaultAsync(f => f.InterviewQuestionId == interviewQuestionId && !f.IsDeleted);
        }

        public async Task<List<InterviewQuestionFeedback>> GetByInterviewQuestionIdsAsync(IEnumerable<int> interviewQuestionIds)
        {
            var ids = interviewQuestionIds?.Distinct().ToList() ?? new List<int>();
            if (ids.Count == 0) return new List<InterviewQuestionFeedback>();

            return await _context.InterviewQuestionFeedbacks
                .Where(f => ids.Contains(f.InterviewQuestionId) && !f.IsDeleted)
                .ToListAsync();
        }

        public async Task<InterviewQuestionFeedback> CreateAsync(InterviewQuestionFeedback feedback)
        {
            _context.InterviewQuestionFeedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }

        public async Task<bool> UpdateAsync(InterviewQuestionFeedback feedback)
        {
            feedback.UpdatedAt = DateTime.UtcNow;
            _context.InterviewQuestionFeedbacks.Update(feedback);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}


