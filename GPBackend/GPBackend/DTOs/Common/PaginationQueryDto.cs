using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Common
{
    public class PaginationQueryDto
    {
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
