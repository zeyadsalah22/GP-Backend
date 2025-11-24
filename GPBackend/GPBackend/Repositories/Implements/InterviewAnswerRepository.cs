using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class InterviewAnswerRepository : IInterviewAnswerRepository
    {
        private readonly GPDBContext _context;

        public InterviewAnswerRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InterviewAnswer>> GetByQuestionIdAsync(int questionId)
        {
            return await _context.InterviewAnswers
                .Include(a => a.User)
                .Include(a => a.HelpfulVotes)
                .Where(a => a.QuestionId == questionId && !a.IsDeleted)
                .OrderByDescending(a => a.HelpfulCount)
                .ThenByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<InterviewAnswer?> GetByIdAsync(int id)
        {
            return await _context.InterviewAnswers
                .Include(a => a.User)
                .Include(a => a.Question)
                .FirstOrDefaultAsync(a => a.AnswerId == id && !a.IsDeleted);
        }

        public async Task<InterviewAnswer> CreateAsync(InterviewAnswer answer)
        {
            answer.CreatedAt = DateTime.UtcNow;
            answer.UpdatedAt = DateTime.UtcNow;
            answer.IsDeleted = false;
            answer.HelpfulCount = 0;

            _context.InterviewAnswers.Add(answer);
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task<bool> UpdateAsync(InterviewAnswer answer)
        {
            answer.UpdatedAt = DateTime.UtcNow;
            _context.InterviewAnswers.Update(answer);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var answer = await _context.InterviewAnswers.FindAsync(id);
            if (answer == null) return false;

            answer.IsDeleted = true;
            answer.UpdatedAt = DateTime.UtcNow;
            return await UpdateAsync(answer);
        }

        public async Task<bool> IncrementHelpfulCountAsync(int answerId)
        {
            var answer = await _context.InterviewAnswers.FindAsync(answerId);
            if (answer == null || answer.IsDeleted) return false;

            answer.HelpfulCount++;
            answer.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DecrementHelpfulCountAsync(int answerId)
        {
            var answer = await _context.InterviewAnswers.FindAsync(answerId);
            if (answer == null || answer.IsDeleted || answer.HelpfulCount <= 0) return false;

            answer.HelpfulCount--;
            answer.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

