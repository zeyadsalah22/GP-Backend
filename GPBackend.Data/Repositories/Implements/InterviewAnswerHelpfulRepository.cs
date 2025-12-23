using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class InterviewAnswerHelpfulRepository : IInterviewAnswerHelpfulRepository
    {
        private readonly GPDBContext _context;

        public InterviewAnswerHelpfulRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<InterviewAnswerHelpful?> GetByAnswerAndUserAsync(int answerId, int userId)
        {
            return await _context.InterviewAnswerHelpfuls
                .FirstOrDefaultAsync(h => h.AnswerId == answerId && h.UserId == userId);
        }

        public async Task<InterviewAnswerHelpful> CreateAsync(InterviewAnswerHelpful helpful)
        {
            helpful.CreatedAt = DateTime.UtcNow;
            _context.InterviewAnswerHelpfuls.Add(helpful);
            await _context.SaveChangesAsync();
            return helpful;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var helpful = await _context.InterviewAnswerHelpfuls.FindAsync(id);
            if (helpful == null) return false;

            _context.InterviewAnswerHelpfuls.Remove(helpful);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int answerId, int userId)
        {
            return await _context.InterviewAnswerHelpfuls
                .AnyAsync(h => h.AnswerId == answerId && h.UserId == userId);
        }
    }
}

