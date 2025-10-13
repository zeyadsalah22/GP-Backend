using Microsoft.EntityFrameworkCore;
using GPBackend.DTOs.Common;
using GPBackend.DTOs.WeeklyGoal;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using System.Linq.Expressions;

namespace GPBackend.Repositories.Implements
{
    public class WeeklyGoalRepository : IWeeklyGoalRepository
    {
        private readonly GPDBContext _context;

        public WeeklyGoalRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<WeeklyGoal?> GetByIdAsync(int id)
        {
            return await _context.WeeklyGoals
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.WeeklyGoalId == id && !w.IsDeleted);
        }

        public async Task<WeeklyGoal?> GetByWeekStartDateAsync(int userId, DateOnly weekStartDate)
        {
            return await _context.WeeklyGoals
                .FirstOrDefaultAsync(w => w.UserId == userId 
                    && w.WeekStartDate == weekStartDate 
                    && !w.IsDeleted);
        }

        public async Task<WeeklyGoal?> GetCurrentWeekGoalAsync(int userId)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            
            return await _context.WeeklyGoals
                .Where(w => w.UserId == userId 
                    && !w.IsDeleted
                    && w.WeekStartDate <= today 
                    && w.WeekEndDate >= today)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<WeeklyGoal>> GetFilteredWeeklyGoalsAsync(int userId, WeeklyGoalQueryDto queryDto)
        {
            IQueryable<WeeklyGoal> query = _context.WeeklyGoals
                .Include(w => w.User)
                .Where(w => w.UserId == userId && !w.IsDeleted);

            // Apply filters
            if (queryDto.FromDate.HasValue)
            {
                query = query.Where(w => w.WeekStartDate >= queryDto.FromDate.Value);
            }

            if (queryDto.ToDate.HasValue)
            {
                query = query.Where(w => w.WeekEndDate <= queryDto.ToDate.Value);
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(queryDto.SortBy))
            {
                query = ApplySorting(query, queryDto.SortBy, queryDto.SortDescending);
            }
            else
            {
                // Default sorting by week start date descending
                query = query.OrderByDescending(w => w.WeekStartDate);
            }

            // Calculate total count
            var totalCount = await query.CountAsync();

            // Apply pagination
            var pageSize = queryDto.PageSize;
            var pageNumber = queryDto.PageNumber;
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<WeeklyGoal>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber
            };
        }

        public async Task<IEnumerable<WeeklyGoal>> GetAllByUserIdAsync(int userId)
        {
            return await _context.WeeklyGoals
                .Where(w => w.UserId == userId && !w.IsDeleted)
                .OrderByDescending(w => w.WeekStartDate)
                .ToListAsync();
        }

        public async Task<int> CreateAsync(WeeklyGoal weeklyGoal)
        {
            _context.WeeklyGoals.Add(weeklyGoal);
            await _context.SaveChangesAsync();
            return weeklyGoal.WeeklyGoalId;
        }

        public async Task<bool> UpdateAsync(WeeklyGoal weeklyGoal)
        {
            weeklyGoal.UpdatedAt = DateTime.UtcNow;
            _context.WeeklyGoals.Update(weeklyGoal);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var weeklyGoal = await _context.WeeklyGoals.FindAsync(id);
            if (weeklyGoal == null)
            {
                return false;
            }

            weeklyGoal.IsDeleted = true;
            weeklyGoal.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.WeeklyGoals.AnyAsync(w => w.WeeklyGoalId == id && !w.IsDeleted);
        }

        public async Task<int> GetApplicationCountForWeekAsync(int userId, DateOnly weekStart, DateOnly weekEnd)
        {
            return await _context.Applications
                .Where(a => a.UserId == userId 
                    && !a.IsDeleted
                    && a.SubmissionDate >= weekStart 
                    && a.SubmissionDate <= weekEnd)
                .CountAsync();
        }

        private IQueryable<WeeklyGoal> ApplySorting(IQueryable<WeeklyGoal> query, string sortBy, bool descending)
        {
            Expression<Func<WeeklyGoal, object>> keySelector = sortBy.ToLower() switch
            {
                "weekstartdate" => w => w.WeekStartDate,
                "targetcount" => w => w.TargetApplicationCount,
                "createdat" => w => w.CreatedAt,
                "updatedat" => w => w.UpdatedAt,
                _ => w => w.WeekStartDate // Default sorting
            };

            return descending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
        }
    }
}

