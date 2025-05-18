using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int UserId { get; set; }

    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string? LinkedinLink { get; set; }

    public string? Email { get; set; }

    public string? JobTitle { get; set; }

    public string? Contacted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<ApplicationEmployee> ApplicationEmployees { get; set; } = new List<ApplicationEmployee>();

    public virtual UserCompany UserCompany { get; set; } = null!;

}
