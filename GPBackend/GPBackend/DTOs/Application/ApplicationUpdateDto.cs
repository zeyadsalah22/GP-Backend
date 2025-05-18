using System;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Application
{
    public class ApplicationUpdateDto
    {
        [StringLength(100, ErrorMessage = "Job title cannot exceed 100 characters")]
        public string? JobTitle { get; set; }
        
        [StringLength(50, ErrorMessage = "Job type cannot exceed 50 characters")]
        public string? JobType { get; set; }
        
        public string? Description { get; set; }
        
        [Url(ErrorMessage = "Please provide a valid URL")]
        [StringLength(255, ErrorMessage = "Link cannot exceed 255 characters")]
        public string? Link { get; set; }
        
        public int? SubmittedCvId { get; set; }
        
        public int? AtsScore { get; set; }
        
        [StringLength(50, ErrorMessage = "Stage cannot exceed 50 characters")]
        public string? Stage { get; set; }
        
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string? Status { get; set; }
        
        public DateOnly? SubmissionDate { get; set; }
    }
} 