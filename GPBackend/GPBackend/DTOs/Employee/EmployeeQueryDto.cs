using GPBackend.DTOs.Common;

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
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
} 
