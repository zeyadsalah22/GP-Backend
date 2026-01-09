using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Company
{
    public class CompanyQueryDto
    {
        // Search parameters
        public string? SearchTerm { get; set; }
            
        // Filter parameters
        public string? Location { get; set; }
        public string? Name { get; set; }
        public int? IndustryId { get; set; }
        //public GPBackend.Models.Enums.CompanySize? CompanySize { get; set; }
        public string? CompanySize { get; set; }

        // Pagination parameters
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 500, ErrorMessage = "Maximum page size is 500")]
        public int PageSize { get; set; } = 10;
        
        // Sorting
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
    }
} 