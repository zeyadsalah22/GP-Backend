using Microsoft.EntityFrameworkCore;
using GPBackend.DTOs.Application;
using GPBackend.DTOs.Common;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using System.Linq.Expressions;

namespace GPBackend.Repositories.Implements
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly GPDBContext _context;

        public ApplicationRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<Application?> GetByIdAsync(int id)
        {
            return await _context.Applications
                .Include(a => a.UserCompany)
                .ThenInclude(uc => uc.Company)
                .FirstOrDefaultAsync(a => a.ApplicationId == id && !a.IsDeleted);
        }

        public async Task<PagedResult<Application>> GetFilteredApplicationsAsync(int userId, ApplicationQueryDto queryDto)
        {
            IQueryable<Application> query = _context.Applications
                .Include(a => a.UserCompany)
                .ThenInclude(uc => uc.Company)
                .Where(a => a.UserId == userId && !a.IsDeleted);

            // Apply filters
            if (queryDto.CompanyId.HasValue)
            {
                query = query.Where(a => a.CompanyId == queryDto.CompanyId.Value);
            }

            if (!string.IsNullOrWhiteSpace(queryDto.CompanyName))
            {
                query = query.Where(a => a.UserCompany.Company.Name.Contains(queryDto.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(queryDto.JobTitle))
            {
                query = query.Where(a => a.JobTitle.Contains(queryDto.JobTitle));
            }

            if (!string.IsNullOrWhiteSpace(queryDto.JobType))
            {
                query = query.Where(a => a.JobType == queryDto.JobType);
            }

            if (!string.IsNullOrWhiteSpace(queryDto.Stage))
            {
                query = query.Where(a => a.Stage == queryDto.Stage);
            }

            if (!string.IsNullOrWhiteSpace(queryDto.Status))
            {
                query = query.Where(a => a.Status == queryDto.Status);
            }

            if (queryDto.FromDate.HasValue)
            {
                query = query.Where(a => a.SubmissionDate >= queryDto.FromDate.Value);
            }

            if (queryDto.ToDate.HasValue)
            {
                query = query.Where(a => a.SubmissionDate <= queryDto.ToDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(queryDto.SearchTerm))
            {
                query = query.Where(a => 
                    a.JobTitle.Contains(queryDto.SearchTerm) || 
                    a.Description != null && a.Description.Contains(queryDto.SearchTerm) ||
                    a.UserCompany.Company.Name.Contains(queryDto.SearchTerm)
                );
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(queryDto.SortBy))
            {
                query = ApplySorting(query, queryDto.SortBy, queryDto.SortDescending);
            }
            else
            {
                // Default sorting by submission date descending
                query = query.OrderByDescending(a => a.SubmissionDate);
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

            // Return result
            return new PagedResult<Application>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber
            };
        }

        public async Task<IEnumerable<Application>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Applications
                .Include(a => a.UserCompany)
                .ThenInclude(uc => uc.Company)
                .Where(a => a.UserId == userId && !a.IsDeleted)
                .OrderByDescending(a => a.SubmissionDate)
                .ToListAsync();
        }

        public async Task<int> CreateAsync(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return application.ApplicationId;
        }

        public async Task<bool> UpdateAsync(Application application)
        {
            application.UpdatedAt = DateTime.UtcNow;
            _context.Applications.Update(application);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return false;
            }

            application.IsDeleted = true;
            application.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Applications.AnyAsync(a => a.ApplicationId == id && !a.IsDeleted);
        }

        private IQueryable<Application> ApplySorting(IQueryable<Application> query, string sortBy, bool descending)
        {
            Expression<Func<Application, object>> keySelector = sortBy.ToLower() switch
            {
                "jobtitle" => a => a.JobTitle,
                "jobtype" => a => a.JobType,
                "company" => a => a.UserCompany.Company.Name,
                "stage" => a => a.Stage,
                "status" => a => a.Status,
                "submissiondate" => a => a.SubmissionDate,
                "atsscore" => a => a.AtsScore ?? 0,
                "createdat" => a => a.CreatedAt,
                "updatedat" => a => a.UpdatedAt,
                _ => a => a.SubmissionDate // Default sorting by submission date
            };

            return descending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
        }
    }
} 