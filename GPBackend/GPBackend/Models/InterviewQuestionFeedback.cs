using System;

namespace GPBackend.Models;

public partial class InterviewQuestionFeedback
{
    public int Id { get; set; }

    public int InterviewQuestionId { get; set; }

    public double Score { get; set; }

    public string Feedback { get; set; } = string.Empty;

    // JSON array of strings
    public string? StrengthsJson { get; set; }

    // JSON array of strings
    public string? ImprovementsJson { get; set; }

    public string? Context { get; set; }

    // JSON object of the FastAPI response (for forward compatibility/debugging)
    public string? RawResponseJson { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual InterviewQuestion InterviewQuestion { get; set; } = null!;
}


