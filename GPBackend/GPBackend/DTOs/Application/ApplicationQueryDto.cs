using GPBackend.DTOs.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Application
{
    public class ApplicationQueryDto
    {
        // Search and filters
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? JobTitle { get; set; }
        public string? JobType { get; set; }
        public string? Stage { get; set; }
        public string? Status { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? SearchTerm { get; set; }
        
        // Pagination parameters
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Maximum page size is 100")]
        public int PageSize { get; set; } = 10;

        // Sorting
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
    }
} 