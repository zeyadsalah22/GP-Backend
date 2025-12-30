namespace GPBackend.DTOs.NodeRAG
{
    public class NodeRAGGraphStatsDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public int TotalNodes { get; set; }
        public int TotalEdges { get; set; }
        public Dictionary<string, int> NodeTypeDistribution { get; set; } = new();
        public int DocumentsCount { get; set; }
        public int QaPairsCount { get; set; }
    }

    public class NodeRAGHealthDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public NodeRAGHealthStatusDto? Status { get; set; }
    }

    public class NodeRAGHealthStatusDto
    {
        public string ApiStatus { get; set; } = null!;
        public bool Neo4jConnected { get; set; }
        public bool GraphLoaded { get; set; }
        public bool SearchReady { get; set; }
        public int? TotalNodes { get; set; }
        public int? TotalRelationships { get; set; }
    }
}

