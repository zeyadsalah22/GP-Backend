using GPBackend.Services.Interfaces;

namespace GPBackend.BackgoundServices
{
    public class TokenCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TokenCleanupService> _logger;
        private readonly TimeSpan _period = TimeSpan.FromHours(24); // Run daily

        public TokenCleanupService(IServiceProvider serviceProvider, ILogger<TokenCleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var refreshTokenService = scope.ServiceProvider.GetRequiredService<IRefreshTokenService>();
                    
                    await refreshTokenService.CleanupExpiredTokensAsync();
                    _logger.LogInformation("Expired refresh tokens cleaned up at {Time}", DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while cleaning up expired tokens");
                }

                await Task.Delay(_period, stoppingToken);
            }
        }
    }
} 