using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Reaction
{
    public class PostReactionResponseDto
    {
        public int PostReactionId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public ReactionType ReactionType { get; set; }
        public string ReactionTypeName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

