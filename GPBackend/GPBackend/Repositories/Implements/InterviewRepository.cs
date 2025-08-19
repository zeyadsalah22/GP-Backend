using GPBackend.DTOs.Interview;
using GPBackend.DTOs.Common;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GPBackend.DTOs.Question;
using System.Linq.Expressions;

namespace GPBackend.Repositories.Implements
{
    public class InterviewRepository : IInterviewRepository
    {
        private readonly GPDBContext _context;

        public InterviewRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Interview>> GetAllInterviewsAsync(int userId)
        {
            return await _context.Interviews
                        .Include(a => a.Application)
                        .Include(a => a.InterviewQuestions)
                        .Where(a => a.UserId == userId && !a.IsDeleted)
                        .OrderByDescending(a => a.CreatedAt)
                        .ToListAsync();
        }

        public async Task<PagedResult<Interview>> GetFilteredInterviewsAsync(int userId, InterviewQueryDto interviewQueryDto)
        {
            IQueryable<Interview> query = _context.Interviews
                .Include(a => a.Application)
                .Include(a => a.User)
                .Include(a => a.Company)
                .Include(a => a.InterviewQuestions)
                .Where(a => a.UserId == userId && !a.IsDeleted);

            // Apply filters
            if (interviewQueryDto.ApplicationId.HasValue)
            {
                query = query.Where(a => a.ApplicationId == interviewQueryDto.ApplicationId.Value);
            }
            if (interviewQueryDto.CompanyId.HasValue)
            {
                query = query.Where(a => a.CompanyId == interviewQueryDto.CompanyId.Value);
            }
            if (!string.IsNullOrEmpty(interviewQueryDto.Position))
            {
                query = query.Where(a => a.Position.Contains(interviewQueryDto.Position));
            }
            if (!string.IsNullOrEmpty(interviewQueryDto.JobDescription))
            {
                query = query.Where(a => a.JobDescription.Contains(interviewQueryDto.JobDescription));
            }
            if (interviewQueryDto.StartDate.HasValue)
            {
                query = query.Where(a => a.StartDate >= interviewQueryDto.StartDate.Value);
            }


            // Apply sorting
            if (!string.IsNullOrWhiteSpace(interviewQueryDto.SortBy))
            {
                query = ApplySorting(query, interviewQueryDto.SortBy, interviewQueryDto.SortDescending);
            }
            else
            {
                // Default sorting by start date descending
                query = query.OrderByDescending(a => a.StartDate);
            }


            // Pagination
            int totalCount = await query.CountAsync();
            var interviews = await query
                .Skip((interviewQueryDto.PageNumber - 1) * interviewQueryDto.PageSize)
                .Take(interviewQueryDto.PageSize)
                .ToListAsync();

            return new PagedResult<Interview>
            {
                Items = interviews,
                TotalCount = totalCount,
                PageNumber = interviewQueryDto.PageNumber,
                PageSize = interviewQueryDto.PageSize
            };
        }

        public async Task<Interview?> GetInterviewByIdAsync(int userId, int interviewId)
        {
            return await _context.Interviews
                    .Include(a => a.Application)
                    .Include(a => a.Company)
                    .Include(a => a.InterviewQuestions)
                    .FirstOrDefaultAsync(a => a.InterviewId == interviewId && a.UserId == userId && !a.IsDeleted);
        }

        public async Task<int> CreateInterviewAsync(Interview interview)
        {
            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync();
            return interview.InterviewId;
        }

        public async Task<bool> UpdateInterviewAsync(Interview interview)
        {
            interview.UpdatedAt = DateTime.UtcNow;

            // Attach the interview entity to the context
            _context.Interviews.Attach(interview);

            // Explicitly mark the properties you want to update
            _context.Entry(interview).Property(i => i.StartDate).IsModified = true;
            _context.Entry(interview).Property(i => i.Duration).IsModified = true;
            _context.Entry(interview).Property(i => i.Position).IsModified = true;
            _context.Entry(interview).Property(i => i.JobDescription).IsModified = true;
            _context.Entry(interview).Property(i => i.Feedback).IsModified = true;
            _context.Entry(interview).Property(i => i.UpdatedAt).IsModified = true;
            _context.Entry(interview).Property(i => i.ApplicationId).IsModified = true;
            _context.Entry(interview).Property(i => i.CompanyId).IsModified = true;
            // _context.Entry(interview).Collection(i => i.InterviewQuestions).IsModified = false; // Do not update the questions here
            
            // update only answers in interview questions
            if (interview.InterviewQuestions != null && interview.InterviewQuestions.Any())
            {
                foreach (var question in interview.InterviewQuestions)
                {
                    _context.InterviewQuestions.Attach(question);
                    _context.Entry(question).Property(q => q.Answer).IsModified = true;
                    _context.Entry(question).Property(q => q.UpdatedAt).IsModified = true;

                    // set update time for each question
                    question.UpdatedAt = DateTime.UtcNow;
                }
                // interview.InterviewQuestions.ForEach(q => q.UpdatedAt = DateTime.UtcNow);
            }
            // _context.Interviews.Update(interview);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteInterviewByIdAsync(int id)
        {
            var interview = await _context.Interviews.FindAsync(id);
            if (interview == null || interview.IsDeleted)
            {
                return false; // Interview not found or already deleted
            }
            // Mark each question as deleted
            foreach (var question in interview.InterviewQuestions)
            {
                question.IsDeleted = true;
            }

            interview.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> BulkSoftDeleteAsync(int userId, IEnumerable<int> ids)
        {
            if (ids == null) return 0;
            var idList = ids.Distinct().ToList();
            if (idList.Count == 0) return 0;

            var interviews = await _context.Interviews
                .Where(i => idList.Contains(i.InterviewId) && !i.IsDeleted && i.UserId == userId)
                .Include(i => i.InterviewQuestions)
                .ToListAsync();

            foreach (var i in interviews)
            {
                i.IsDeleted = true;
                foreach (var q in i.InterviewQuestions)
                {
                    q.IsDeleted = true;
                }
            }

            await _context.SaveChangesAsync();
            return interviews.Count;
        }

        private IQueryable<Interview> ApplySorting(IQueryable<Interview> query, string sortBy, bool sortDescending)
        {
            Expression<Func<Interview, object>> keySelector = sortBy.ToLower() switch
            {
                "startdate" => a => a.StartDate,
                "createdat" => a => a.CreatedAt,
                "updatedat" => a => a.UpdatedAt,
                "position" => a => a.Position,
                "duration" => a => a.Duration,
                "companyname" => a => a.Company.Name,
                "applicationid" => a => a.ApplicationId,
                _ => a => a.StartDate // Default sorting by start date
            };
            return sortDescending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
        }
    } 
}