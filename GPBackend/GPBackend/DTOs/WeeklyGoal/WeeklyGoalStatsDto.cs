namespace GPBackend.DTOs.WeeklyGoal
{
    public class WeeklyGoalStatsDto
    {
        public int TotalGoals { get; set; }
        public int CompletedGoals { get; set; }
        public int ActiveGoals { get; set; }
        public double AverageCompletionRate { get; set; }
        public int CurrentWeekTarget { get; set; }
        public int CurrentWeekActual { get; set; }
        public double CurrentWeekProgress { get; set; }
    }
}

