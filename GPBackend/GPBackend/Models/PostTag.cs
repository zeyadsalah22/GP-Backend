using System;

namespace GPBackend.Models;

public partial class PostTag
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public int TagId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}

