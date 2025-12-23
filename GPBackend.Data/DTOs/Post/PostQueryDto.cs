using GPBackend.DTOs.Common;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Post
{
    public class PostQueryDto : PaginationQueryDto
    {
        public string? SearchTerm { get; set; }

        public PostType? PostType { get; set; }

        public PostStatus? Status { get; set; }

        public int? UserId { get; set; }

        public List<string>? Tags { get; set; }

        public bool? IsAnonymous { get; set; }

        public DateTime? CreatedAfter { get; set; }

        public DateTime? CreatedBefore { get; set; }

        public string? SortBy { get; set; } = "CreatedAt";

        public bool SortDescending { get; set; } = true;
    }
}

