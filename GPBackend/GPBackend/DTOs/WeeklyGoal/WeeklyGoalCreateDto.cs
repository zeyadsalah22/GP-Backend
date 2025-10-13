using System;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.WeeklyGoal
{
    public class WeeklyGoalCreateDto
    {
        [Required(ErrorMessage = "Week start date is required")]
        public DateOnly WeekStartDate { get; set; }

        [Required(ErrorMessage = "Target application count is required")]
        [Range(1, 100, ErrorMessage = "Target must be between 1 and 100")]
        public int TargetApplicationCount { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }
}

