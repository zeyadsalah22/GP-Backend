using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Post
{
    public class PostResponseDto
    {
        public int PostId { get; set; }

        public int? UserId { get; set; } // Null for anonymous posts in response

        public string? AuthorName { get; set; } // Null for anonymous posts

        public PostType PostType { get; set; }

        public string PostTypeName { get; set; } = null!;

        public string? Title { get; set; }

        public string Content { get; set; } = null!;

        public bool IsAnonymous { get; set; }

        public PostStatus Status { get; set; }

        public string StatusName { get; set; } = null!;

        public List<TagDto> Tags { get; set; } = new List<TagDto>();

        public DateTime CreatedAt { get; set; }

        public string RelativeTime { get; set; } = null!;

        public DateTime UpdatedAt { get; set; }

        public byte[] Rowversion { get; set; } = null!;
    }

    public class TagDto
    {
        public int TagId { get; set; }
        public string Name { get; set; } = null!;
    }
}

