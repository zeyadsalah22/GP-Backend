using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Comment
{
    public class CommentUpdateDto
    {
        [Required]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Comment must be between 1 and 2000 characters")]
        public string Content { get; set; } = null!;
        public List<int> MentionedUserIds { get; set; } = new List<int>();

        // public byte[] Rowversion { get; set; } = null!;
    }
}

