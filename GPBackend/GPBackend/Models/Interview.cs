using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class Interview
{
    public int InterviewId { get; set; }

    public int ApplicationId { get; set; }

    public int CompanyId { get; set; }

    public int UserId { get; set; }

    public string? Position { get; set; }
    public string? JobDescription { get; set; }

    public string? Feedback { get; set; }

    public DateTime StartDate { get; set; }

    public int Duration { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<InterviewQuestion> InterviewQuestions { get; set; } = new List<InterviewQuestion>();

    public virtual User User { get; set; } = null!;
}
