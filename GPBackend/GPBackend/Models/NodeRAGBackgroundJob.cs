namespace GPBackend.Models
{
    public enum NodeRAGJobType
    {
        BuildGraph,
        SyncQAPair
    }

    public class NodeRAGBackgroundJob
    {
        public Guid JobId { get; set; } = Guid.NewGuid();
        public NodeRAGJobType JobType { get; set; }
        public int UserId { get; set; }
        public Dictionary<string, object>? Metadata { get; set; }
        public DateTime QueuedAt { get; set; } = DateTime.UtcNow;
        public int RetryCount { get; set; } = 0;
        public int MaxRetries { get; set; } = 3;
    }
}

