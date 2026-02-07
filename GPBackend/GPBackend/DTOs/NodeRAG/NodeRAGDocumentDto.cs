using System.Text.Json.Serialization;

namespace GPBackend.DTOs.NodeRAG
{
    public class NodeRAGDocumentUploadResponseDto
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        [JsonPropertyName("document")]
        public NodeRAGDocumentDto? Document { get; set; }

        [JsonPropertyName("requires_rebuild")]
        public bool RequiresRebuild { get; set; }
    }

    public class NodeRAGDocumentDto
    {
        [JsonPropertyName("filename")]
        public string Filename { get; set; } = null!;

        [JsonPropertyName("path")]
        public string Path { get; set; } = null!;

        [JsonPropertyName("size_bytes")]
        public long SizeBytes { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("document_type")]
        public string DocumentType { get; set; } = null!;

        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = null!;
    }
}

