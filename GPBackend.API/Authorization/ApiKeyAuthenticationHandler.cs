using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GPBackend.Authorization
{
    /// <summary>
    /// Authorization handler for n8n API Key authentication
    /// Validates X-N8N-API-Key header against configured value
    /// </summary>
    public class ApiKeyRequirement : IAuthorizationRequirement
    {
    }

    public class ApiKeyAuthenticationHandler : AuthorizationHandler<ApiKeyRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiKeyAuthenticationHandler> _logger;

        public ApiKeyAuthenticationHandler(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            ILogger<ApiKeyAuthenticationHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ApiKeyRequirement requirement)
        {
            _logger.LogInformation("🔍 ApiKeyAuthenticationHandler called");
            _logger.LogInformation("User.Identity.IsAuthenticated: {IsAuth}", context.User.Identity?.IsAuthenticated);
            _logger.LogInformation("Has NameIdentifier claim: {HasClaim}", context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier));
            
            // Check if user is already authenticated via JWT
            if (context.User.Identity?.IsAuthenticated == true &&
                context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                _logger.LogInformation("✅ User authenticated via JWT");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogWarning("❌ HttpContext is null");
                return Task.CompletedTask;
            }

            _logger.LogInformation("Request path: {Path}", httpContext.Request.Path);
            _logger.LogInformation("Request headers: {Headers}", string.Join(", ", httpContext.Request.Headers.Keys));

            // Check if X-N8N-API-Key header is present
            if (!httpContext.Request.Headers.TryGetValue("X-N8N-API-Key", out var apiKeyHeader))
            {
                _logger.LogWarning("❌ X-N8N-API-Key header not found, and user not authenticated via JWT");
                return Task.CompletedTask;
            }

            var providedApiKey = apiKeyHeader.ToString();
            var configuredApiKey = _configuration["N8nSettings:ApiKey"];

            if (string.IsNullOrEmpty(configuredApiKey))
            {
                _logger.LogError("N8nSettings:ApiKey is not configured in appsettings");
                return Task.CompletedTask;
            }

            // Validate API key
            if (providedApiKey != configuredApiKey)
            {
                _logger.LogWarning("❌ Invalid n8n API key provided. Expected: {Expected}, Got: {Got}", 
                    configuredApiKey?.Substring(0, 10) + "...", 
                    providedApiKey?.Substring(0, 10) + "...");
                return Task.CompletedTask;
            }

            _logger.LogInformation("✅ n8n API key validated successfully");

            // Add a claim to indicate this is an n8n authenticated request
            var claims = new List<Claim>
            {
                new Claim("AuthenticationType", "ApiKey"),
                new Claim("N8N", "true")
            };

            var identity = new ClaimsIdentity(claims, "ApiKey");
            var principal = new ClaimsPrincipal(identity);
            
            // CRITICAL: Update the HttpContext.User so controller-level [Authorize] sees the authenticated user
            httpContext.User = principal;

            _logger.LogInformation("✅ Claims added. HttpContext.User updated. IsAuthenticated: {IsAuth}", 
                httpContext.User.Identity?.IsAuthenticated);
            
            context.Succeed(requirement);
            
            _logger.LogInformation("✅ Authorization requirement succeeded");
            return Task.CompletedTask;
        }
    }
}

