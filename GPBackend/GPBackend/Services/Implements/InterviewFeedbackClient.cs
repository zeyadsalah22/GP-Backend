using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using GPBackend.DTOs.InterviewFeedback;
using GPBackend.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GPBackend.Services.Implements
{
    public class InterviewFeedbackClient : IInterviewFeedbackClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<InterviewFeedbackClient> _logger;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            // FastAPI expects camelCase field names (e.g., items/question/answer/context)
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public InterviewFeedbackClient(HttpClient httpClient, ILogger<InterviewFeedbackClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<InterviewFeedbackHealthResponseDto> HealthAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Calling Interview Feedback service health endpoint");

                using var response = await _httpClient.GetAsync("health", cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogError("Interview Feedback service returned error status {StatusCode}: {Error}",
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"Interview Feedback service returned status {response.StatusCode}: {errorContent}");
                }

                var result = await response.Content.ReadFromJsonAsync<InterviewFeedbackHealthResponseDto>(JsonOptions, cancellationToken);
                if (result == null)
                {
                    throw new InvalidOperationException("Interview Feedback service returned null health response");
                }

                return result;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout while calling Interview Feedback health endpoint");
                throw new TimeoutException("Interview Feedback service request timed out", ex);
            }
        }

        public async Task<GradeAnswerResponseDto> GradeAnswerAsync(GradeAnswerRequestDto request, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Calling Interview Feedback service to grade single answer");

                using var response = await _httpClient.PostAsJsonAsync("grade-answer", request, JsonOptions, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogError("Interview Feedback service returned error status {StatusCode}: {Error}",
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"Interview Feedback service returned status {response.StatusCode}: {errorContent}");
                }

                var result = await response.Content.ReadFromJsonAsync<GradeAnswerResponseDto>(JsonOptions, cancellationToken);
                if (result == null)
                {
                    throw new InvalidOperationException("Interview Feedback service returned null response for grade-answer");
                }

                return result;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout while grading single answer");
                throw new TimeoutException("Interview Feedback service request timed out", ex);
            }
        }

        public async Task<GradeAnswersBatchResponseDto> GradeAnswersBatchAsync(GradeAnswersBatchRequestDto request, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Calling Interview Feedback service to grade answers batch. Count={Count}", request.Items?.Count ?? 0);

                using var response = await _httpClient.PostAsJsonAsync("grade-answers-batch", request, JsonOptions, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogError("Interview Feedback service returned error status {StatusCode}: {Error}",
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"Interview Feedback service returned status {response.StatusCode}: {errorContent}");
                }

                var result = await response.Content.ReadFromJsonAsync<GradeAnswersBatchResponseDto>(JsonOptions, cancellationToken);
                if (result == null)
                {
                    throw new InvalidOperationException("Interview Feedback service returned null response for grade-answers-batch");
                }

                return result;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout while grading answers batch");
                throw new TimeoutException("Interview Feedback service request timed out", ex);
            }
        }

        public async Task<AnalyzeVideoResponseDto> AnalyzeVideoAsync(
            Stream videoStream,
            string fileName,
            string? contentType,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Calling Interview Feedback service to analyze video. FileName={FileName}", fileName);

                using var form = new MultipartFormDataContent();

                using var streamContent = new StreamContent(videoStream);
                if (!string.IsNullOrWhiteSpace(contentType))
                {
                    try
                    {
                        streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    }
                    catch (FormatException)
                    {
                        // ignore invalid content-type
                    }
                }

                form.Add(streamContent, "file", fileName);

                using var response = await _httpClient.PostAsync("analyze-video", form, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogError("Interview Feedback service returned error status {StatusCode}: {Error}",
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"Interview Feedback service returned status {response.StatusCode}: {errorContent}");
                }

                var result = await response.Content.ReadFromJsonAsync<AnalyzeVideoResponseDto>(JsonOptions, cancellationToken);
                if (result == null)
                {
                    throw new InvalidOperationException("Interview Feedback service returned null response for analyze-video");
                }

                return result;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout while analyzing interview video");
                throw new TimeoutException("Interview Feedback service request timed out", ex);
            }
        }
    }
}


