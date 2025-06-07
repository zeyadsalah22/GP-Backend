using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly GPDBContext _context;

        public RefreshTokenRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(int userId)
        {
            return await _context.RefreshTokens
                .Where(rt => rt.UserId == userId && rt.IsActive)
                .ToListAsync();
        }

        public async Task<RefreshToken> CreateAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<bool> UpdateAsync(RefreshToken refreshToken)
        {
            try
            {
                _context.RefreshTokens.Update(refreshToken);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RevokeAllUserTokensAsync(int userId)
        {
            try
            {
                var tokens = await _context.RefreshTokens
                    .Where(rt => rt.UserId == userId && rt.IsActive)
                    .ToListAsync();

                foreach (var token in tokens)
                {
                    token.IsRevoked = true;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            try
            {
                var refreshToken = await GetByTokenAsync(token);
                if (refreshToken != null)
                {
                    refreshToken.IsRevoked = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task CleanupExpiredTokensAsync()
        {
            try
            {
                var expiredTokens = await _context.RefreshTokens
                    .Where(rt => rt.ExpiryDate < DateTime.UtcNow)
                    .ToListAsync();

                _context.RefreshTokens.RemoveRange(expiredTokens);
                await _context.SaveChangesAsync();
            }
            catch
            {
                // Log error in production
            }
        }
    }
} 