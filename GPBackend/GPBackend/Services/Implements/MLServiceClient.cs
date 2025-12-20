using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using GPBackend.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GPBackend.Services.Implements
{
    public class MLServiceClient : IMLServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MLServiceClient> _logger;

        public MLServiceClient(HttpClient httpClient, IConfiguration configuration, ILogger<MLServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<string>> GenerateQuestionsAsync(string description, int numQuestions = 3)
        {
            try
            {
                var requestBody = new
                {
                    description = description,
                    num_questions = numQuestions
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Calling ML service to generate questions. Description length: {Length}, NumQuestions: {NumQuestions}",
                    description?.Length ?? 0, numQuestions);

                var response = await _httpClient.PostAsync("generate-questions", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("ML service returned error status {StatusCode}: {Error}",
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"ML service returned status {response.StatusCode}: {errorContent}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("ML service response received: {Response}", responseContent);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<GenerateQuestionsResponse>(options);

                if (result == null || result.Questions == null || result.Questions.Count == 0)
                {
                    _logger.LogWarning("ML service returned empty questions list");
                    throw new InvalidOperationException("ML service returned empty questions list");
                }

                _logger.LogInformation("Successfully generated {Count} questions", result.Questions.Count);
                return result.Questions;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout while calling ML service to generate questions");
                throw new TimeoutException("ML service request timed out", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling ML service to generate questions");
                throw;
            }
        }

        public async Task<ResumeMatchingResponse> MatchResumeAsync(string base64Resume, string jobDescription)
        {
            try
            {
                var requestBody = new
                {
                    file = base64Resume,
                    JobDescription = jobDescription
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Calling ML service to match resume. Resume length: {Length}, JobDescription length: {JobDescLength}",
                    base64Resume?.Length ?? 0, jobDescription?.Length ?? 0);

                var response = await _httpClient.PostAsync("resume-matching", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("ML service returned error status {StatusCode}: {Error}",
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"ML service returned status {response.StatusCode}: {errorContent}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("ML service response received: {Response}", responseContent);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<ResumeMatchingResponseDto>(options);

                if (result == null)
                {
                    _logger.LogWarning("ML service returned null response");
                    throw new InvalidOperationException("ML service returned null response");
                }

                _logger.LogInformation("Successfully matched resume. Score: {Score}, MatchedSkills: {MatchedCount}, MissingSkills: {MissingCount}",
                    result.ResumeScore, result.MatchedSkills?.Count ?? 0, result.MissingSkills?.Count ?? 0);

                return new ResumeMatchingResponse
                {
                    MatchedSkills = result.MatchedSkills ?? new List<string>(),
                    MissingSkills = result.MissingSkills ?? new List<string>(),
                    ResumeScore = result.ResumeScore
                };
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout while calling ML service to match resume");
                throw new TimeoutException("ML service request timed out", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling ML service to match resume");
                throw;
            }
        }

        public async Task<bool> HealthCheckAsync()
        {
            try
            {
                // Try to call a simple endpoint or just check if the base URL is reachable (try a simple GET request)
                var response = await _httpClient.GetAsync("");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking ML service health");
                return false;
            }
        }

        private class GenerateQuestionsResponse
        {
            [JsonPropertyName("questions")]
            public List<string> Questions { get; set; } = new();
        }

        private class ResumeMatchingResponseDto
        {
            [JsonPropertyName("matchedSkills")]
            public List<string>? MatchedSkills { get; set; }

            [JsonPropertyName("MissingSkills")]
            public List<string>? MissingSkills { get; set; }

            [JsonPropertyName("ResumeScore")]
            public double ResumeScore { get; set; }
        }
    }
}

