using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class PasswordResetTokenRepository : IPasswordResetTokenRepository
    {
        private readonly GPDBContext _context;

        public PasswordResetTokenRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<PasswordResetToken> CreateAsync(PasswordResetToken token)
        {
            _context.PasswordResetTokens.Add(token);
            await _context.SaveChangesAsync();
            return token;
        }

        public async Task<PasswordResetToken?> GetActiveByHashAsync(byte[] tokenHash)
        {
            var now = DateTime.UtcNow;
            return await _context.PasswordResetTokens
                .Include(t => t.User)
                .Where(t => t.TokenHash == tokenHash && t.UsedAt == null && t.ExpiresAt > now)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> MarkUsedAsync(int id)
        {
            var entity = await _context.PasswordResetTokens.FirstOrDefaultAsync(t => t.Id == id);
            if (entity == null) return false;
            entity.UsedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task CleanupExpiredAsync()
        {
            var now = DateTime.UtcNow;
            var expired = await _context.PasswordResetTokens
                .Where(t => t.ExpiresAt <= now || t.UsedAt != null)
                .ToListAsync();

            if (expired.Count == 0) return;
            _context.RemoveRange(expired);
            await _context.SaveChangesAsync();
        }
    }
}


