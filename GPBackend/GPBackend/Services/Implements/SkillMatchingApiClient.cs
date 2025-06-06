using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GPBackend.DTOs.ResumeMatching;
using GPBackend.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GPBackend.Services.Implements
{
    public class SkillMatchingApiClient : ISkillMatchingApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _modelEndpoint;

        public SkillMatchingApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _modelEndpoint = configuration["AIModel:SkillMatchingEndpoint"] 
                ?? throw new ArgumentException("AI Model endpoint not configured");
        }

        public async Task<double> GetMatchingScoreAsync(string skills, string jobDescription)
        {
            try
            {
                var request = new SkillMatchingRequestDto
                {
                    Skills = skills,
                    JobDescription = jobDescription
                };

                var response = await _httpClient.PostAsJsonAsync(_modelEndpoint, request);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<SkillMatchingResponseDto>();
                
                if (result == null)
                {
                    throw new Exception("Received null response from AI model");
                }

                return result.Score;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to communicate with AI model service", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error processing AI model request", ex);
            }
        }
    }
} 