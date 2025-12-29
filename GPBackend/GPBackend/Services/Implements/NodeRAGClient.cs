using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using GPBackend.DTOs.NodeRAG;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class NodeRAGClient : INodeRAGClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NodeRAGClient> _logger;
        private readonly string _apiKey;
        private readonly int _maxRetries;

        public NodeRAGClient(HttpClient httpClient, IConfiguration configuration, ILogger<NodeRAGClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiKey = configuration["NodeRAG:ApiKey"] ?? throw new InvalidOperationException("NodeRAG:ApiKey not configured");
            _maxRetries = int.Parse(configuration["NodeRAG:MaxRetries"] ?? "3");
        }

        public async Task<NodeRAGDocumentUploadResponseDto> UploadDocumentAsync(
            int userId, 
            byte[] fileContent, 
            string filename, 
            string documentType = "resume")
        {
            try
            {
                _logger.LogInformation("Uploading document to NodeRAG: UserId={UserId}, Filename={Filename}, Size={Size}", 
                    userId, filename, fileContent.Length);

                // Create a function that creates a fresh request each time (for retry support)
                HttpResponseMessage response = await ExecuteWithRetryAsync(async () =>
                {
                    // NodeRAG expects multipart/form-data with file upload
                    var content = new MultipartFormDataContent();
                    var fileByteContent = new ByteArrayContent(fileContent);
                    fileByteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    
                    content.Add(fileByteContent, "file", filename);
                    content.Add(new StringContent(userId.ToString()), "user_id");
                    content.Add(new StringContent(documentType), "document_type");

                    var request = new HttpRequestMessage(HttpMethod.Post, "documents");
                    request.Headers.Add("X-API-Key", _apiKey);
                    request.Content = content;

                    return await _httpClient.SendAsync(request);
                });

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("NodeRAG document upload failed with status {StatusCode}: {Error}", 
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"NodeRAG returned status {response.StatusCode}: {errorContent}");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<NodeRAGDocumentUploadResponseDto>(options);
                
                if (result == null)
                {
                    throw new InvalidOperationException("NodeRAG returned null response");
                }

                _logger.LogInformation("Document uploaded successfully to NodeRAG: UserId={UserId}, RequiresRebuild={RequiresRebuild}", 
                    userId, result.RequiresRebuild);

                return result;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout uploading document to NodeRAG for UserId={UserId}", userId);
                throw new TimeoutException("NodeRAG request timed out", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading document to NodeRAG for UserId={UserId}", userId);
                throw;
            }
        }

        public async Task<NodeRAGBuildResponseDto> TriggerBuildAsync(
            int userId, 
            bool incremental = true, 
            bool syncToNeo4j = true, 
            bool forceRebuild = false)
        {
            try
            {
                _logger.LogInformation("Triggering build in NodeRAG: UserId={UserId}, Incremental={Incremental}", 
                    userId, incremental);

                var response = await ExecuteWithRetryAsync(async () =>
                {
                    var requestBody = new NodeRAGBuildRequestDto
                    {
                        UserId = userId.ToString(),
                        Incremental = incremental,
                        SyncToNeo4j = syncToNeo4j,
                        ForceRebuild = forceRebuild
                    };

                    var json = JsonSerializer.Serialize(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(HttpMethod.Post, "build");
                    request.Headers.Add("X-API-Key", _apiKey);
                    request.Content = content;

                    _logger.LogInformation("Sending build request to NodeRAG: BaseUrl={BaseUrl}, Timeout={Timeout}s", 
                        _httpClient.BaseAddress, _httpClient.Timeout.TotalSeconds);
                    
                    var responseTask = _httpClient.SendAsync(request);
                    _logger.LogInformation("Waiting for NodeRAG build response...");
                    
                    var result = await responseTask;
                    _logger.LogInformation("Received response from NodeRAG: StatusCode={StatusCode}", result.StatusCode);
                    
                    return result;
                });

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("NodeRAG build trigger failed with status {StatusCode}: {Error}", 
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"NodeRAG returned status {response.StatusCode}: {errorContent}");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<NodeRAGBuildResponseDto>(options);
                
                if (result == null)
                {
                    throw new InvalidOperationException("NodeRAG returned null response");
                }

                _logger.LogInformation("Build triggered successfully: UserId={UserId}, BuildId={BuildId}, Status={Status}", 
                    userId, result.BuildId, result.Status);

                return result;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout triggering build in NodeRAG for UserId={UserId}", userId);
                throw new TimeoutException("NodeRAG request timed out", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error triggering build in NodeRAG for UserId={UserId}", userId);
                throw;
            }
        }

        public async Task<NodeRAGBuildStatusDto> GetBuildStatusAsync(string buildId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"build/{buildId}/status");
                request.Headers.Add("X-API-Key", _apiKey);

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("NodeRAG build status check failed with status {StatusCode}: {Error}", 
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"NodeRAG returned status {response.StatusCode}: {errorContent}");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<NodeRAGBuildStatusDto>(options);
                
                if (result == null)
                {
                    throw new InvalidOperationException("NodeRAG returned null response");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking build status for BuildId={BuildId}", buildId);
                throw;
            }
        }

        public async Task<NodeRAGAnswerResponseDto> GenerateAnswerAsync(
            int userId, 
            string query, 
            string? jobContext = null, 
            int topK = 10)
        {
            try
            {
                _logger.LogInformation("Generating answer in NodeRAG: UserId={UserId}, QueryLength={QueryLength}", 
                    userId, query.Length);

                var response = await ExecuteWithRetryAsync(async () =>
                {
                    var requestBody = new
                    {
                        query = query,
                        user_id = userId.ToString(),
                        job_context = jobContext,
                        top_k = topK
                    };

                    var json = JsonSerializer.Serialize(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(HttpMethod.Post, "answer");
                    request.Headers.Add("X-API-Key", _apiKey);
                    request.Content = content;

                    return await _httpClient.SendAsync(request);
                });

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("NodeRAG answer generation failed with status {StatusCode}: {Error}", 
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"NodeRAG returned status {response.StatusCode}: {errorContent}", null, response.StatusCode);
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<NodeRAGAnswerResponseDto>(options);
                
                if (result == null)
                {
                    throw new InvalidOperationException("NodeRAG returned null response");
                }

                _logger.LogInformation("Answer generated successfully: UserId={UserId}, ProcessingTime={ProcessingTime}ms", 
                    userId, result.ProcessingTimeMs);

                return result;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout generating answer in NodeRAG for UserId={UserId}", userId);
                throw new TimeoutException("NodeRAG request timed out", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating answer in NodeRAG for UserId={UserId}", userId);
                throw;
            }
        }

        public async Task<NodeRAGQAPairResponseDto> CreateQAPairAsync(NodeRAGQAPairCreateDto qaPair)
        {
            try
            {
                _logger.LogInformation("Creating Q&A pair in NodeRAG: UserId={UserId}, QuestionId={QuestionId}", 
                    qaPair.UserId, qaPair.QuestionId);

                var response = await ExecuteWithRetryAsync(async () =>
                {
                    var requestBody = new
                    {
                        question = qaPair.Question,
                        answer = qaPair.Answer,
                        question_id = qaPair.QuestionId,
                        user_id = qaPair.UserId,
                        job_title = qaPair.JobTitle,
                        company_name = qaPair.CompanyName,
                        submission_date = qaPair.SubmissionDate?.ToString("o")
                    };

                    var json = JsonSerializer.Serialize(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(HttpMethod.Post, "qa-pairs");
                    request.Headers.Add("X-API-Key", _apiKey);
                    request.Content = content;

                    return await _httpClient.SendAsync(request);
                });

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("NodeRAG Q&A pair creation failed with status {StatusCode}: {Error}", 
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"NodeRAG returned status {response.StatusCode}: {errorContent}");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<NodeRAGQAPairResponseDto>(options);
                
                if (result == null)
                {
                    throw new InvalidOperationException("NodeRAG returned null response");
                }

                _logger.LogInformation("Q&A pair created successfully: UserId={UserId}, AddedToGraph={AddedToGraph}", 
                    qaPair.UserId, result.AddedToGraph);

                return result;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout creating Q&A pair in NodeRAG for UserId={UserId}", qaPair.UserId);
                throw new TimeoutException("NodeRAG request timed out", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Q&A pair in NodeRAG for UserId={UserId}", qaPair.UserId);
                throw;
            }
        }

        public async Task<NodeRAGGraphStatsDto> GetGraphStatsAsync(int userId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"graph/stats?user_id={userId}");
                request.Headers.Add("X-API-Key", _apiKey);

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("NodeRAG graph stats request failed with status {StatusCode}: {Error}", 
                        response.StatusCode, errorContent);
                    throw new HttpRequestException($"NodeRAG returned status {response.StatusCode}: {errorContent}");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<NodeRAGGraphStatsDto>(options);
                
                if (result == null)
                {
                    throw new InvalidOperationException("NodeRAG returned null response");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting graph stats from NodeRAG for UserId={UserId}", userId);
                throw;
            }
        }

        public async Task<NodeRAGHealthDto> HealthCheckAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("health");

                if (!response.IsSuccessStatusCode)
                {
                    return new NodeRAGHealthDto
                    {
                        Success = false,
                        Message = $"NodeRAG health check failed with status {response.StatusCode}"
                    };
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<NodeRAGHealthDto>(options);
                return result ?? new NodeRAGHealthDto
                {
                    Success = false,
                    Message = "NodeRAG returned null response"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking NodeRAG health");
                return new NodeRAGHealthDto
                {
                    Success = false,
                    Message = $"Health check failed: {ex.Message}"
                };
            }
        }

        private async Task<HttpResponseMessage> ExecuteWithRetryAsync(Func<Task<HttpResponseMessage>> action)
        {
            int retryCount = 0;
            Exception? lastException = null;
            
            while (retryCount <= _maxRetries)
            {
                try
                {
                    return await action();
                }
                catch (HttpRequestException ex) when (retryCount < _maxRetries)
                {
                    // Retry on specific status codes or network errors
                    bool shouldRetry = ex.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable ||
                                      ex.StatusCode == System.Net.HttpStatusCode.InternalServerError ||
                                      ex.StatusCode == null; // Network errors have null status code
                    
                    if (shouldRetry)
                    {
                        lastException = ex;
                        retryCount++;
                        var delaySeconds = Math.Pow(2, retryCount);
                        _logger.LogWarning(ex, "HTTP request failed, retrying in {Delay}s (Attempt {Attempt}/{Max})", 
                            delaySeconds, retryCount, _maxRetries);
                        await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                    }
                    else
                    {
                        throw; // Don't retry on other status codes
                    }
                }
                catch (TaskCanceledException) when (retryCount < _maxRetries)
                {
                    // Retry on timeout
                    lastException = new TimeoutException("Request timed out");
                    retryCount++;
                    var delaySeconds = Math.Pow(2, retryCount);
                    _logger.LogWarning("Request timed out, retrying in {Delay}s (Attempt {Attempt}/{Max})", 
                        delaySeconds, retryCount, _maxRetries);
                    await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                }
            }
            
            // If we get here, all retries failed
            throw lastException ?? new HttpRequestException("Request failed after all retries");
        }
    }
}

