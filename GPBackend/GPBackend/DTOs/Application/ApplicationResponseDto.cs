using System;
using System.Collections.Generic;
using GPBackend.DTOs.Company;
using GPBackend.DTOs.Employee;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Application
{
    public class ApplicationResponseDto
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public CompanyResponseDto? Company { get; set; }
        public string JobTitle { get; set; } = null!;
        public string JobType { get; set; } = null!;
        public string? Description { get; set; }
        public string? Link { get; set; }
        public int? SubmittedCvId { get; set; }
        public int? AtsScore { get; set; }
        public ApplicationStage Stage { get; set; }
        public ApplicationDecisionStatus Status { get; set; }
        public DateOnly SubmissionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<EmployeeDto> ContactedEmployees { get; set; } = new List<EmployeeDto>();
        public List<ApplicationStageHistoryDto> Timeline { get; set; } = new List<ApplicationStageHistoryDto>();
    }
} 