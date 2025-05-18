using System;
using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Application
{
    public class ApplicationCreateDto
    {
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Job title is required")]
        [StringLength(100, ErrorMessage = "Job title cannot exceed 100 characters")]
        public string JobTitle { get; set; } = null!;
        
        [Required(ErrorMessage = "Job type is required")]
        [StringLength(50, ErrorMessage = "Job type cannot exceed 50 characters")]
        public string JobType { get; set; } = null!;
        
        public string? Description { get; set; }
        
        [Url(ErrorMessage = "Please provide a valid URL")]
        [StringLength(255, ErrorMessage = "Link cannot exceed 255 characters")]
        public string? Link { get; set; }
        
        public int? SubmittedCvId { get; set; }
        
        [Required(ErrorMessage = "Stage is required")]
        [StringLength(50, ErrorMessage = "Stage cannot exceed 50 characters")]
        public string Stage { get; set; } = null!;
        
        [Required(ErrorMessage = "Status is required")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string Status { get; set; } = null!;
        
        public DateOnly? SubmissionDate { get; set; }
    }
} 