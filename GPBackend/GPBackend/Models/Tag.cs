using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}

