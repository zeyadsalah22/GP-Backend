using System.ComponentModel.DataAnnotations;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Reaction
{
    public class PostReactionCreateDto
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        public ReactionType ReactionType { get; set; }
    }
}

