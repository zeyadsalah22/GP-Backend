using GPBackend.DTOs.Interview;
using GPBackend.DTOs.Common;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                        .Where(a => a.UserId == userId)
                        .ToListAsync();
        }

        public async Task<PagedResult<Interview>> GetFilteredInterviewsAsync(InterviewQueryDto interviewQueryDto)
        {
            
        }

        public async Task<Interview?> GetInterviewByIdAsync(int interviewId)
        {
            return await _context.Interviews
                    .Include(a => a.Application)
                    .Include(a => a.Company)
                    .FirstOrDefaultAsync(a => a.InterviewId == interviewId);
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
            Interview? interview = await GetInterviewByIdAsync(id);
            if (interview == null)
            {
                return false;
            }

            interview.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }
    } 
}