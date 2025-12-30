namespace GPBackend.DTOs.NodeRAG
{
    public class NodeRAGDocumentUploadResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public NodeRAGDocumentDto? Document { get; set; }
        public bool RequiresRebuild { get; set; }
    }

    public class NodeRAGDocumentDto
    {
        public string Filename { get; set; } = null!;
        public string Path { get; set; } = null!;
        public long SizeBytes { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DocumentType { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}

