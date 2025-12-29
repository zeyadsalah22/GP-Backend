using GPBackend.BackgoundServices;
using GPBackend.DTOs.NodeRAG;
using GPBackend.Models;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class NodeRAGService : INodeRAGService
    {
        private readonly INodeRAGClient _nodeRAGClient;
        private readonly NodeRAGBackgroundService _backgroundService;
        private readonly ILogger<NodeRAGService> _logger;
        
        public NodeRAGService(
            INodeRAGClient nodeRAGClient, 
            NodeRAGBackgroundService backgroundService,
            ILogger<NodeRAGService> logger)
        {
            _nodeRAGClient = nodeRAGClient;
            _backgroundService = backgroundService;
            _logger = logger;
        }
        
        public async Task<NodeRAGAnswerResponseDto> GenerateAnswerAsync(
            int userId, string query, string? jobContext = null, int topK = 10)
        {
            try
            {
                _logger.LogInformation("Generating answer for UserId={UserId}, Query={Query}", userId, query);
                return await _nodeRAGClient.GenerateAnswerAsync(userId, query, jobContext, topK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating answer for UserId={UserId}", userId);
                throw;
            }
        }
        
        public async Task TriggerGraphBuildAsync(int userId)
        {
            var job = new NodeRAGBackgroundJob
            {
                JobType = NodeRAGJobType.BuildGraph,
                UserId = userId
            };
            
            await _backgroundService.QueueJobAsync(job);
            _logger.LogInformation("Graph build job queued for UserId={UserId}", userId);
        }
        
        public async Task SyncQuestionAnswerAsync(
            int userId, int questionId, string question, string answer, 
            string? jobTitle, string? companyName)
        {
            var qaPair = new NodeRAGQAPairCreateDto
            {
                Question = question,
                Answer = answer,
                QuestionId = $"q_{questionId}",
                UserId = userId.ToString(),
                JobTitle = jobTitle,
                CompanyName = companyName,
                SubmissionDate = DateTime.UtcNow
            };
            
            var job = new NodeRAGBackgroundJob
            {
                JobType = NodeRAGJobType.SyncQAPair,
                UserId = userId,
                Metadata = new Dictionary<string, object> { ["QAPair"] = qaPair }
            };
            
            await _backgroundService.QueueJobAsync(job);
            _logger.LogInformation("Q&A sync job queued for UserId={UserId}, QuestionId={QuestionId}", userId, questionId);
        }
        
        public async Task<NodeRAGGraphStatsDto> GetUserGraphStatsAsync(int userId)
        {
            try
            {
                return await _nodeRAGClient.GetGraphStatsAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting graph stats for UserId={UserId}", userId);
                throw;
            }
        }
    }
}

