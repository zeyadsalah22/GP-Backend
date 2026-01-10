using AutoMapper;
using GPBackend.DTOs.Gmail;
using GPBackend.DTOs.Notification;
using GPBackend.Models;
using GPBackend.Models.Enums;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class EmailProcessingService : IEmailProcessingService
    {
        private readonly IEmailApplicationUpdateRepository _emailUpdateRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly ILogger<EmailProcessingService> _logger;

        public EmailProcessingService(
            IEmailApplicationUpdateRepository emailUpdateRepository,
            IApplicationRepository applicationRepository,
            INotificationService notificationService,
            IMapper mapper,
            ILogger<EmailProcessingService> logger)
        {
            _emailUpdateRepository = emailUpdateRepository;
            _applicationRepository = applicationRepository;
            _notificationService = notificationService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<EmailProcessingResultDto> ProcessEmailUpdateAsync(EmailUpdateWebhookDto webhookData)
        {
            try
            {
                // Check if we've already processed this email
                if (await _emailUpdateRepository.EmailAlreadyProcessedAsync(webhookData.EmailData.EmailId, webhookData.UserId))
                {
                    _logger.LogInformation("Email {EmailId} already processed for user {UserId}", 
                        webhookData.EmailData.EmailId, webhookData.UserId);
                    
                    return new EmailProcessingResultDto
                    {
                        Success = true,
                        Message = "Email already processed",
                        WasApplied = false,
                        FailureReason = "Duplicate email"
                    };
                }

                // Get the matched application from n8n (if provided)
                // n8n is responsible for matching logic - backend only validates and persists
                Application? application = null;
                string? failureReason = null;
                
                if (webhookData.MatchedApplicationId.HasValue)
                {
                    application = await _applicationRepository.GetByIdAsync(webhookData.MatchedApplicationId.Value);
                    
                    // Validate the application belongs to the user (security check)
                    if (application == null)
                    {
                        failureReason = $"Application ID {webhookData.MatchedApplicationId.Value} not found";
                        _logger.LogWarning("Application {AppId} not found for user {UserId}", 
                            webhookData.MatchedApplicationId.Value, webhookData.UserId);
                    }
                    else if (application.UserId != webhookData.UserId)
                    {
                        failureReason = $"Application {webhookData.MatchedApplicationId.Value} does not belong to user";
                        _logger.LogWarning("Application {AppId} does not belong to user {UserId}", 
                            webhookData.MatchedApplicationId.Value, webhookData.UserId);
                        application = null;
                    }
                    else
                    {
                        _logger.LogInformation("Validated n8n-matched application: ID={AppId}, Company={Company}, Job={Job}", 
                            application.ApplicationId, 
                            application.UserCompany.Company.Name,
                            application.JobTitle);
                    }
                }
                else
                {
                    failureReason = "No matching application provided by n8n";
                    _logger.LogInformation("Email {EmailId} from user {UserId} has no matched application (low confidence or no match)", 
                        webhookData.EmailData.EmailId, webhookData.UserId);
                }

                // Parse email date
                DateTime emailDate;
                if (!DateTime.TryParse(webhookData.EmailData.Date, out emailDate))
                {
                    emailDate = DateTime.UtcNow;
                }

                // Parse enums from strings
                ApplicationDecisionStatus? detectedStatus = null;
                ApplicationStage? detectedStage = null;

                if (!string.IsNullOrEmpty(webhookData.DetectedStatus))
                {
                    Enum.TryParse<ApplicationDecisionStatus>(webhookData.DetectedStatus, true, out var status);
                    detectedStatus = status;
                }

                if (!string.IsNullOrEmpty(webhookData.DetectedStage))
                {
                    Enum.TryParse<ApplicationStage>(webhookData.DetectedStage, true, out var stage);
                    detectedStage = stage;
                }

                // Create audit record (always log, even if no match)
                var emailUpdate = new EmailApplicationUpdate
                {
                    ApplicationId = application?.ApplicationId ?? 0,
                    UserId = webhookData.UserId,
                    EmailId = webhookData.EmailData.EmailId,
                    EmailSubject = webhookData.EmailData.Subject,
                    EmailFrom = webhookData.EmailData.From,
                    EmailDate = emailDate,
                    EmailSnippet = webhookData.EmailData.Snippet,
                    DetectedStatus = detectedStatus,
                    DetectedStage = detectedStage,
                    Confidence = webhookData.Confidence,
                    CompanyNameHint = webhookData.CompanyNameHint,
                    MatchReasons = webhookData.MatchReasons,
                    WasApplied = application != null,
                    FailureReason = failureReason,
                    CreatedAt = DateTime.UtcNow
                };

                await _emailUpdateRepository.CreateAsync(emailUpdate);

                // If we found a matching application, update it
                if (application != null)
                {
                    // Capture old status/stage BEFORE updating
                    var oldStatus = application.Status;
                    var oldStage = application.Stage;
                    var companyName = application.UserCompany.Company.Name;
                    var jobTitle = application.JobTitle;

                    var updated = await UpdateApplicationFromEmailAsync(
                        application.ApplicationId,
                        webhookData.UserId,
                        webhookData.DetectedStatus,
                        webhookData.DetectedStage,
                        webhookData.EmailData.Subject,
                        webhookData.EmailData.From
                    );

                    if (updated)
                    {
                        _logger.LogInformation("Successfully updated application {ApplicationId} from email {EmailId}", 
                            application.ApplicationId, webhookData.EmailData.EmailId);

                        // Send notification if status or stage changed
                        await SendNotificationIfChangedAsync(
                            webhookData.UserId,
                            application.ApplicationId,
                            companyName,
                            jobTitle,
                            oldStatus,
                            detectedStatus,
                            oldStage,
                            detectedStage
                        );

                        return new EmailProcessingResultDto
                        {
                            Success = true,
                            Message = "Application updated successfully",
                            ApplicationId = application.ApplicationId,
                            WasApplied = true
                        };
                    }
                }

                // No match found or update failed
                _logger.LogInformation("Email {EmailId} processed but not applied to any application. Reason: {Reason}", 
                    webhookData.EmailData.EmailId, failureReason ?? "Unknown");

                return new EmailProcessingResultDto
                {
                    Success = true,
                    Message = "Email processed but no matching application found",
                    WasApplied = false,
                    FailureReason = failureReason ?? "No matching application found"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing email update for user {UserId}", webhookData.UserId);
                
                return new EmailProcessingResultDto
                {
                    Success = false,
                    Message = $"Error: {ex.Message}",
                    WasApplied = false,
                    FailureReason = ex.Message
                };
            }
        }

        public async Task<bool> UpdateApplicationFromEmailAsync(
            int applicationId,
            int userId,
            string? detectedStatus,
            string? detectedStage,
            string emailSubject,
            string emailFrom)
        {
            try
            {
                var application = await _applicationRepository.GetByIdAsync(applicationId);

                if (application == null || application.UserId != userId)
                {
                    return false;
                }

                // Parse enums
                ApplicationDecisionStatus? newStatus = null;
                ApplicationStage? newStage = null;

                if (!string.IsNullOrEmpty(detectedStatus))
                {
                    Enum.TryParse<ApplicationDecisionStatus>(detectedStatus, true, out var status);
                    newStatus = status;
                }

                if (!string.IsNullOrEmpty(detectedStage))
                {
                    Enum.TryParse<ApplicationStage>(detectedStage, true, out var stage);
                    newStage = stage;
                }

                // Only update if we detected valid status/stage
                if (!newStatus.HasValue && !newStage.HasValue)
                {
                    return false;
                }

                // Update application
                if (newStatus.HasValue)
                {
                    application.Status = newStatus.Value;
                }

                if (newStage.HasValue)
                {
                    application.Stage = newStage.Value;
                }

                application.UpdatedAt = DateTime.UtcNow;

                await _applicationRepository.UpdateAsync(application);

                // Add to stage history
                if (newStage.HasValue)
                {
                    await _applicationRepository.UpsertStageHistoryAsync(
                        applicationId,
                        newStage.Value,
                        DateOnly.FromDateTime(DateTime.UtcNow),
                        $"Auto-updated from email: {emailSubject.Substring(0, Math.Min(100, emailSubject.Length))}"
                    );
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating application {ApplicationId} from email", applicationId);
                return false;
            }
        }

        public async Task<IEnumerable<EmailApplicationUpdateResponseDto>> GetRecentUpdatesAsync(int userId, int limit = 50)
        {
            var updates = await _emailUpdateRepository.GetByUserIdAsync(userId, limit);
            return _mapper.Map<IEnumerable<EmailApplicationUpdateResponseDto>>(updates);
        }

        public async Task<IEnumerable<EmailApplicationUpdateResponseDto>> GetUnmatchedUpdatesAsync(int userId)
        {
            var unmatchedUpdates = await _emailUpdateRepository.GetUnmatchedByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<EmailApplicationUpdateResponseDto>>(unmatchedUpdates);
        }

        /// <summary>
        /// Send notification to user if application status or stage changed
        /// </summary>
        private async Task SendNotificationIfChangedAsync(
            int userId,
            int applicationId,
            string companyName,
            string jobTitle,
            ApplicationDecisionStatus oldStatus,
            ApplicationDecisionStatus? newStatus,
            ApplicationStage oldStage,
            ApplicationStage? newStage)
        {
            try
            {
                // Check if anything actually changed
                var statusChanged = newStatus.HasValue && oldStatus != newStatus.Value;
                var stageChanged = newStage.HasValue && oldStage != newStage.Value;

                if (!statusChanged && !stageChanged)
                {
                    _logger.LogInformation("No status/stage change detected for application {ApplicationId}, skipping notification", applicationId);
                    return;
                }

                // Build notification message
                var message = BuildNotificationMessage(
                    companyName,
                    jobTitle,
                    oldStatus,
                    newStatus,
                    oldStage,
                    newStage,
                    statusChanged,
                    stageChanged
                );

                // Create notification
                await _notificationService.CreateNotificationAsync(new NotificationCreateDto
                {
                    UserId = userId,
                    ActorId = 2,  // System actor (ID 2)
                    Type = NotificationType.Application,
                    NotificationCategory = NotificationCategory.System,
                    EntityTargetedId = applicationId,
                    Message = message
                });

                _logger.LogInformation("Notification sent to user {UserId} for application {ApplicationId} update", userId, applicationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending notification for application {ApplicationId} update", applicationId);
                // Don't throw - notification failure shouldn't stop email processing
            }
        }

        /// <summary>
        /// Build notification message based on what changed
        /// </summary>
        private string BuildNotificationMessage(
            string companyName,
            string jobTitle,
            ApplicationDecisionStatus oldStatus,
            ApplicationDecisionStatus? newStatus,
            ApplicationStage oldStage,
            ApplicationStage? newStage,
            bool statusChanged,
            bool stageChanged)
        {
            var messageParts = new List<string>();
            messageParts.Add($"Application to {companyName} ({jobTitle}) updated:");

            if (statusChanged && newStatus.HasValue)
            {
                messageParts.Add($"Status {oldStatus} → {newStatus.Value}");
            }

            if (stageChanged && newStage.HasValue)
            {
                messageParts.Add($"Stage {oldStage} → {newStage.Value}");
            }

            return string.Join(", ", messageParts);
        }
    }
}

