using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface IEmailApplicationUpdateRepository
    {
        /// <summary>
        /// Create a new email application update record
        /// </summary>
        Task<EmailApplicationUpdate> CreateAsync(EmailApplicationUpdate update);

        /// <summary>
        /// Get all email updates for a specific application
        /// </summary>
        Task<IEnumerable<EmailApplicationUpdate>> GetByApplicationIdAsync(int applicationId);

        /// <summary>
        /// Get recent email updates for a user (for audit trail/transparency)
        /// </summary>
        Task<IEnumerable<EmailApplicationUpdate>> GetByUserIdAsync(int userId, int limit = 50);

        /// <summary>
        /// Check if an email has already been processed to avoid duplicates
        /// </summary>
        Task<bool> EmailAlreadyProcessedAsync(string emailId, int userId);

        /// <summary>
        /// Get unmatched email updates (where WasApplied = false) for manual linking UI
        /// </summary>
        Task<IEnumerable<EmailApplicationUpdate>> GetUnmatchedByUserIdAsync(int userId);
    }
}

