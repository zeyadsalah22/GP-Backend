using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.ResumeTest
{
    public class ResumeTestQueryDto
    {
        // search and filters
        public int? ResumeId { get; set; }
        public string? JobDescription { get; set; }
        public DateTime? TestDate { get; set; }
        public int? AtsScore { get; set; }
        public string? SearchTerm { get; set; }

        // pagination
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0.")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Maximum page size is 100.")]
        public int PageSize { get; set; } = 10;

        // sorting
        public string? SortBy { get; set; } = "testDate";
        // can sort by score
        public bool SortDescending { get; set; } = true;
    }
} 