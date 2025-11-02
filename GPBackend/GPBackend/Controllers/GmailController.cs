using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GPBackend.DTOs.Gmail;
using GPBackend.Services.Interfaces;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GmailController : ControllerBase
    {
        private readonly IGmailService _gmailService;
        private readonly IEmailProcessingService _emailProcessingService;
        private readonly ILogger<GmailController> _logger;
        private readonly IConfiguration _configuration;

        public GmailController(
            IGmailService gmailService,
            IEmailProcessingService emailProcessingService,
            ILogger<GmailController> logger,
            IConfiguration configuration)
        {
            _gmailService = gmailService;
            _emailProcessingService = emailProcessingService;
            _logger = logger;
            _configuration = configuration;
        }

        // ============================================
        // User-facing endpoints (JWT authenticated)
        // ============================================

        /// <summary>
        /// Initiate Gmail OAuth flow - returns OAuth URL for user to visit
        /// </summary>
        [HttpGet("connect")]
        [Authorize(Policy = "UserOrAdmin")]
        public async Task<ActionResult> InitiateOAuth()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID in token" });
            }

            try
            {
                var authUrl = await _gmailService.GenerateOAuthUrlAsync(userId);
                return Ok(new { authUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating OAuth URL for user {UserId}", userId);
                return StatusCode(500, new { message = "Error initiating Gmail connection" });
            }
        }

        /// <summary>
        /// Handle OAuth callback - exchange code for tokens
        /// </summary>
        [HttpPost("callback")]
        [Authorize(Policy = "UserOrAdmin")]
        public async Task<ActionResult> HandleOAuthCallback([FromBody] OAuthCallbackDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID in token" });
            }

            try
            {
                var success = await _gmailService.HandleOAuthCallbackAsync(userId, dto.Code);

                if (success)
                {
                    return Ok(new { message = "Gmail connected successfully" });
                }

                return BadRequest(new { message = "Failed to connect Gmail. Please try again." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling OAuth callback for user {UserId}", userId);
                return StatusCode(500, new { message = "Error connecting Gmail" });
            }
        }

        /// <summary>
        /// Disconnect Gmail and revoke access
        /// </summary>
        [HttpDelete("disconnect")]
        [Authorize(Policy = "UserOrAdmin")]
        public async Task<ActionResult> DisconnectGmail()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID in token" });
            }

            try
            {
                var success = await _gmailService.DisconnectGmailAsync(userId);

                if (success)
                {
                    return Ok(new { message = "Gmail disconnected successfully" });
                }

                return NotFound(new { message = "No Gmail connection found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disconnecting Gmail for user {UserId}", userId);
                return StatusCode(500, new { message = "Error disconnecting Gmail" });
            }
        }

        /// <summary>
        /// Get Gmail connection status for current user
        /// </summary>
        [HttpGet("status")]
        [Authorize(Policy = "UserOrAdmin")]
        public async Task<ActionResult<GmailConnectionResponseDto>> GetConnectionStatus()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID in token" });
            }

            try
            {
                var status = await _gmailService.GetConnectionStatusAsync(userId);

                if (status == null)
                {
                    return Ok(new { connected = false });
                }

                return Ok(status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting connection status for user {UserId}", userId);
                return StatusCode(500, new { message = "Error retrieving connection status" });
            }
        }

        /// <summary>
        /// Get recent email updates (audit trail)
        /// </summary>
        [HttpGet("recent-updates")]
        [Authorize(Policy = "UserOrAdmin")]
        public async Task<ActionResult<IEnumerable<EmailApplicationUpdateResponseDto>>> GetRecentUpdates([FromQuery] int limit = 50)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID in token" });
            }

            try
            {
                var updates = await _emailProcessingService.GetRecentUpdatesAsync(userId, limit);
                return Ok(updates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting recent updates for user {UserId}", userId);
                return StatusCode(500, new { message = "Error retrieving updates" });
            }
        }

        /// <summary>
        /// Get unmatched email updates (for manual linking)
        /// </summary>
        [HttpGet("unmatched-updates")]
        [Authorize(Policy = "UserOrAdmin")]
        public async Task<ActionResult<IEnumerable<EmailApplicationUpdateResponseDto>>> GetUnmatchedUpdates()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID in token" });
            }

            try
            {
                var unmatchedUpdates = await _emailProcessingService.GetUnmatchedUpdatesAsync(userId);
                return Ok(unmatchedUpdates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unmatched updates for user {UserId}", userId);
                return StatusCode(500, new { message = "Error retrieving unmatched updates" });
            }
        }

        // ============================================
        // n8n-facing endpoints (API Key authenticated)
        // ============================================

        /// <summary>
        /// Get all active Gmail connections for n8n polling
        /// Protected by API key in header
        /// </summary>
        [HttpGet("active-connections")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ActiveGmailConnectionDto>>> GetActiveConnections([FromHeader(Name = "X-N8N-API-Key")] string? apiKey)
        {
            // Validate API key
            var expectedApiKey = _configuration["N8nSettings:ApiKey"];
            if (string.IsNullOrEmpty(apiKey) || apiKey != expectedApiKey)
            {
                _logger.LogWarning("Unauthorized access attempt to active-connections endpoint");
                return Unauthorized(new { message = "Invalid API key" });
            }

            try
            {
                var connections = await _gmailService.GetAllActiveConnectionsForN8nAsync();
                return Ok(connections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active connections for n8n");
                return StatusCode(500, new { message = "Error retrieving connections" });
            }
        }

        /// <summary>
        /// Receive email update webhook from n8n
        /// Protected by API key in header
        /// </summary>
        [HttpPost("webhook/email-update")]
        [AllowAnonymous]
        public async Task<ActionResult<EmailProcessingResultDto>> ReceiveEmailUpdate(
            [FromBody] EmailUpdateWebhookDto dto,
            [FromHeader(Name = "X-N8N-API-Key")] string? apiKey)
        {
            // Validate API key
            var expectedApiKey = _configuration["N8nSettings:ApiKey"];
            if (string.IsNullOrEmpty(apiKey) || apiKey != expectedApiKey)
            {
                _logger.LogWarning("Unauthorized access attempt to webhook endpoint");
                return Unauthorized(new { message = "Invalid API key" });
            }

            try
            {
                var result = await _emailProcessingService.ProcessEmailUpdateAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing email update webhook");
                return StatusCode(500, new { message = "Error processing email update", error = ex.Message });
            }
        }

        /// <summary>
        /// Update last checked timestamp after n8n polls
        /// Protected by API key in header
        /// </summary>
        [HttpPost("update-last-checked")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateLastChecked(
            [FromBody] UpdateLastCheckedDto dto,
            [FromHeader(Name = "X-N8N-API-Key")] string? apiKey)
        {
            // Validate API key
            var expectedApiKey = _configuration["N8nSettings:ApiKey"];
            if (string.IsNullOrEmpty(apiKey) || apiKey != expectedApiKey)
            {
                _logger.LogWarning("Unauthorized access attempt to update-last-checked endpoint");
                return Unauthorized(new { message = "Invalid API key" });
            }

            try
            {
                // Parse ISO 8601 date string
                if (!DateTime.TryParse(dto.LastCheckedAt, out DateTime lastChecked))
                {
                    return BadRequest(new { message = "Invalid date format" });
                }

                var success = await _gmailService.UpdateLastCheckedAsync(dto.UserId, lastChecked);

                if (success)
                {
                    return Ok(new { message = "Last checked time updated successfully" });
                }

                return NotFound(new { message = "Gmail connection not found for user" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating last checked time");
                return StatusCode(500, new { message = "Error updating last checked time" });
            }
        }

        /// <summary>
        /// Get Gmail connection by email address
        /// Used by n8n workflow to get user connection from Pub/Sub notification
        /// Protected by API key in header
        /// </summary>
        [HttpGet("connection-by-email")]
        [AllowAnonymous]
        public async Task<ActionResult> GetConnectionByEmail(
            [FromQuery] string email,
            [FromHeader(Name = "X-N8N-API-Key")] string? apiKey)
        {
            // Validate API key
            var expectedApiKey = _configuration["N8nSettings:ApiKey"];
            if (string.IsNullOrEmpty(apiKey) || apiKey != expectedApiKey)
            {
                _logger.LogWarning("Unauthorized access attempt to connection-by-email endpoint");
                return Unauthorized(new { message = "Invalid API key" });
            }

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { message = "Email parameter is required" });
            }

            try
            {
                var connection = await _gmailService.GetConnectionByEmailAsync(email);
                
                if (connection == null)
                {
                    return NotFound(new { message = "No connection found for this email address" });
                }

                return Ok(connection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting connection by email");
                return StatusCode(500, new { message = "Error retrieving connection" });
            }
        }

        /// <summary>
        /// Update history ID after processing push notification
        /// Protected by API key in header
        /// </summary>
        [HttpPost("update-history-id")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateHistoryId(
            [FromBody] UpdateHistoryIdDto dto,
            [FromHeader(Name = "X-N8N-API-Key")] string? apiKey)
        {
            // Validate API key
            var expectedApiKey = _configuration["N8nSettings:ApiKey"];
            if (string.IsNullOrEmpty(apiKey) || apiKey != expectedApiKey)
            {
                _logger.LogWarning("Unauthorized access attempt to update-history-id endpoint");
                return Unauthorized(new { message = "Invalid API key" });
            }

            try
            {
                var success = await _gmailService.UpdateHistoryIdAsync(dto.UserId, dto.HistoryId);

                if (success)
                {
                    return Ok(new { message = "History ID updated successfully" });
                }

                return NotFound(new { message = "Gmail connection not found for user" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating history ID");
                return StatusCode(500, new { message = "Error updating history ID" });
            }
        }
    }
}

