using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IPasswordResetTokenRepository
    {
        Task<PasswordResetToken> CreateAsync(PasswordResetToken token);
        Task<PasswordResetToken?> GetActiveByHashAsync(byte[] tokenHash);
        Task<bool> MarkUsedAsync(int id);
        Task CleanupExpiredAsync();
    }
}


