using GPBackend.DTOs.Common;

namespace GPBackend.DTOs.Employee
{
    public class EmployeeQueryDto : PaginationQueryDto
    {
        public string? Search { get; set; }
        public int? CompanyId { get; set; }
    }
} 
