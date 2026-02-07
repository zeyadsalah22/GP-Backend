using System.Text.Json.Serialization;

namespace GPBackend.DTOs.NodeRAG
{
    public class NodeRAGGraphStatsDto
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        [JsonPropertyName("total_nodes")]
        public int TotalNodes { get; set; }

        [JsonPropertyName("total_edges")]
        public int TotalEdges { get; set; }

        [JsonPropertyName("node_type_distribution")]
        public Dictionary<string, int> NodeTypeDistribution { get; set; } = new();

        [JsonPropertyName("documents_count")]
        public int DocumentsCount { get; set; }

        [JsonPropertyName("qa_pairs_count")]
        public int QaPairsCount { get; set; }
    }

    public class NodeRAGHealthDto
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        [JsonPropertyName("status")]
        public NodeRAGHealthStatusDto? Status { get; set; }
    }

    public class NodeRAGHealthStatusDto
    {
        [JsonPropertyName("api_status")]
        public string ApiStatus { get; set; } = null!;

        [JsonPropertyName("neo4j_connected")]
        public bool Neo4jConnected { get; set; }

        [JsonPropertyName("graph_loaded")]
        public bool GraphLoaded { get; set; }

        [JsonPropertyName("search_ready")]
        public bool SearchReady { get; set; }

        [JsonPropertyName("total_nodes")]
        public int? TotalNodes { get; set; }

        [JsonPropertyName("total_relationships")]
        public int? TotalRelationships { get; set; }
    }
}

