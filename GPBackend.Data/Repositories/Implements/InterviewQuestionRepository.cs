using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class InterviewQuestionRepository : IInterviewQuestionRepository
    {
        private readonly GPDBContext _context;

        public InterviewQuestionRepository(GPDBContext context)
        {
            _context = context;
        }

        // Implement the methods defined in the interface
        public async Task<InterviewQuestion?> GetByIdAsync(int id)
        {
            return await _context.InterviewQuestions
                .FirstOrDefaultAsync(iq => iq.QuestionId == id && !iq.IsDeleted);
        }

        public async Task<IEnumerable<InterviewQuestion?>> GetAllAsync(int userId)
        {
            // Implementation logic to retrieve all InterviewQuestions for a specific user
            return await _context.InterviewQuestions
                .Where(iq => iq.Interview.UserId == userId && !iq.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<InterviewQuestion?>> GetByInterviewIdAsync(int userId, int interviewId)
        {
            // Implementation logic to retrieve InterviewQuestions by Interview ID
            return await _context.InterviewQuestions
                .Where(iq => iq.InterviewId == interviewId && iq.Interview.UserId == userId && !iq.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<InterviewQuestion>> CreateAsync(List<InterviewQuestion> interviewQuestions)
        {
            // Implementation logic to create a new InterviewQuestion
            await _context.InterviewQuestions.AddRangeAsync(interviewQuestions);
            await _context.SaveChangesAsync();

            return interviewQuestions;

        }

        public async Task<List<InterviewQuestion>> UpdateAsync(List<InterviewQuestion> interviewQuestions)
        {
            foreach (InterviewQuestion iq in interviewQuestions)
            {
                // Attach only by key, without replacing existing tracked values
                _context.InterviewQuestions.Attach(iq);

                // Explicitly mark only the fields you want to update
                _context.Entry(iq).Property(q => q.Answer).IsModified = true;
                _context.Entry(iq).Property(q => q.UpdatedAt).IsModified = true;

                // Set the update time
                iq.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return interviewQuestions;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation logic to delete an InterviewQuestion by ID
            var existingQuestion = await GetByIdAsync(id);
            if (existingQuestion == null) return false;
            existingQuestion.IsDeleted = true;
            // _context.InterviewQuestions.Remove(existingQuestion);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}