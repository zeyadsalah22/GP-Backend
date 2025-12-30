using System.ComponentModel.DataAnnotations;

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
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public string Query { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public NodeRAGSearchResultsDto? SearchResults { get; set; }
        public int? ProcessingTimeMs { get; set; }
    }

    public class NodeRAGSearchResultsDto
    {
        public List<NodeRAGNodeDto> Nodes { get; set; } = new();
        public List<NodeRAGQAPairResultDto> QaPairs { get; set; } = new();
        public int TotalCount { get; set; }
    }

    public class NodeRAGNodeDto
    {
        public string HashId { get; set; } = null!;
        public string NodeType { get; set; } = null!;
        public string Text { get; set; } = null!;
        public double Weight { get; set; }
    }

    public class NodeRAGQAPairResultDto
    {
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public double Similarity { get; set; }
    }
}

