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

[Authorize(Policy = "AdminOnly")]
[EnableRateLimiting("ChatbotPerIp")]
[ApiController]
[Route("api/chatbot")]
public class ChatBotController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IApplicationService _applicationService;
    private readonly IQuestionService _questionService;
    private readonly string _pythonServiceUrl = "http://localhost:8001"; // Configure this

    public ChatBotController(HttpClient httpClient, IApplicationService applicationService, IQuestionService questionService)
    {
        _httpClient = httpClient;
        _applicationService = applicationService;
        _questionService = questionService;
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
    public async Task<IActionResult> InitializeChat(int userId)
    {
        try
        {
            // Get user's applications and questions data from your database
            var applicationsData = await _applicationService.GetFilteredApplicationsAsync(userId, new ApplicationQueryDto { PageSize = 100 });
            var questionsData = await _questionService.GetFilteredQuestionBasedOnQuery(userId, new QuestionQueryDto { PageSize = 100 });

            if (applicationsData.Items.Count == 0)
            {
                return BadRequest("No applications found, please create an application first");
            }

            var request = new
            {
                user_id = userId,
                applications_data = applicationsData,
                questions_data = questionsData
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_pythonServiceUrl}/initialize-chat", content);
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Ok(JsonSerializer.Deserialize<object>(responseContent));
            }
            
            return BadRequest("Failed to initialize chat session");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    // Send Message with Streaming Response
    [HttpPost("send-message")]
    public async Task SendMessage([FromBody] SendMessageRequestDto request)
    {
        try
        {
            var requestData = new
            {
                session_id = request.SessionId,
                message = request.Message
            };

            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Set response headers for streaming
            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("Connection", "keep-alive");

            var response = await _httpClient.PostAsync($"{_pythonServiceUrl}/send-message", content);
            
            if (response.IsSuccessStatusCode)
            {
                // Stream the response back to the client
                await using var stream = await response.Content.ReadAsStreamAsync();
                await stream.CopyToAsync(Response.Body);
            }
            else
            {
                Response.StatusCode = 500;
                await Response.WriteAsync("data: {\"type\":\"error\",\"data\":\"Failed to send message\"}\n\n");
            }
        }
        catch (Exception ex)
        {
            Response.StatusCode = 500;
            await Response.WriteAsync($"data: {{\"type\":\"error\",\"data\":\"{ex.Message}\"}}\n\n");
        }
    }

    // Close Chat Session
    [HttpPost("close-chat")]
    public async Task<IActionResult> CloseChat([FromBody] CloseChatRequestDto request)
    {
        try
        {
            var requestData = new { session_id = request.SessionId };
            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_pythonServiceUrl}/close-chat", content);
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Ok(JsonSerializer.Deserialize<object>(responseContent));
            }

            return BadRequest("Failed to close chat session");
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}


