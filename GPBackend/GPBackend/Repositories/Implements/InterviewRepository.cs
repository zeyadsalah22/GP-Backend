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

        InterviewRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Interview>> GetAllInterviewsAsync(int userId)
        {
            return await _context.Interviews
                        .Include(a => a.Application)
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
            _context.Interviews.Update(interview);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteInterviewByIdAsync(int id)
        {
            var interview = await _context.Interviews.FindAsync(id);
            if (interview == null || interview.IsDeleted)
            {
                return false; // Interview not found or already deleted
            }

            interview.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }

        private IQueryable<Interview> ApplySorting(IQueryable<Interview> query, string sortBy, bool sortDescending)
        {
            Expression<Func<Interview, object>> keySelector = sortBy.ToLower() switch
            {
                "startdate" => a => a.StartDate,
                "createdat" => a => a.CreatedAt,
                "updatedat" => a => a.UpdatedAt,
                "jobtitle" => a => a.Position,
                "companyname" => a => a.Company.Name,
                "applicationid" => a => a.ApplicationId,
                _ => a => a.StartDate // Default sorting by start date
            };
            return sortDescending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
        }
    } 
}