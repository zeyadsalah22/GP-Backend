using System;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.CompanyRequest
{
    public class CompanyRequestQueryDto
    {
        // Filtering
        public int? UserId { get; set; }
        
        public CompanyRequestStatus? RequestStatus { get; set; }
        
        public string? CompanyName { get; set; }
        
        public int? IndustryId { get; set; }
        
        public DateTime? FromDate { get; set; }
        
        public DateTime? ToDate { get; set; }
        
        public string? SearchTerm { get; set; }

        // Sorting
        public string? SortBy { get; set; } = "RequestedAt";
        
        public bool SortDescending { get; set; } = true;

        // Pagination
        public int PageNumber { get; set; } = 1;
        
        public int PageSize { get; set; } = 20;
    }
}

