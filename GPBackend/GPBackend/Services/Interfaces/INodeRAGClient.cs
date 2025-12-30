using GPBackend.DTOs.NodeRAG;

namespace GPBackend.Services.Interfaces
{
    public interface INodeRAGClient
    {
        // Documents
        Task<NodeRAGDocumentUploadResponseDto> UploadDocumentAsync(
            int userId, 
            byte[] fileContent, 
            string filename, 
            string documentType = "resume");
        
        // Build
        Task<NodeRAGBuildResponseDto> TriggerBuildAsync(
            int userId, 
            bool incremental = true, 
            bool syncToNeo4j = true, 
            bool forceRebuild = false);
        
        Task<NodeRAGBuildStatusDto> GetBuildStatusAsync(string buildId);
        
        // Answer Generation
        Task<NodeRAGAnswerResponseDto> GenerateAnswerAsync(
            int userId, 
            string query, 
            string? jobContext = null, 
            int topK = 10);
        
        // Q&A Pairs
        Task<NodeRAGQAPairResponseDto> CreateQAPairAsync(NodeRAGQAPairCreateDto qaPair);
        
        // Stats & Health
        Task<NodeRAGGraphStatsDto> GetGraphStatsAsync(int userId);
        Task<NodeRAGHealthDto> HealthCheckAsync();
    }
}

