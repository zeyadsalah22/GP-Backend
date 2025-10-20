using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Reaction
{
    public class CommentReactionResponseDto
    {
        public int CommentReactionId { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public CommentReactionType ReactionType { get; set; }
        public string ReactionTypeName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

