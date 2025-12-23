namespace GPBackend.DTOs.Comment
{
    public class CommentQueryDto
    {
        public int PostId { get; set; }

        public int? Level { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; } = "CreatedAt";

        public string? SortOrder { get; set; } = "DESC";
    }
}

