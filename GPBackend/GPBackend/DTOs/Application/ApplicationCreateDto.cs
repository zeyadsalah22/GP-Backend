using System;
using System.ComponentModel.DataAnnotations;
using GPBackend.Models.Enums;

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

        [Range(0, 100, ErrorMessage = "ATS score must be between 0 and 100")]
        public int? AtsScore { get; set; }

        [Required(ErrorMessage = "Stage is required")]
        public ApplicationStage Stage { get; set; }
        
        [Required(ErrorMessage = "Status is required")]
        public ApplicationDecisionStatus Status { get; set; }

        [PastOrTodayDateOnly(ErrorMessage = "Submission date cannot be in the future")]
        public DateOnly? SubmissionDate { get; set; }
        
        public List<int>? ContactedEmployeeIds { get; set; }
    }

    public class PastOrTodayDateOnlyAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null) return true; // Allow nulls for optional fields

            if (value is DateOnly date)
            {
                return date <= DateOnly.FromDateTime(DateTime.Today);
            }
            return false;
        }
    }
} 