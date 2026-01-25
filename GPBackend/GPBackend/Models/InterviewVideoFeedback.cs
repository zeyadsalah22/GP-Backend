using System;

namespace GPBackend.Models;

public partial class InterviewVideoFeedback
{
    public int Id { get; set; }

    public int InterviewId { get; set; }

    public double Confidence { get; set; }
    public double Engagement { get; set; }
    public double Stress { get; set; }
    public double Authenticity { get; set; }

    public string? Summary { get; set; }

    // JSON arrays of objects (as returned by FastAPI)
    public string? StrengthsJson { get; set; }
    public string? ImprovementsJson { get; set; }
    public string? RecommendationsJson { get; set; }

    public string? ReportPath { get; set; }

    // JSON object of the FastAPI response (for forward compatibility/debugging)
    public string? RawResponseJson { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Interview Interview { get; set; } = null!;
}


