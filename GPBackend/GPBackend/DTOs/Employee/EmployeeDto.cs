using System;
using GPBackend.DTOs.Company;

namespace GPBackend.DTOs.Employee
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public CompanyResponseDto? Company { get; set; }
        public string? Name { get; set; }
        public string? LinkedinLink { get; set; }
        public string? Email { get; set; }
        public string? JobTitle { get; set; }
        public string? Contacted { get; set; }
        public string? Phone { get; set; }
        public string? Department { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
} 
