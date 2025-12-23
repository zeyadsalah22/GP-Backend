using System;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.WeeklyGoal
{
    public class WeeklyGoalQueryDto
    {
        // Filter by date range
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        
        // Filter by completion status
        public bool? IsCompleted { get; set; }
        
        // Filter by progress
        public int? MinProgress { get; set; } // 0-100
        public int? MaxProgress { get; set; } // 0-100

        // Pagination
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Maximum page size is 100")]
        public int PageSize { get; set; } = 10;

        // Sorting
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = true;
    }
}

