using System;

namespace GPBackend.Models;

public partial class SavedPost
{
    public int SavedPostId { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public DateTime SavedAt { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}

