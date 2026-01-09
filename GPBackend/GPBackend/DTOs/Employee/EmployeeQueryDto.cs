using GPBackend.DTOs.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Employee
{
    public class EmployeeQueryDto
    {
        public string? Search { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? JobTitle { get; set; }
        public string? Department { get; set; }
        public string? ContactStatus { get; set; }

        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;

        // Pagination parameters
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 500, ErrorMessage = "Maximum page size is 500")]
        public int PageSize { get; set; } = 10;

    }
} 
