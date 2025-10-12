using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.WeeklyGoal
{
    public class WeeklyGoalUpdateDto
    {
        [Range(1, 100, ErrorMessage = "Target must be between 1 and 100")]
        public int? TargetApplicationCount { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }
}

