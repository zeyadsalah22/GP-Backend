using GPBackend.DTOs.Gmail;
using GPBackend.Models;

namespace GPBackend.Services.Interfaces
{
    public interface IEmailProcessingService
    {
        /// <summary>
        /// Main entry point from n8n webhook - process email and update application.
        /// n8n is responsible for email parsing and application matching.
        /// Backend validates, persists, and updates the matched application.
        /// </summary>
        Task<EmailProcessingResultDto> ProcessEmailUpdateAsync(EmailUpdateWebhookDto webhookData);

        /// <summary>
        /// Update application status/stage from email and create history record
        /// </summary>
        Task<bool> UpdateApplicationFromEmailAsync(
            int applicationId,
            int userId,
            string? detectedStatus,
            string? detectedStage,
            string emailSubject,
            string emailFrom);

        /// <summary>
        /// Get recent email updates for a user (audit trail)
        /// </summary>
        Task<IEnumerable<EmailApplicationUpdateResponseDto>> GetRecentUpdatesAsync(int userId, int limit = 50);

        /// <summary>
        /// Get unmatched email updates for manual linking UI
        /// </summary>
        Task<IEnumerable<EmailApplicationUpdateResponseDto>> GetUnmatchedUpdatesAsync(int userId);
    }
}

