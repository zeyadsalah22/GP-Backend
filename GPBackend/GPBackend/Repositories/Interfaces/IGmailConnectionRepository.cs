using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IGmailConnectionRepository
    {
        /// <summary>
        /// Get Gmail connection by user ID
        /// </summary>
        Task<GmailConnection?> GetByUserIdAsync(int userId);

        /// <summary>
        /// Get Gmail connection by Gmail address
        /// </summary>
        Task<GmailConnection?> GetByGmailAddressAsync(string gmailAddress);

        /// <summary>
        /// Get all active Gmail connections for n8n polling
        /// </summary>
        Task<IEnumerable<GmailConnection>> GetAllActiveConnectionsAsync();

        /// <summary>
        /// Create a new Gmail connection
        /// </summary>
        Task<GmailConnection> CreateAsync(GmailConnection connection);

        /// <summary>
        /// Update an existing Gmail connection
        /// </summary>
        Task<bool> UpdateAsync(GmailConnection connection);

        /// <summary>
        /// Delete (soft delete) a Gmail connection by user ID
        /// </summary>
        Task<bool> DeleteAsync(int userId);

        /// <summary>
        /// Check if a Gmail connection exists for a user
        /// </summary>
        Task<bool> ExistsForUserAsync(int userId);

        /// <summary>
        /// Update only the last checked timestamp
        /// </summary>
        Task<bool> UpdateLastCheckedAsync(int userId, DateTime lastChecked);
    }
}

