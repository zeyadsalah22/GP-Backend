﻿using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class UserCompany
{
    public int UserId { get; set; }

    public int CompanyId { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual User User { get; set; } = null!;
}
