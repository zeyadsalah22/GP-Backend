using Microsoft.AspNetCore.Mvc;
using GPBackend.Models;
using GPBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Text.Json;
using GPBackend.DTOs.Chatbot;
using GPBackend.DTOs.Application;
using GPBackend.DTOs.Question;
using System.Security.Claims;
using Microsoft.AspNetCore.RateLimiting;

// [Authorize(Policy = "AdminOnly")]
[Authorize]
[EnableRateLimiting("ChatbotPerIp")]
[ApiController]
[Route("api/chatbot")]
public class ChatBotController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ChatBotController> _logger;
    private readonly string _n8nWebhookUrl;
    private readonly string _n8nApiKey;

    public ChatBotController(
        HttpClient httpClient, 
        IConfiguration configuration,
        ILogger<ChatBotController> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
        
        var baseUrl = _configuration["N8NChatbot:BaseUrl"] ?? "http://localhost:5678";
        var webhookPath = _configuration["N8NChatbot:WebhookPath"] ?? "/webhook/chatbot/send-message";
        _n8nWebhookUrl = $"{baseUrl}{webhookPath}";
        _n8nApiKey = _configuration["N8NChatbot:ApiKey"] ?? throw new InvalidOperationException("N8NChatbot:ApiKey is not configured");
        
        var timeoutSeconds = int.Parse(_configuration["N8NChatbot:TimeoutSeconds"] ?? "60");
        _httpClient.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
    }

    // Helper method to get the authenticated user's ID
    private int GetAuthenticatedUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            throw new UnauthorizedAccessException("User is not authenticated properly");
        }
        return userId;
    }

    // Initialize Chat Session
    [HttpPost("initialize/{userId}")]
    public IActionResult InitializeChat(int userId)
    {
        try
        {
            // Validate user is authenticated
            var authenticatedUserId = GetAuthenticatedUserId();
            if (authenticatedUserId != userId)
            {
                return Forbid("You can only initialize chat for your own account");
            }

            // Generate session ID for conversation memory in n8n
            var sessionId = Guid.NewGuid().ToString();
            
            _logger.LogInformation("Chat session initialized for UserId={UserId}, SessionId={SessionId}", 
                userId, sessionId);
            
            return Ok(new { 
                sessionId = sessionId,
                message = "Chat initialized successfully. You can now start asking questions about your job applications."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initializing chat for UserId={UserId}", userId);
            return StatusCode(500, new { error = ex.Message });
        }
    }

    // Send Message to n8n AI Agent
    [HttpPost("send-message")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequestDto request)
    {
        try
        {
            var userId = GetAuthenticatedUserId();
            var token = Request.Headers["Authorization"].ToString();
            
            _logger.LogInformation("Sending message to n8n chatbot: UserId={UserId}, SessionId={SessionId}", 
                userId, request.SessionId);
            
            var requestBody = new
            {
                session_id = request.SessionId,
                user_id = userId,
                message = request.Message,
                auth_token = token
            };
            
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            // Create request with authentication header
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, _n8nWebhookUrl)
            {
                Content = content
            };
            httpRequest.Headers.Add("N8N-Chatbot-API-Key", _n8nApiKey);
            
            // Call n8n webhook
            var response = await _httpClient.SendAsync(httpRequest);
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Received response from n8n chatbot for SessionId={SessionId}", 
                    request.SessionId);
                
                // Parse and return the response
                var result = JsonSerializer.Deserialize<JsonElement>(responseContent);
                return Ok(result);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("n8n chatbot returned error: StatusCode={StatusCode}, Error={Error}", 
                    response.StatusCode, errorContent);
                return StatusCode(500, new { error = "Failed to get response from chatbot", details = errorContent });
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error communicating with n8n chatbot");
            return StatusCode(503, new { error = "Chatbot service unavailable", details = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing chatbot message");
            return StatusCode(500, new { error = "Internal server error", details = ex.Message });
        }
    }
}


