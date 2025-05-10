using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class TodoList
{
    public int TodoId { get; set; }

    public int UserId { get; set; }

    public string ApplicationTitle { get; set; } = null!;

    public string? ApplicationLink { get; set; }

    public DateTime? Deadline { get; set; }

    public bool Completed { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual User User { get; set; } = null!;
}
