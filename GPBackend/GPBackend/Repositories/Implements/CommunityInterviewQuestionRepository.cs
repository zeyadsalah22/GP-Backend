using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.DTOs.CommunityInterviewQuestion;
using GPBackend.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class CommunityInterviewQuestionRepository : ICommunityInterviewQuestionRepository
    {
        private readonly GPDBContext _context;

        public CommunityInterviewQuestionRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<CommunityInterviewQuestion>> GetFilteredAsync(CommunityInterviewQuestionQueryDto queryDto)
        {
            IQueryable<CommunityInterviewQuestion> query = _context.CommunityInterviewQuestions
                .Include(q => q.User)
                .Include(q => q.Company)
                .Where(q => !q.IsDeleted);

            // Apply search
            if (!string.IsNullOrWhiteSpace(queryDto.SearchText))
            {
                string searchTerm = queryDto.SearchText.ToLower();
                query = query.Where(q =>
                    q.QuestionText.ToLower().Contains(searchTerm) ||
                    (q.Company != null && q.Company.Name.ToLower().Contains(searchTerm)) ||
                    (q.CompanyName != null && q.CompanyName.ToLower().Contains(searchTerm)) ||
                    (q.AddedRoleType != null && q.AddedRoleType.ToLower().Contains(searchTerm)) ||
                    (q.AddedQuestionType != null && q.AddedQuestionType.ToLower().Contains(searchTerm))
                );
            }

            // Apply filters
            if (queryDto.CompanyIds != null && queryDto.CompanyIds.Any())
            {
                query = query.Where(q => q.CompanyId.HasValue && queryDto.CompanyIds.Contains(q.CompanyId.Value));
            }

            if (!string.IsNullOrWhiteSpace(queryDto.CompanyName))
            {
                string companyNameSearch = queryDto.CompanyName.ToLower();
                query = query.Where(q =>
                    (q.Company != null && q.Company.Name.ToLower().Contains(companyNameSearch)) ||
                    (q.CompanyName != null && q.CompanyName.ToLower().Contains(companyNameSearch))
                );
            }

            if (queryDto.RoleType.HasValue)
            {
                query = query.Where(q => q.RoleType == queryDto.RoleType.Value);
            }

            if (!string.IsNullOrWhiteSpace(queryDto.AddedRoleType))
            {
                string addedRoleTypeSearch = queryDto.AddedRoleType.ToLower();
                query = query.Where(q => q.AddedRoleType != null && q.AddedRoleType.ToLower().Contains(addedRoleTypeSearch));
            }

            if (queryDto.QuestionType.HasValue)
            {
                query = query.Where(q => q.QuestionType == queryDto.QuestionType.Value);
            }

            if (!string.IsNullOrWhiteSpace(queryDto.AddedQuestionType))
            {
                string addedQuestionTypeSearch = queryDto.AddedQuestionType.ToLower();
                query = query.Where(q => q.AddedQuestionType != null && q.AddedQuestionType.ToLower().Contains(addedQuestionTypeSearch));
            }

            if (queryDto.Difficulty.HasValue)
            {
                query = query.Where(q => q.Difficulty == queryDto.Difficulty.Value);
            }

            if (queryDto.MostFrequentlyAsked.HasValue && queryDto.MostFrequentlyAsked.Value)
            {
                query = query.Where(q => q.AskedCount > 0);
            }

            // Get total count before pagination
            int totalCount = await query.CountAsync();

            // Apply sorting
            query = ApplySorting(query, queryDto.SortBy ?? "MostRecent");

            // Apply pagination
            var items = await query
                .Skip((queryDto.Page - 1) * queryDto.PageSize)
                .Take(queryDto.PageSize)
                .ToListAsync();

            return new PagedResult<CommunityInterviewQuestion>
            {
                Items = items,
                PageNumber = queryDto.Page,
                PageSize = queryDto.PageSize,
                TotalCount = totalCount
            };
        }

        private IQueryable<CommunityInterviewQuestion> ApplySorting(IQueryable<CommunityInterviewQuestion> query, string sortBy)
        {
            return sortBy switch
            {
                "MostRecent" => query.OrderByDescending(q => q.CreatedAt),
                "MostAsked" => query.OrderByDescending(q => q.AskedCount).ThenByDescending(q => q.CreatedAt),
                "MostAnswered" => query.OrderByDescending(q => q.AnswerCount).ThenByDescending(q => q.CreatedAt),
                _ => query.OrderByDescending(q => q.CreatedAt)
            };
        }

        public async Task<CommunityInterviewQuestion?> GetByIdAsync(int id)
        {
            return await _context.CommunityInterviewQuestions
                .Include(q => q.User)
                .Include(q => q.Company)
                .FirstOrDefaultAsync(q => q.QuestionId == id && !q.IsDeleted);
        }

        public async Task<CommunityInterviewQuestion?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.CommunityInterviewQuestions
                .Include(q => q.User)
                .Include(q => q.Company)
                .Include(q => q.Answers.Where(a => !a.IsDeleted))
                    .ThenInclude(a => a.User)
                .Include(q => q.Answers.Where(a => !a.IsDeleted))
                    .ThenInclude(a => a.HelpfulVotes)
                .FirstOrDefaultAsync(q => q.QuestionId == id && !q.IsDeleted);
        }

        public async Task<CommunityInterviewQuestion> CreateAsync(CommunityInterviewQuestion question)
        {
            question.CreatedAt = DateTime.UtcNow;
            question.UpdatedAt = DateTime.UtcNow;
            question.IsDeleted = false;
            question.AskedCount = 0;
            question.AnswerCount = 0;

            _context.CommunityInterviewQuestions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<bool> UpdateAsync(CommunityInterviewQuestion question)
        {
            question.UpdatedAt = DateTime.UtcNow;
            _context.CommunityInterviewQuestions.Update(question);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var question = await _context.CommunityInterviewQuestions.FindAsync(id);
            if (question == null) return false;

            question.IsDeleted = true;
            question.UpdatedAt = DateTime.UtcNow;
            return await UpdateAsync(question);
        }

        public async Task<bool> IncrementAskedCountAsync(int questionId)
        {
            var question = await _context.CommunityInterviewQuestions.FindAsync(questionId);
            if (question == null || question.IsDeleted) return false;

            question.AskedCount++;
            question.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IncrementAnswerCountAsync(int questionId)
        {
            var question = await _context.CommunityInterviewQuestions.FindAsync(questionId);
            if (question == null || question.IsDeleted) return false;

            question.AnswerCount++;
            question.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DecrementAskedCountAsync(int questionId)
        {
            var question = await _context.CommunityInterviewQuestions.FindAsync(questionId);
            if (question == null || question.IsDeleted || question.AskedCount <= 0) return false;

            question.AskedCount--;
            question.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DecrementAnswerCountAsync(int questionId)
        {
            var question = await _context.CommunityInterviewQuestions.FindAsync(questionId);
            if (question == null || question.IsDeleted || question.AnswerCount <= 0) return false;

            question.AnswerCount--;
            question.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

