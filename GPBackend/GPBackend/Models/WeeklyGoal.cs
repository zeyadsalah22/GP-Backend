using System;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.Models;

public partial class WeeklyGoal
{
    public int WeeklyGoalId { get; set; }

    public int UserId { get; set; }

    public DateOnly WeekStartDate { get; set; }

    public DateOnly WeekEndDate { get; set; }

    public int TargetApplicationCount { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    // Navigation property
    public virtual User User { get; set; } = null!;
}

