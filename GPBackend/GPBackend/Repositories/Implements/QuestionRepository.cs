using Microsoft.EntityFrameworkCore;
using GPBackend.DTOs.Question;
using GPBackend.DTOs.Common;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using System.Linq.Expressions;

namespace GPBackend.Repositories.Implements
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly GPDBContext _context;

        public QuestionRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Question>> GetAllQuestionAsync(int userId)
        {
            return await _context.Questions
                        .Include(q => q.Application)
                        .Where(q => q.Application.UserId == userId && !q.IsDeleted)
                        .OrderBy(q => q.CreatedAt)
                        .ToListAsync();
        }

        public async Task<PagedResult<Question>> GetFilteredQuestionAsync(int userId, QuestionQueryDto questionQueryDto)
        {
            IQueryable<Question> query = _context.Questions
                        .Include(q => q.Application)
                        .Where(q => q.Application.UserId == userId && !q.IsDeleted);

            if (questionQueryDto.ApplicationId.HasValue)
            {
                query = query.Where(q => q.ApplicationId == questionQueryDto.ApplicationId);
            }

            if (!string.IsNullOrWhiteSpace(questionQueryDto.Question))
            {
                query = query.Where(q => q.Question1.Contains(questionQueryDto.Question));
            }

            if (!string.IsNullOrWhiteSpace(questionQueryDto.Answer))
            {
                query = query.Where(q => q.Answer != null && q.Answer.Contains(questionQueryDto.Answer));
            }

            if (!string.IsNullOrWhiteSpace(questionQueryDto.SearchTerm))
            {
                query = query.Where(q =>
                    q.Question1.Contains(questionQueryDto.SearchTerm) ||
                    q.Answer != null && q.Answer.Contains(questionQueryDto.SearchTerm)
                );
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(questionQueryDto.SortBy))
            {
                query = ApplySorting(query, questionQueryDto.SortBy, questionQueryDto.SortDescending);
            }
            else
            {
                // Default sorting by submission date descending
                query = query.OrderByDescending(a => a.UpdatedAt);
            }

            // Calculate total count
            var totalCount = await query.CountAsync();

            // Apply pagination
            var pageSize = questionQueryDto.PageSize;
            var pageNumber = questionQueryDto.PageNumber;
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Return result
            return new PagedResult<Question>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

        }

        public async Task<Question?> GetQuestionByIdAsync(int questionId)
        {
            return await _context.Questions
                        .Include(q => q.Application)
                        .FirstOrDefaultAsync(q => q.QuestionId == questionId && !q.IsDeleted);
        }

        public async Task<Question> CreateNewQuestionAsync(Question question)
        {
            question.CreatedAt = DateTime.UtcNow;
            question.UpdatedAt = DateTime.UtcNow;
            question.IsDeleted = false;

            _context.Questions.Add(question);

            int rows = await _context.SaveChangesAsync();
            return question;
        }

        public async Task<bool> UpdateQuestionAsync(Question question)
        {
            question.UpdatedAt = DateTime.UtcNow;

            _context.Questions.Update(question);

            int rows = await _context.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<bool> DeleteQuestionByIdAsync(int questionId)
        {
            Question? question = await _context.Questions.FindAsync(questionId);

            if (question == null)
            {
                return false;
            }
            question.IsDeleted = true;
            question.UpdatedAt = DateTime.UtcNow;

            _context.Questions.Update(question);

            int rows = await _context.SaveChangesAsync();

            // true if rows affected are at least one
            return rows > 0;
        }

        public async Task<bool> ExistsAsync(int questionId)
        {
            return await _context.Questions.AnyAsync(q => q.QuestionId == questionId && !q.IsDeleted);
        }
        
        private IQueryable<Question> ApplySorting(IQueryable<Question> query, string sortBy, bool descending)
        {
            Expression<Func<Question, object>> keySelector = sortBy.ToLower() switch
            {
                "question" => a => a.Question1,
                "answer" => a => a.Answer,
                "createdat" => a => a.CreatedAt,
                "updatedat" => a => a.UpdatedAt,
                _ => a => a.CreatedAt // Default sorting by submission date
            };

            return descending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
        }
    }
}