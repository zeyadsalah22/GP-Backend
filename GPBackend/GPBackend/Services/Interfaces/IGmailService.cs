using GPBackend.DTOs.Gmail;

namespace GPBackend.Services.Interfaces
{
    public interface IGmailService
    {
        // OAuth flow
        
        /// <summary>
        /// Generate Google OAuth URL for user to grant permissions
        /// </summary>
        Task<string> GenerateOAuthUrlAsync(int userId);

        /// <summary>
        /// Handle OAuth callback and exchange code for tokens
        /// </summary>
        Task<bool> HandleOAuthCallbackAsync(int userId, string code);

        /// <summary>
        /// Disconnect Gmail and revoke access
        /// </summary>
        Task<bool> DisconnectGmailAsync(int userId);

        // Connection management
        
        /// <summary>
        /// Get Gmail connection status for a user
        /// </summary>
        Task<GmailConnectionResponseDto?> GetConnectionStatusAsync(int userId);

        /// <summary>
        /// Get all active Gmail connections with decrypted tokens for n8n
        /// </summary>
        Task<IEnumerable<ActiveGmailConnectionDto>> GetAllActiveConnectionsForN8nAsync();

        // Token management
        
        /// <summary>
        /// Get decrypted access token for a user
        /// </summary>
        Task<string?> GetDecryptedAccessTokenAsync(int userId);

        /// <summary>
        /// Refresh expired access token using refresh token
        /// </summary>
        Task<bool> RefreshAccessTokenAsync(int userId);

        // Monitoring
        
        /// <summary>
        /// Update last checked timestamp after n8n polls
        /// </summary>
        Task<bool> UpdateLastCheckedAsync(int userId, DateTime lastChecked);

        // Push notification support
        
        /// <summary>
        /// Get Gmail connection by email address (for n8n push workflow)
        /// </summary>
        Task<GmailConnectionForN8nDto?> GetConnectionByEmailAsync(string gmailAddress);

        /// <summary>
        /// Update history ID after processing push notification
        /// </summary>
        Task<bool> UpdateHistoryIdAsync(int userId, string historyId);

        /// <summary>
        /// Renew Gmail watch before it expires (should be called before 7 day expiration)
        /// </summary>
        Task<bool> RenewGmailWatchAsync(int userId);
    }
}

