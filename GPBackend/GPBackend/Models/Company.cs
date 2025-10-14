using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public string? CareersLink { get; set; }

    public string? LinkedinLink { get; set; }

    public int IndustryId { get; set; }

    //public Models.Enums.CompanySize CompanySize { get; set; }
    public string CompanySize { get; set; } = null!;

    //public byte[]? Logo { get; set; }
    public string? LogoUrl { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public byte[]? Rowversion { get; set; } = null!;

    public virtual Industry? Industry { get; set; }

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public virtual ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>();
}
