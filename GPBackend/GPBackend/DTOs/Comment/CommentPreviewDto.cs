namespace GPBackend.DTOs.Comment
{
    public class CommentPreviewDto
    {
        public int CommentId { get; set; }

        public string AuthorName { get; set; } = null!;

        public string ContentSnippet { get; set; } = null!; // First 100 characters 

        public string TimeAgo { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}

