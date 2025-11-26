using System;

namespace GPBackend.Models;

public partial class CommentMention
{
    public int Id { get; set; }

    public int CommentId { get; set; }

    public int MentionedUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual User MentionedUser { get; set; } = null!;
}

