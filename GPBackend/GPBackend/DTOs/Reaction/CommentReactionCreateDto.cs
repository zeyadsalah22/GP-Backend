using System.ComponentModel.DataAnnotations;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Reaction
{
    public class CommentReactionCreateDto
    {
        [Required]
        public int CommentId { get; set; }

        [Required]
        public CommentReactionType ReactionType { get; set; }
    }
}

