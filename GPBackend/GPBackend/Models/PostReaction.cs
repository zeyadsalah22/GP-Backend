using System;
using GPBackend.Models.Enums;

namespace GPBackend.Models;

public partial class PostReaction
{
    public int PostReactionId { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }

    public ReactionType ReactionType { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

