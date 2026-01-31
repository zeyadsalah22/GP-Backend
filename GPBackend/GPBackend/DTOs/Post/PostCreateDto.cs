using System.ComponentModel.DataAnnotations;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Post
{
    public class PostCreateDto
    {
        [Required(ErrorMessage = "Post type is required")]
        public PostType PostType { get; set; }

        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [MinLength(10, ErrorMessage = "Content must be at least 10 characters")]
        [MaxLength(10000, ErrorMessage = "Content cannot exceed 10,000 characters")]
        public string Content { get; set; } = null!;

        public bool IsAnonymous { get; set; } = false;

        [Required(ErrorMessage = "Status is required")]
        public PostStatus Status { get; set; }

        [MaxLength(5, ErrorMessage = "Maximum 5 tags allowed")]
        public List<string>? Tags { get; set; }

        public List<int> MentionedUserIds { get; set; } = new List<int>();
    }
}

