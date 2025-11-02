using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using GPBackend.DTOs.Gmail;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GPBackend.Services.Implements
{
    public class GmailService : IGmailService
    {
        private readonly IGmailConnectionRepository _gmailConnectionRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<GmailService> _logger;

        public GmailService(
            IGmailConnectionRepository gmailConnectionRepository,
            IEncryptionService encryptionService,
            IMapper mapper,
            IConfiguration configuration,
            HttpClient httpClient,
            ILogger<GmailService> logger)
        {
            _gmailConnectionRepository = gmailConnectionRepository;
            _encryptionService = encryptionService;
            _mapper = mapper;
            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GenerateOAuthUrlAsync(int userId)
        {
            var clientId = _configuration["GoogleOAuth:ClientId"];
            var redirectUri = _configuration["GoogleOAuth:RedirectUri"];
            var scopes = _configuration["GoogleOAuth:Scopes"];

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(redirectUri))
            {
                throw new InvalidOperationException("Google OAuth configuration is missing");
            }

            // Generate OAuth URL with state parameter containing userId
            var state = Convert.ToBase64String(Encoding.UTF8.GetBytes(userId.ToString()));
            
            var authUrl = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                         $"client_id={Uri.EscapeDataString(clientId)}&" +
                         $"redirect_uri={Uri.EscapeDataString(redirectUri)}&" +
                         $"response_type=code&" +
                         $"scope={Uri.EscapeDataString(scopes ?? "https://www.googleapis.com/auth/gmail.readonly")}&" +
                         $"access_type=offline&" +  // Request refresh token
                         $"prompt=consent&" +  // Force consent screen to get refresh token
                         $"state={state}";

            return authUrl;
        }

        public async Task<bool> HandleOAuthCallbackAsync(int userId, string code)
        {
            try
            {
                _logger.LogInformation("Starting OAuth callback for user {UserId}", userId);
                
                // Exchange authorization code for tokens
                var tokens = await ExchangeCodeForTokensAsync(code);

                if (tokens == null)
                {
                    _logger.LogError("Failed to exchange code for tokens for user {UserId}. Check logs for Google API error.", userId);
                    return false;
                }

                _logger.LogInformation("Successfully exchanged code for tokens for user {UserId}", userId);

                // Get user's Gmail address
                var gmailAddress = await GetUserEmailAddressAsync(tokens.AccessToken);

                if (string.IsNullOrEmpty(gmailAddress))
                {
                    _logger.LogError("Failed to get Gmail address for user {UserId}", userId);
                    return false;
                }
                
                _logger.LogInformation("Successfully retrieved Gmail address {GmailAddress} for user {UserId}", gmailAddress, userId);

                // Check if connection already exists
                var existingConnection = await _gmailConnectionRepository.GetByUserIdAsync(userId);

                if (existingConnection != null)
                {
                    // Update existing connection
                    existingConnection.EncryptedAccessToken = _encryptionService.Encrypt(tokens.AccessToken);
                    existingConnection.EncryptedRefreshToken = _encryptionService.Encrypt(tokens.RefreshToken);
                    existingConnection.TokenExpiresAt = DateTime.UtcNow.AddSeconds(tokens.ExpiresIn);
                    existingConnection.GmailAddress = gmailAddress;
                    existingConnection.IsActive = true;
                    existingConnection.IsDeleted = false;

                    await _gmailConnectionRepository.UpdateAsync(existingConnection);
                }
                else
                {
                    // Create new connection
                    var newConnection = new GmailConnection
                    {
                        UserId = userId,
                        EncryptedAccessToken = _encryptionService.Encrypt(tokens.AccessToken),
                        EncryptedRefreshToken = _encryptionService.Encrypt(tokens.RefreshToken),
                        TokenExpiresAt = DateTime.UtcNow.AddSeconds(tokens.ExpiresIn),
                        GmailAddress = gmailAddress,
                        IsActive = true,
                        LastCheckedAt = DateTime.UtcNow.AddDays(-7) // Start checking from 7 days ago
                    };

                    await _gmailConnectionRepository.CreateAsync(newConnection);
                }

                // Setup Gmail watch for push notifications
                await SetupGmailWatchAsync(userId, tokens.AccessToken);

                _logger.LogInformation("Successfully connected Gmail for user {UserId}", userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling OAuth callback for user {UserId}", userId);
                return false;
            }
        }

        public async Task<bool> DisconnectGmailAsync(int userId)
        {
            try
            {
                var connection = await _gmailConnectionRepository.GetByUserIdAsync(userId);
                if (connection == null)
                {
                    return false;
                }

                // Optionally revoke token with Google
                try
                {
                    var accessToken = _encryptionService.Decrypt(connection.EncryptedAccessToken);
                    await RevokeTokenAsync(accessToken);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to revoke token for user {UserId}, continuing with disconnect", userId);
                }

                // Soft delete the connection
                await _gmailConnectionRepository.DeleteAsync(userId);

                _logger.LogInformation("Successfully disconnected Gmail for user {UserId}", userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disconnecting Gmail for user {UserId}", userId);
                return false;
            }
        }

        public async Task<GmailConnectionResponseDto?> GetConnectionStatusAsync(int userId)
        {
            var connection = await _gmailConnectionRepository.GetByUserIdAsync(userId);
            
            if (connection == null)
            {
                return null;
            }

            return _mapper.Map<GmailConnectionResponseDto>(connection);
        }

        public async Task<IEnumerable<ActiveGmailConnectionDto>> GetAllActiveConnectionsForN8nAsync()
        {
            var connections = await _gmailConnectionRepository.GetAllActiveConnectionsAsync();
            var result = new List<ActiveGmailConnectionDto>();

            foreach (var conn in connections)
            {
                var connection = conn;
                
                // Refresh token if expired
                if (connection.IsTokenExpired)
                {
                    await RefreshAccessTokenAsync(connection.UserId);
                    var refreshedConnection = await _gmailConnectionRepository.GetByUserIdAsync(connection.UserId);
                    if (refreshedConnection == null) continue;
                    connection = refreshedConnection;
                }

                var dto = new ActiveGmailConnectionDto
                {
                    UserId = connection.UserId,
                    UserEmail = connection.User.Email,
                    GmailAddress = connection.GmailAddress,
                    AccessToken = _encryptionService.Decrypt(connection.EncryptedAccessToken),
                    LastCheckedAt = (connection.LastCheckedAt ?? DateTime.UtcNow.AddDays(-7)).ToString("o"),
                    CredentialId = "default" // n8n will use default Gmail credentials
                };

                result.Add(dto);
            }

            return result;
        }

        public async Task<string?> GetDecryptedAccessTokenAsync(int userId)
        {
            var connection = await _gmailConnectionRepository.GetByUserIdAsync(userId);
            
            if (connection == null)
            {
                return null;
            }

            // Refresh if expired
            if (connection.IsTokenExpired)
            {
                await RefreshAccessTokenAsync(userId);
                connection = await _gmailConnectionRepository.GetByUserIdAsync(userId);
                if (connection == null) return null;
            }

            return _encryptionService.Decrypt(connection.EncryptedAccessToken);
        }

        public async Task<bool> RefreshAccessTokenAsync(int userId)
        {
            try
            {
                var connection = await _gmailConnectionRepository.GetByUserIdAsync(userId);
                if (connection == null)
                {
                    return false;
                }

                var refreshToken = _encryptionService.Decrypt(connection.EncryptedRefreshToken);
                var newTokens = await RefreshTokensAsync(refreshToken);

                if (newTokens == null)
                {
                    _logger.LogError("Failed to refresh tokens for user {UserId}", userId);
                    return false;
                }

                connection.EncryptedAccessToken = _encryptionService.Encrypt(newTokens.AccessToken);
                connection.TokenExpiresAt = DateTime.UtcNow.AddSeconds(newTokens.ExpiresIn);

                await _gmailConnectionRepository.UpdateAsync(connection);

                _logger.LogInformation("Successfully refreshed tokens for user {UserId}", userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing tokens for user {UserId}", userId);
                return false;
            }
        }

        public async Task<bool> UpdateLastCheckedAsync(int userId, DateTime lastChecked)
        {
            return await _gmailConnectionRepository.UpdateLastCheckedAsync(userId, lastChecked);
        }

        // Private helper methods

        private async Task<OAuthTokenResponse?> ExchangeCodeForTokensAsync(string code)
        {
            var clientId = _configuration["GoogleOAuth:ClientId"];
            var clientSecret = _configuration["GoogleOAuth:ClientSecret"];
            var redirectUri = _configuration["GoogleOAuth:RedirectUri"];

            var requestData = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", clientId! },
                { "client_secret", clientSecret! },
                { "redirect_uri", redirectUri! },
                { "grant_type", "authorization_code" }
            };

            var response = await _httpClient.PostAsync(
                "https://oauth2.googleapis.com/token",
                new FormUrlEncodedContent(requestData)
            );

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to exchange code: {Error}", error);
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<OAuthTokenResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return tokenResponse;
        }

        private async Task<OAuthTokenResponse?> RefreshTokensAsync(string refreshToken)
        {
            var clientId = _configuration["GoogleOAuth:ClientId"];
            var clientSecret = _configuration["GoogleOAuth:ClientSecret"];

            var requestData = new Dictionary<string, string>
            {
                { "refresh_token", refreshToken },
                { "client_id", clientId! },
                { "client_secret", clientSecret! },
                { "grant_type", "refresh_token" }
            };

            var response = await _httpClient.PostAsync(
                "https://oauth2.googleapis.com/token",
                new FormUrlEncodedContent(requestData)
            );

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<OAuthTokenResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return tokenResponse;
        }

        private async Task<string?> GetUserEmailAddressAsync(string accessToken)
        {
            try
            {
                _logger.LogInformation("Attempting to get Gmail profile. Token length: {TokenLength}, Token prefix: {TokenPrefix}", 
                    accessToken?.Length ?? 0, 
                    accessToken?.Substring(0, Math.Min(10, accessToken?.Length ?? 0)) ?? "null");

                // Create a new request with proper Authorization header
                var request = new HttpRequestMessage(HttpMethod.Get, "https://gmail.googleapis.com/gmail/v1/users/me/profile");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                _logger.LogInformation("Authorization header set: {HasAuth}, Value: Bearer {TokenPrefix}...", 
                    request.Headers.Authorization != null,
                    accessToken?.Substring(0, Math.Min(20, accessToken?.Length ?? 0)) ?? "null");

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to get Gmail profile. Status: {StatusCode}, Error: {Error}", 
                        response.StatusCode, errorContent);
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var profile = JsonSerializer.Deserialize<GmailProfileResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                _logger.LogInformation("Successfully retrieved Gmail profile for email: {Email}", profile?.EmailAddress);

                return profile?.EmailAddress;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception getting Gmail email address");
                return null;
            }
        }

        private async Task RevokeTokenAsync(string token)
        {
            await _httpClient.PostAsync(
                $"https://oauth2.googleapis.com/revoke?token={token}",
                null
            );
        }

        public async Task<GmailConnectionForN8nDto?> GetConnectionByEmailAsync(string gmailAddress)
        {
            var connection = await _gmailConnectionRepository.GetByGmailAddressAsync(gmailAddress);
            
            if (connection == null || !connection.IsActive || connection.IsDeleted)
            {
                return null;
            }

            // Refresh token if expired
            if (connection.IsTokenExpired)
            {
                await RefreshAccessTokenAsync(connection.UserId);
                var refreshedConnection = await _gmailConnectionRepository.GetByGmailAddressAsync(gmailAddress);
                if (refreshedConnection == null) return null;
                connection = refreshedConnection;
            }

            return new GmailConnectionForN8nDto
            {
                UserId = connection.UserId,
                UserEmail = connection.User.Email,
                GmailAddress = connection.GmailAddress,
                AccessToken = _encryptionService.Decrypt(connection.EncryptedAccessToken),
                HistoryId = connection.HistoryId
            };
        }

        public async Task<bool> UpdateHistoryIdAsync(int userId, string historyId)
        {
            try
            {
                var connection = await _gmailConnectionRepository.GetByUserIdAsync(userId);
                if (connection == null)
                {
                    return false;
                }

                connection.HistoryId = historyId;
                connection.UpdatedAt = DateTime.UtcNow;

                await _gmailConnectionRepository.UpdateAsync(connection);

                _logger.LogInformation("Updated history ID for user {UserId} to {HistoryId}", userId, historyId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating history ID for user {UserId}", userId);
                return false;
            }
        }

        public async Task<bool> RenewGmailWatchAsync(int userId)
        {
            try
            {
                var connection = await _gmailConnectionRepository.GetByUserIdAsync(userId);
                if (connection == null || !connection.IsActive)
                {
                    _logger.LogWarning("Cannot renew watch for user {UserId}: connection not found or inactive", userId);
                    return false;
                }

                // Get fresh access token (will refresh if expired)
                var accessToken = await GetDecryptedAccessTokenAsync(userId);
                if (string.IsNullOrEmpty(accessToken))
                {
                    _logger.LogError("Failed to get access token for user {UserId}", userId);
                    return false;
                }

                // Setup/renew the watch (reuses existing method)
                await SetupGmailWatchAsync(userId, accessToken);

                _logger.LogInformation("Successfully renewed Gmail watch for user {UserId}", userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error renewing Gmail watch for user {UserId}", userId);
                return false;
            }
        }

        private async Task SetupGmailWatchAsync(int userId, string accessToken)
        {
            try
            {
                var topicName = _configuration["GooglePubSub:TopicName"];
                
                if (string.IsNullOrEmpty(topicName))
                {
                    _logger.LogWarning("GooglePubSub:TopicName not configured, skipping watch setup for user {UserId}", userId);
                    return;
                }

                var watchRequest = new
                {
                    topicName = topicName,
                    labelIds = new[] { "INBOX" }
                };

                var request = new HttpRequestMessage(HttpMethod.Post, 
                    "https://gmail.googleapis.com/gmail/v1/users/me/watch");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                request.Content = new StringContent(
                    JsonSerializer.Serialize(watchRequest),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to setup Gmail watch for user {UserId}: {Error}", userId, responseContent);
                    return;
                }

                var watchResponse = JsonSerializer.Deserialize<WatchResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (watchResponse == null)
                {
                    _logger.LogError("Failed to deserialize watch response for user {UserId}", userId);
                    return;
                }

                // Update connection with historyId and watch expiration
                var connection = await _gmailConnectionRepository.GetByUserIdAsync(userId);
                if (connection != null)
                {
                    connection.HistoryId = watchResponse.HistoryId;
                    
                    // Parse expiration timestamp (milliseconds since epoch)
                    if (long.TryParse(watchResponse.Expiration, out long expirationMs))
                    {
                        connection.WatchExpiresAt = DateTimeOffset.FromUnixTimeMilliseconds(expirationMs).UtcDateTime;
                    }
                    else
                    {
                        // Default to 7 days from now
                        connection.WatchExpiresAt = DateTime.UtcNow.AddDays(7);
                    }

                    connection.UpdatedAt = DateTime.UtcNow;
                    await _gmailConnectionRepository.UpdateAsync(connection);

                    _logger.LogInformation("Gmail watch setup successfully for user {UserId}, historyId: {HistoryId}, expires: {ExpiresAt}", 
                        userId, watchResponse.HistoryId, connection.WatchExpiresAt);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting up Gmail watch for user {UserId}", userId);
                // Don't throw - watch setup is optional, user can still use polling
            }
        }

        // Helper classes for JSON deserialization
        private class OAuthTokenResponse
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; } = null!;
            
            [JsonPropertyName("refresh_token")]
            public string? RefreshToken { get; set; } = null!;
            
            [JsonPropertyName("expires_in")]
            public int ExpiresIn { get; set; }
            
            [JsonPropertyName("token_type")]
            public string TokenType { get; set; } = null!;
        }

        private class GmailProfileResponse
        {
            [JsonPropertyName("emailAddress")]
            public string EmailAddress { get; set; } = null!;
        }

        private class WatchResponse
        {
            [JsonPropertyName("historyId")]
            public string HistoryId { get; set; } = null!;
            
            [JsonPropertyName("expiration")]
            public string Expiration { get; set; } = null!;
        }
    }
}

