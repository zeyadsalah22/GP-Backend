using System.Text.Json.Serialization;

namespace GPBackend.DTOs.NodeRAG
{
    public class NodeRAGBuildRequestDto
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = null!;
        
        [JsonPropertyName("incremental")]
        public bool Incremental { get; set; } = true;
        
        [JsonPropertyName("sync_to_neo4j")]
        public bool SyncToNeo4j { get; set; } = true;
        
        [JsonPropertyName("force_rebuild")]
        public bool ForceRebuild { get; set; } = false;
    }

    public class NodeRAGBuildResponseDto
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        // NodeRAG responses use snake_case (build_id). Keep nullable to handle older deployments.
        [JsonPropertyName("build_id")]
        public string? BuildId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = null!;

        [JsonPropertyName("duration_seconds")]
        public double? DurationSeconds { get; set; }

        [JsonPropertyName("nodes_created")]
        public int? NodesCreated { get; set; }

        [JsonPropertyName("edges_created")]
        public int? EdgesCreated { get; set; }

        [JsonPropertyName("neo4j_synced")]
        public bool? Neo4jSynced { get; set; }
    }

    public class NodeRAGBuildStatusDto
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        [JsonPropertyName("status")]
        public string Status { get; set; } = null!;

        [JsonPropertyName("current_stage")]
        public string? CurrentStage { get; set; }

        [JsonPropertyName("stages_completed")]
        public List<string>? StagesCompleted { get; set; }

        [JsonPropertyName("error_details")]
        public string? ErrorDetails { get; set; }
    }
}

