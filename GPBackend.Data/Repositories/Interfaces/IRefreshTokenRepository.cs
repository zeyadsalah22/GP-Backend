using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(int userId);
        Task<RefreshToken> CreateAsync(RefreshToken refreshToken);
        Task<bool> UpdateAsync(RefreshToken refreshToken);
        Task<bool> RevokeAllUserTokensAsync(int userId);
        Task<bool> RevokeTokenAsync(string token);
        Task CleanupExpiredTokensAsync();
    }
} 