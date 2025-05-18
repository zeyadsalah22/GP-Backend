namespace GPBackend.Services.Interfaces
{
    public interface ITokenBlacklistService
    {
        void BlacklistToken(string token, DateTime expiry);
        bool IsBlacklisted(string token);
        void CleanupExpiredTokens();
    }
} 