namespace GPBackend.DTOs.Reaction
{
    public class CommentReactionSummaryDto
    {
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public int Score { get; set; } // UpvoteCount - DownvoteCount
        public string? UserReaction { get; set; }
    }
}

