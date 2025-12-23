using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class QuestionAskedByRepository : IQuestionAskedByRepository
    {
        private readonly GPDBContext _context;

        public QuestionAskedByRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<QuestionAskedBy?> GetByQuestionAndUserAsync(int questionId, int userId)
        {
            return await _context.QuestionAskedBys
                .FirstOrDefaultAsync(q => q.QuestionId == questionId && q.UserId == userId);
        }

        public async Task<QuestionAskedBy> CreateAsync(QuestionAskedBy questionAskedBy)
        {
            questionAskedBy.CreatedAt = DateTime.UtcNow;
            _context.QuestionAskedBys.Add(questionAskedBy);
            await _context.SaveChangesAsync();
            return questionAskedBy;
        }

        public async Task<bool> DeleteAsync(int questionId, int userId)
        {
            var questionAskedBy = await GetByQuestionAndUserAsync(questionId, userId);
            if (questionAskedBy == null) return false;

            _context.QuestionAskedBys.Remove(questionAskedBy);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int questionId, int userId)
        {
            return await _context.QuestionAskedBys
                .AnyAsync(q => q.QuestionId == questionId && q.UserId == userId);
        }
    }
}

