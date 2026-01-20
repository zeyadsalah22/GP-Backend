using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GPBackend.Services.Interfaces;
using MimeKit;

namespace GPBackend.Services.Implements
{
    public class GmailApiEmailSender : IGmailApiEmailSender
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GmailApiEmailSender> _logger;

        private const string GoogleTokenEndpoint = "https://oauth2.googleapis.com/token";
        private const string GmailSendEndpoint = "https://gmail.googleapis.com/gmail/v1/users/me/messages/send";

        public GmailApiEmailSender(HttpClient httpClient, IConfiguration configuration, ILogger<GmailApiEmailSender> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendAsync(MimeMessage message, CancellationToken cancellationToken = default)
        {
            var clientId = _configuration["GmailSender:ClientId"];
            var clientSecret = _configuration["GmailSender:ClientSecret"];
            var refreshToken = _configuration["GmailSender:RefreshToken"];

            if (string.IsNullOrWhiteSpace(clientId) ||
                string.IsNullOrWhiteSpace(clientSecret) ||
                string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new InvalidOperationException("GmailSender credentials are not configured (ClientId/ClientSecret/RefreshToken).");
            }

            var accessToken = await GetAccessTokenAsync(clientId, clientSecret, refreshToken, cancellationToken);

            // Encode raw RFC822 message as base64url per Gmail API requirements
            string raw;
            using (var ms = new MemoryStream())
            {
                await message.WriteToAsync(ms, cancellationToken);
                raw = Base64UrlEncode(ms.ToArray());
            }

            using var req = new HttpRequestMessage(HttpMethod.Post, GmailSendEndpoint);
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            req.Content = new StringContent(JsonSerializer.Serialize(new { raw }), Encoding.UTF8, "application/json");

            using var resp = await _httpClient.SendAsync(req, cancellationToken);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("Gmail API send failed: Status={StatusCode}, Body={Body}", (int)resp.StatusCode, body);
                throw new InvalidOperationException($"Gmail API send failed with status {(int)resp.StatusCode}.");
            }
        }

        private async Task<string> GetAccessTokenAsync(string clientId, string clientSecret, string refreshToken, CancellationToken cancellationToken)
        {
            using var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["refresh_token"] = refreshToken,
                ["grant_type"] = "refresh_token"
            });

            using var resp = await _httpClient.PostAsync(GoogleTokenEndpoint, content, cancellationToken);
            var json = await resp.Content.ReadAsStringAsync(cancellationToken);

            if (!resp.IsSuccessStatusCode)
            {
                _logger.LogError("Google token refresh failed: Status={StatusCode}, Body={Body}", (int)resp.StatusCode, json);
                throw new InvalidOperationException("Failed to refresh GmailSender access token.");
            }

            var token = JsonSerializer.Deserialize<TokenResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (token?.AccessToken is null || string.IsNullOrWhiteSpace(token.AccessToken))
            {
                _logger.LogError("Google token refresh returned invalid response: {Body}", json);
                throw new InvalidOperationException("Invalid token response from Google.");
            }

            return token.AccessToken;
        }

        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        private sealed class TokenResponse
        {
            public string? AccessToken { get; set; }
            public int ExpiresIn { get; set; }
            public string? TokenType { get; set; }

            // Google uses snake_case
            public string? access_token { set => AccessToken = value; }
            public int expires_in { set => ExpiresIn = value; }
            public string? token_type { set => TokenType = value; }
        }
    }
}


