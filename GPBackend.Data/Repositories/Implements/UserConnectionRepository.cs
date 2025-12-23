using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class UserConnectionRepository : IUserConnectionRepository
    {
        private readonly GPDBContext _context;

        public UserConnectionRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<UserConnection> AddOrUpdateConnectionAsync(int userId, string connectionId)
        {
            // Add new connection
            var newConnection = new UserConnection
            {
                UserId = userId,
                ConnectionId = connectionId,
                ConnectedAt = DateTime.UtcNow
            };
            await _context.UserConnections.AddAsync(newConnection);
            int rows = await _context.SaveChangesAsync();
            if (rows == 0){
                throw new Exception("Could add the connectionID in user connection repository");
            }
            
            return newConnection;
            
        }

        public async Task<UserConnection?> GetConnectionByUserIdAsync(int userId)
        {
            return await _context.UserConnections
                .AsNoTracking()
                .FirstOrDefaultAsync(uc => uc.UserId == userId);
        }

        public async Task<UserConnection?> GetConnectionByConnectionIdAsync(string connectionId)
        {
            return await _context.UserConnections
                .AsNoTracking()
                .FirstOrDefaultAsync(uc => uc.ConnectionId == connectionId);
        }

        public async Task<bool> RemoveConnectionAsync(int userId, string connectionId)
        {
            var connection = await _context.UserConnections
                .FirstOrDefaultAsync(uc => uc.UserId == userId &&
                                           uc.ConnectionId == connectionId);

            if (connection == null)
                return false;

            _context.UserConnections.Remove(connection);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveConnectionByConnectionIdAsync(string connectionId)
        {
            var connection = await _context.UserConnections
                .FirstOrDefaultAsync(uc => uc.ConnectionId == connectionId);

            if (connection == null)
                return false;

            _context.UserConnections.Remove(connection);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateLastActivityAsync(int userId)
        {
            var connection = await _context.UserConnections
                .FirstOrDefaultAsync(uc => uc.UserId == userId);

            if (connection == null)
                return false;

            connection.ConnectedAt = DateTime.UtcNow;
            _context.UserConnections.Update(connection);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<UserConnection>> GetAllActiveConnectionsAsync()
        {
            return await _context.UserConnections
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> IsUserConnectedAsync(int userId)
        {
            return await _context.UserConnections
                .AnyAsync(uc => uc.UserId == userId);
        }

        public async Task<List<string>> GetConnectionIdsByUserIdsAsync(List<int> userIds)
        {
            return await _context.UserConnections
                .AsNoTracking()
                .Where(uc => userIds.Contains(uc.UserId))
                .Select(uc => uc.ConnectionId)
                .ToListAsync();
        }
    }
}
