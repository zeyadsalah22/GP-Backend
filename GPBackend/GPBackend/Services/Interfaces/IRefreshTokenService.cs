using GPBackend.DTOs.Auth;
using GPBackend.Models;

namespace GPBackend.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken);
        Task<string> GenerateRefreshTokenAsync(User user);
        Task<bool> RevokeRefreshTokenAsync(string refreshToken);
        Task<bool> RevokeAllUserRefreshTokensAsync(int userId);
        Task CleanupExpiredTokensAsync();
    }
} 