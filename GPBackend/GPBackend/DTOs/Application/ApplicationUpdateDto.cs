using System;
using System.ComponentModel.DataAnnotations;
using GPBackend.Models.Enums;

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
        
        [Range(0, 100, ErrorMessage = "ATS score must be between 0 and 100")]
        public int? AtsScore { get; set; }
        
        public ApplicationStage? Stage { get; set; }
        
        public ApplicationDecisionStatus? Status { get; set; }
        
        public DateOnly? SubmissionDate { get; set; }
        
        public List<int>? ContactedEmployeeIds { get; set; }
    }
} 