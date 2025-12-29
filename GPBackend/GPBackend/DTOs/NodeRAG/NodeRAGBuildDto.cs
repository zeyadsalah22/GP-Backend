namespace GPBackend.DTOs.NodeRAG
{
    public class NodeRAGBuildRequestDto
    {
        public string UserId { get; set; } = null!;
        public bool Incremental { get; set; } = true;
        public bool SyncToNeo4j { get; set; } = true;
        public bool ForceRebuild { get; set; } = false;
    }

    public class NodeRAGBuildResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public string BuildId { get; set; } = null!;
        public string Status { get; set; } = null!;
        public double? DurationSeconds { get; set; }
        public int? NodesCreated { get; set; }
        public int? EdgesCreated { get; set; }
        public bool? Neo4jSynced { get; set; }
    }

    public class NodeRAGBuildStatusDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? CurrentStage { get; set; }
        public List<string>? StagesCompleted { get; set; }
        public string? ErrorDetails { get; set; }
    }
}

