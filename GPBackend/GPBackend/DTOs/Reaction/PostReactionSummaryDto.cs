namespace GPBackend.DTOs.Reaction
{
    public class PostReactionSummaryDto
    {
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public int HelpfulCount { get; set; }
        public int InsightfulCount { get; set; }
        public int ThanksCount { get; set; }
        public int TotalReactions { get; set; }
        public string? UserReaction { get; set; }
    }
}

