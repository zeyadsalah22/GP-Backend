using GPBackend.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace GPBackend.Services.Implements
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private readonly ConcurrentDictionary<string, DateTime> _blacklistedTokens = new();
        private readonly Timer _cleanupTimer;

        public TokenBlacklistService()
        {
            // Run cleanup every hour
            _cleanupTimer = new Timer(_ => CleanupExpiredTokens(), null, TimeSpan.Zero, TimeSpan.FromHours(1));
        }

        public void BlacklistToken(string token, DateTime expiry)
        {
            _blacklistedTokens.TryAdd(token, expiry);
        }

        public bool IsBlacklisted(string token)
        {
            return _blacklistedTokens.ContainsKey(token);
        }

        public void CleanupExpiredTokens()
        {
            var now = DateTime.UtcNow;
            
            // Find all expired tokens
            var expiredTokens = _blacklistedTokens
                .Where(pair => pair.Value < now)
                .Select(pair => pair.Key)
                .ToList();

            // Remove expired tokens from the dictionary
            foreach (var token in expiredTokens)
            {
                _blacklistedTokens.TryRemove(token, out _);
            }
        }
    }
} 