using GPBackend.DTOs.NodeRAG;

namespace GPBackend.Services.Interfaces
{
    public interface INodeRAGService
    {
        Task<NodeRAGAnswerResponseDto> GenerateAnswerAsync(int userId, string query, string? jobContext = null, int topK = 10);
        Task<NodeRAGGraphStatsDto> GetUserGraphStatsAsync(int userId);
        Task TriggerGraphBuildAsync(int userId);
        Task SyncQuestionAnswerAsync(int userId, int questionId, string question, string answer, string? jobTitle, string? companyName);
    }
}

