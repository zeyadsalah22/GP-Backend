using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class GmailConnectionRepository : IGmailConnectionRepository
    {
        private readonly GPDBContext _context;

        public GmailConnectionRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<GmailConnection?> GetByUserIdAsync(int userId)
        {
            return await _context.GmailConnections
                .Where(gc => !gc.IsDeleted && gc.UserId == userId)
                .Include(gc => gc.User)
                .FirstOrDefaultAsync();
        }

        public async Task<GmailConnection?> GetByGmailAddressAsync(string gmailAddress)
        {
            return await _context.GmailConnections
                .Where(gc => !gc.IsDeleted && gc.GmailAddress == gmailAddress)
                .Include(gc => gc.User)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GmailConnection>> GetAllActiveConnectionsAsync()
        {
            return await _context.GmailConnections
                .Where(gc => !gc.IsDeleted && gc.IsActive)
                .Include(gc => gc.User)
                .ToListAsync();
        }

        public async Task<GmailConnection> CreateAsync(GmailConnection connection)
        {
            connection.ConnectedAt = DateTime.UtcNow;
            connection.UpdatedAt = DateTime.UtcNow;
            connection.IsActive = true;
            connection.IsDeleted = false;

            _context.GmailConnections.Add(connection);
            await _context.SaveChangesAsync();
            return connection;
        }

        public async Task<bool> UpdateAsync(GmailConnection connection)
        {
            try
            {
                connection.UpdatedAt = DateTime.UtcNow;
                _context.GmailConnections.Update(connection);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistsForUserAsync(connection.UserId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            var connection = await GetByUserIdAsync(userId);
            if (connection == null)
            {
                return false;
            }

            // Soft delete
            connection.IsDeleted = true;
            connection.IsActive = false;
            connection.UpdatedAt = DateTime.UtcNow;
            _context.GmailConnections.Update(connection);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsForUserAsync(int userId)
        {
            return await _context.GmailConnections
                .AnyAsync(gc => !gc.IsDeleted && gc.UserId == userId);
        }

        public async Task<bool> UpdateLastCheckedAsync(int userId, DateTime lastChecked)
        {
            var connection = await GetByUserIdAsync(userId);
            if (connection == null)
            {
                return false;
            }

            connection.LastCheckedAt = lastChecked;
            connection.UpdatedAt = DateTime.UtcNow;
            _context.GmailConnections.Update(connection);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

