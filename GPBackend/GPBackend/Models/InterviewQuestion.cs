using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class InterviewQuestion
{
    public int QuestionId { get; set; }

    public int InterviewId { get; set; }

    public string Question { get; set; } = null!;

    public string? Answer { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Interview Interview { get; set; } = null!;
}
