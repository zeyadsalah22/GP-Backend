using GPBackend.DTOs.Common;
using GPBackend.DTOs.ResumeTest;
using GPBackend.DTOs.Skill;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GPBackend.Repositories.Implements
{
    public class ResumeTestRepository : IResumeTestRepository
    {
        private readonly GPDBContext _context;

        public ResumeTestRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ResumeTest>> GetAllResumeTestsAsync(int userId)
        {
            return await _context.ResumeTests
                .Include(rt => rt.Resume)
                .Include(rt => rt.Skills)
                .Where(rt => rt.Resume.UserId == userId)
                .OrderByDescending(rt => rt.TestDate)
                .ToListAsync();
        }
       
        public async Task<PagedResult<ResumeTest>> GetFilteredResumeTestAsync(int userId, ResumeTestQueryDto resumeTestQueryDto)
        {
            var query = _context.ResumeTests
                .Include(rt => rt.Resume)
                .Include(rt => rt.Skills)
                .Where(rt => rt.Resume.UserId == userId);

            // Apply filters

            if (resumeTestQueryDto.ResumeId.HasValue) // Filter by specific resume
            {
                query = query.Where(rt => rt.ResumeId == resumeTestQueryDto.ResumeId.Value);
            }

            /*
            if (!string.IsNullOrWhiteSpace(resumeTestQueryDto.JobDescription)) // Filter by job description
            {
                query = query.Where(rt => rt.JobDescription != null && rt.JobDescription.Contains(resumeTestQueryDto.JobDescription));
            }
            */

            if (resumeTestQueryDto.TestDate.HasValue) // Filter by date
            {
                query = query.Where(rt => rt.TestDate >= resumeTestQueryDto.TestDate.Value);
            }
            
            /*
            if (resumeTestQueryDto.MaxScore.HasValue)
            {
                query = query.Where(rt => rt.AtsScore <= resumeTestQueryDto.MaxScore.Value);
            }
            */

            if (resumeTestQueryDto.AtsScore.HasValue) // Filter by score
            {
                query = query.Where(rt => rt.AtsScore >= resumeTestQueryDto.AtsScore.Value);
            }

            
            // Apply search term filter
            if (!string.IsNullOrWhiteSpace(resumeTestQueryDto.SearchTerm))
            {
                var searchTerm = resumeTestQueryDto.SearchTerm.ToLower();
                query = query.Where(rt => 
                    rt.JobDescription != null && rt.JobDescription.ToLower().Contains(searchTerm)
                );
            }
            
            // Apply sorting
            if (!string.IsNullOrWhiteSpace(resumeTestQueryDto.SortBy))
            {
                query = ApplySorting(query, resumeTestQueryDto.SortBy, resumeTestQueryDto.SortDescending);
            }
            else
            {
                // Default sorting by start date descending
                query = query.OrderByDescending(rt => rt.TestDate);
            }

            /*
            query = resumeTestQueryDto.SortBy?.ToLower() switch
            {
                "atsScore" => resumeTestQueryDto.SortDescending ? query.OrderByDescending(rt => rt.AtsScore) : query.OrderBy(rt => rt.AtsScore),
                "jobDescription" => resumeTestQueryDto.SortDescending ? query.OrderByDescending(rt => rt.JobDescription) : query.OrderBy(rt => rt.JobDescription),
                "resumeId" => resumeTestQueryDto.SortDescending ? query.OrderByDescending(rt => rt.ResumeId) : query.OrderBy(rt => rt.ResumeId),
                _ => resumeTestQueryDto.SortDescending ? query.OrderByDescending(rt => rt.TestDate) : query.OrderBy(rt => rt.TestDate)
            };
            */

            // Apply pagination
            int totalCount = await query.CountAsync();
            var tests = await query
                .Skip((resumeTestQueryDto.PageNumber - 1) * resumeTestQueryDto.PageSize)
                .Take(resumeTestQueryDto.PageSize)
                .ToListAsync();

            return new PagedResult<ResumeTest>
            {
                Items = tests,
                TotalCount = totalCount,
                PageSize = resumeTestQueryDto.PageSize,
                PageNumber = resumeTestQueryDto.PageNumber
            };
        }


        public async Task<ResumeTest?> GetResumeTestByIdAsync(int testId, int userId)
        {
            return await _context.ResumeTests
                .Include(rt => rt.Resume)
                .Include(rt => rt.Skills)
                .FirstOrDefaultAsync(rt => rt.TestId == testId && rt.Resume.UserId == userId); // also check if isDeleted
             
        }

        public async Task<ResumeTest> CreateResumeTestAsync(ResumeTest resumeTest)
        {
            _context.ResumeTests.Add(resumeTest);
            await _context.SaveChangesAsync();
            return resumeTest;
        }

        public async Task<bool> UpdateResumeTestAsync(ResumeTest resumeTest)
        {
            try
            {
                _context.ResumeTests.Update(resumeTest);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteResumeTestAsync(int testId, int userId)
        {
            var resumeTest = await GetResumeTestByIdAsync(testId, userId);
            if (resumeTest == null)
            {
                return false;
            }
            
            _context.ResumeTests.Remove(resumeTest);
            return await _context.SaveChangesAsync() > 0;
        }

        private IQueryable<ResumeTest> ApplySorting(IQueryable<ResumeTest> query, string sortBy, bool sortDescending)
        {
            Expression<Func<ResumeTest, object>> keySelector = sortBy.ToLower() switch
            {
                "testDate" => rt => rt.TestDate,    
                "atsScore" => rt => rt.AtsScore,
                "resumeId" => rt => rt.ResumeId,
                

                _ => rt => rt.TestDate // Default sorting by test start date
            };
            return sortDescending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
        }
    }
} 