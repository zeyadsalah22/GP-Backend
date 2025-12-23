using System;
using GPBackend.Models.Enums;

namespace GPBackend.Models;

public partial class CommentReaction
{
    public int CommentReactionId { get; set; }

    public int CommentId { get; set; }

    public int UserId { get; set; }

    public CommentReactionType ReactionType { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public virtual Comment Comment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

