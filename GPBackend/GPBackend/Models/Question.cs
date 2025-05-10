using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public string Question1 { get; set; } = null!;

    public string? Answer { get; set; }

    public int ApplicationId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Application Application { get; set; } = null!;
}
