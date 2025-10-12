using System;

namespace GPBackend.DTOs.WeeklyGoal
{
    public class WeeklyGoalResponseDto
    {
        public int WeeklyGoalId { get; set; }
        public int UserId { get; set; }
        public DateOnly WeekStartDate { get; set; }
        public DateOnly WeekEndDate { get; set; }
        public int TargetApplicationCount { get; set; }
        public int ActualApplicationCount { get; set; }
        public double ProgressPercentage { get; set; }
        public bool IsCompleted { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

