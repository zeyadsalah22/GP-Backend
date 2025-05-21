using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

    public string Stage { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateOnly SubmissionDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public virtual ICollection<ApplicationEmployee> ApplicationEmployees { get; set; } = new List<ApplicationEmployee>();

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public virtual ICollection<Question> Question { get; set; } = new List<Question>();

    public virtual Resume? SubmittedCv { get; set; }

    public virtual UserCompany UserCompany { get; set; } = null!;

}
