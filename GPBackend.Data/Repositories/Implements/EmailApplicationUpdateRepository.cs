using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class EmailApplicationUpdateRepository : IEmailApplicationUpdateRepository
    {
        private readonly GPDBContext _context;

        public EmailApplicationUpdateRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<EmailApplicationUpdate> CreateAsync(EmailApplicationUpdate update)
        {
            update.CreatedAt = DateTime.UtcNow;
            _context.EmailApplicationUpdates.Add(update);
            await _context.SaveChangesAsync();
            return update;
        }

        public async Task<IEnumerable<EmailApplicationUpdate>> GetByApplicationIdAsync(int applicationId)
        {
            return await _context.EmailApplicationUpdates
                .Where(eau => eau.ApplicationId == applicationId)
                .Include(eau => eau.Application)
                    .ThenInclude(a => a.UserCompany)
                        .ThenInclude(uc => uc.Company)
                .Include(eau => eau.User)
                .OrderByDescending(eau => eau.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmailApplicationUpdate>> GetByUserIdAsync(int userId, int limit = 50)
        {
            return await _context.EmailApplicationUpdates
                .Where(eau => eau.UserId == userId)
                .Include(eau => eau.Application)
                    .ThenInclude(a => a.UserCompany)
                        .ThenInclude(uc => uc.Company)
                .OrderByDescending(eau => eau.CreatedAt)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<bool> EmailAlreadyProcessedAsync(string emailId, int userId)
        {
            return await _context.EmailApplicationUpdates
                .AnyAsync(eau => eau.EmailId == emailId && eau.UserId == userId);
        }

        public async Task<IEnumerable<EmailApplicationUpdate>> GetUnmatchedByUserIdAsync(int userId)
        {
            return await _context.EmailApplicationUpdates
                .Where(eau => eau.UserId == userId && !eau.WasApplied)
                .OrderByDescending(eau => eau.CreatedAt)
                .ToListAsync();
        }
    }
}

