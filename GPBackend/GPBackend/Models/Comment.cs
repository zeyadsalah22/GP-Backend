using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }

    public int? ParentCommentId { get; set; }

    public string Content { get; set; } = null!;

    public int Level { get; set; } // 0 for top-level comments, 1 for replies

    public int ReplyCount { get; set; }

    public bool IsEdited { get; set; }

    public DateTime? LastEditedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual Comment? ParentComment { get; set; }

    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();

    public virtual ICollection<CommentEditHistory> EditHistory { get; set; } = new List<CommentEditHistory>();

    public virtual ICollection<CommentMention> Mentions { get; set; } = new List<CommentMention>();

    public virtual ICollection<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();
}

