using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GPBackend.Models.Enums;

namespace GPBackend.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public int UserId { get; set; }

    public int CompanyId { get; set; }

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

    public bool IsDeleted { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public virtual ICollection<ApplicationEmployee> ApplicationEmployees { get; set; } = new List<ApplicationEmployee>();

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<ApplicationStageHistory> StageHistory { get; set; } = new List<ApplicationStageHistory>();

    public virtual Resume? SubmittedCv { get; set; }

    public virtual UserCompany UserCompany { get; set; } = null!;

}
