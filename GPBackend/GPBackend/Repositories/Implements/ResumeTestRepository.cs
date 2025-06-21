using GPBackend.DTOs.Common;
using GPBackend.DTOs.ResumeTest;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class ResumeTestRepository : IResumeTestRepository
    {
        private readonly GPDBContext _context;

        public ResumeTestRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<ResumeTest?> GetByIdAsync(int testId, int userId)
        {
            return await _context.ResumeTests
                .Include(rt => rt.Resume)
                .Include(rt => rt.Skills)
                .Where(rt => rt.TestId == testId && rt.Resume.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ResumeTest>> GetAllByUserIdAsync(int userId)
        {
            return await _context.ResumeTests
                .Include(rt => rt.Resume)
                .Include(rt => rt.Skills)
                .Where(rt => rt.Resume.UserId == userId)
                .OrderByDescending(rt => rt.TestDate)
                .ToListAsync();
        }

        public async Task<PagedResult<ResumeTest>> GetFilteredResumeTestsAsync(int userId, ResumeTestQueryDto queryDto)
        {
            var query = _context.ResumeTests
                .Include(rt => rt.Resume)
                .Include(rt => rt.Skills)
                .Where(rt => rt.Resume.UserId == userId);

            // Apply filters
            if (queryDto.ResumeId.HasValue)
            {
                query = query.Where(rt => rt.ResumeId == queryDto.ResumeId.Value);
            }

            if (queryDto.FromDate.HasValue)
            {
                query = query.Where(rt => rt.TestDate >= queryDto.FromDate.Value);
            }

            if (queryDto.ToDate.HasValue)
            {
                query = query.Where(rt => rt.TestDate <= queryDto.ToDate.Value);
            }

            if (queryDto.MinScore.HasValue)
            {
                query = query.Where(rt => rt.AtsScore >= queryDto.MinScore.Value);
            }

            if (queryDto.MaxScore.HasValue)
            {
                query = query.Where(rt => rt.AtsScore <= queryDto.MaxScore.Value);
            }

            // Get total count
            var totalCount = await query.CountAsync();

            // Apply pagination
            var items = await query
                .OrderByDescending(rt => rt.TestDate)
                .Skip((queryDto.PageNumber - 1) * queryDto.PageSize)
                .Take(queryDto.PageSize)
                .ToListAsync();

            return new PagedResult<ResumeTest>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = queryDto.PageSize,
                PageNumber = queryDto.PageNumber
            };
        }

        public async Task<ResumeTest> CreateAsync(ResumeTest resumeTest)
        {
            _context.ResumeTests.Add(resumeTest);
            await _context.SaveChangesAsync();
            return resumeTest;
        }

        public async Task<bool> DeleteAsync(int testId, int userId)
        {
            var resumeTest = await GetByIdAsync(testId, userId);
            if (resumeTest == null)
            {
                return false;
            }

            _context.ResumeTests.Remove(resumeTest);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 