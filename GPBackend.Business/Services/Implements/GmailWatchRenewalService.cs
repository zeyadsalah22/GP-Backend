using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GPBackend.Services.Implements
{
    /// <summary>
    /// Background service that automatically renews Gmail watches before they expire
    /// Runs daily and renews watches that will expire within 2 days
    /// </summary>
    public class GmailWatchRenewalService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<GmailWatchRenewalService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromHours(12); // Check twice daily

        public GmailWatchRenewalService(
            IServiceProvider serviceProvider,
            ILogger<GmailWatchRenewalService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Gmail Watch Renewal Service started");

            // Wait 1 minute after startup before first check
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await RenewExpiringWatchesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while renewing Gmail watches");
                }

                // Wait for next check interval
                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("Gmail Watch Renewal Service stopped");
        }

        private async Task RenewExpiringWatchesAsync(CancellationToken cancellationToken)
        {
            // Create a scope to resolve scoped services
            using var scope = _serviceProvider.CreateScope();
            var gmailConnectionRepository = scope.ServiceProvider.GetRequiredService<IGmailConnectionRepository>();
            var gmailService = scope.ServiceProvider.GetRequiredService<IGmailService>();

            try
            {
                // Get all active connections
                var activeConnections = await gmailConnectionRepository.GetAllActiveConnectionsAsync();

                if (!activeConnections.Any())
                {
                    _logger.LogInformation("No active Gmail connections to check for renewal");
                    return;
                }

                _logger.LogInformation("Checking {Count} Gmail connections for watch renewal", activeConnections.Count());

                var now = DateTime.UtcNow;
                var renewalThreshold = now.AddDays(2); // Renew if expires within 2 days

                int renewedCount = 0;
                int errorCount = 0;

                foreach (var connection in activeConnections)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    try
                    {
                        // Check if watch needs renewal
                        if (connection.WatchExpiresAt.HasValue && connection.WatchExpiresAt.Value <= renewalThreshold)
                        {
                            var expiresIn = connection.WatchExpiresAt.Value - now;
                            _logger.LogInformation(
                                "Renewing Gmail watch for user {UserId} (expires in {Hours} hours)",
                                connection.UserId,
                                expiresIn.TotalHours);

                            // Renew the watch
                            var success = await gmailService.RenewGmailWatchAsync(connection.UserId);

                            if (success)
                            {
                                renewedCount++;
                                _logger.LogInformation("Successfully renewed Gmail watch for user {UserId}", connection.UserId);
                            }
                            else
                            {
                                errorCount++;
                                _logger.LogWarning("Failed to renew Gmail watch for user {UserId}", connection.UserId);
                            }
                        }
                        else if (!connection.WatchExpiresAt.HasValue)
                        {
                            // Connection has no watch set up, try to set it up
                            _logger.LogWarning(
                                "User {UserId} has no watch expiration set, attempting to set up watch",
                                connection.UserId);

                            var success = await gmailService.RenewGmailWatchAsync(connection.UserId);
                            if (success)
                            {
                                renewedCount++;
                                _logger.LogInformation("Successfully set up Gmail watch for user {UserId}", connection.UserId);
                            }
                        }

                        // Small delay between users to avoid rate limiting
                        await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        _logger.LogError(ex, "Error renewing watch for user {UserId}", connection.UserId);
                    }
                }

                if (renewedCount > 0 || errorCount > 0)
                {
                    _logger.LogInformation(
                        "Gmail watch renewal completed: {Renewed} renewed, {Errors} errors",
                        renewedCount,
                        errorCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RenewExpiringWatchesAsync");
            }
        }
    }
}

