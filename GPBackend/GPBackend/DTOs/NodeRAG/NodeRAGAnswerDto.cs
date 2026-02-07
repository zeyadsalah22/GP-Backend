using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GPBackend.DTOs.NodeRAG
{
    public class NodeRAGAnswerRequestDto
    {
        [Required(ErrorMessage = "Query is required")]
        public string Query { get; set; } = null!;
        
        public string? JobContext { get; set; }
        
        [Range(1, 50, ErrorMessage = "TopK must be between 1 and 50")]
        public int TopK { get; set; } = 10;
    }

    public class NodeRAGAnswerResponseDto
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        [JsonPropertyName("query")]
        public string Query { get; set; } = null!;

        [JsonPropertyName("answer")]
        public string Answer { get; set; } = null!;

        [JsonPropertyName("search_results")]
        public NodeRAGSearchResultsDto? SearchResults { get; set; }

        [JsonPropertyName("processing_time_ms")]
        public int? ProcessingTimeMs { get; set; }
    }

    public class NodeRAGSearchResultsDto
    {
        [JsonPropertyName("nodes")]
        public List<NodeRAGNodeDto> Nodes { get; set; } = new();

        [JsonPropertyName("qa_pairs")]
        public List<NodeRAGQAPairResultDto> QaPairs { get; set; } = new();

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
    }

    public class NodeRAGNodeDto
    {
        [JsonPropertyName("hash_id")]
        public string HashId { get; set; } = null!;

        [JsonPropertyName("node_type")]
        public string NodeType { get; set; } = null!;

        [JsonPropertyName("text")]
        public string Text { get; set; } = null!;

        [JsonPropertyName("weight")]
        public double Weight { get; set; }
    }

    public class NodeRAGQAPairResultDto
    {
        [JsonPropertyName("question")]
        public string Question { get; set; } = null!;

        [JsonPropertyName("answer")]
        public string Answer { get; set; } = null!;

        [JsonPropertyName("similarity")]
        public double Similarity { get; set; }
    }
}

