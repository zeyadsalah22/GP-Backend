using System;

namespace GPBackend.Models;

public partial class CommentEditHistory
{
    public int Id { get; set; }

    public int CommentId { get; set; }

    public string PreviousContent { get; set; } = null!;

    public DateTime EditedAt { get; set; }

    public virtual Comment Comment { get; set; } = null!;
}

