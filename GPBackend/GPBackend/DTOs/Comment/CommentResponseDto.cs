namespace GPBackend.DTOs.Comment
{
    public class CommentResponseDto
    {
        public int CommentId { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public string AuthorName { get; set; } = null!;

        public int? ParentCommentId { get; set; }

        public string? ParentAuthorName { get; set; } 

        public string? ParentContentPreview { get; set; } // First 100 chars of parent, for replies

        public string Content { get; set; } = null!;

        public int Level { get; set; }

        public int ReplyCount { get; set; }

        public bool IsEdited { get; set; }

        public DateTime? LastEditedAt { get; set; }

        public string? EditedTimeAgo { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public string TimeAgo { get; set; } = null!;

        public DateTime UpdatedAt { get; set; }

        // public byte[] Rowversion { get; set; } = null!;

        public List<CommentMentionDto> Mentions { get; set; } = new List<CommentMentionDto>();

        public List<CommentResponseDto> Replies { get; set; } = new List<CommentResponseDto>();
    }

    public class CommentMentionDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
    }
}

