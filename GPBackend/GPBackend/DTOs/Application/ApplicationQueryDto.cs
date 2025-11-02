using GPBackend.DTOs.Common;
using GPBackend.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Application
{
    public class ApplicationQueryDto
    {
        // User identification (required for n8n API key authentication)
        public int? UserId { get; set; }
        
        // Search and filters
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? JobTitle { get; set; }
        public string? JobType { get; set; }
        public ApplicationStage? Stage { get; set; }
        public ApplicationDecisionStatus? Status { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? SearchTerm { get; set; }
        
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