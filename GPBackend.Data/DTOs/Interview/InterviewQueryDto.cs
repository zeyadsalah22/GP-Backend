using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Interview
{
    public class InterviewQueryDto
    {
        // search and filters
        public int? CompanyId { get; set; }
        public int? ApplicationId { get; set; }
        public string? Position { get; set; }
        public string? JobDescription { get; set; }

        public DateTime? StartDate { get; set; }
        
        public string? SearchTerm { get; set; }


        // pagination
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0.")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Maximum page size is 100.")]
        public int PageSize { get; set; } = 10;

        // sorting
        public string? SortBy { get; set; } = "createdAt";
        public bool SortDescending { get; set; } = true;
    }
}

