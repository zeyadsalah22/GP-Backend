using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.UserCompany
{
    public class UserCompanyQueryDto
    {
        // Search parameters
        public string? SearchTerm { get; set; }
        
        // Filter parameters
        public int? UserId { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public GPBackend.Models.Enums.InterestLevel? InterestLevel { get; set; }
        public bool? Favorite { get; set; }
        public List<string>? Tags { get; set; }
        
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