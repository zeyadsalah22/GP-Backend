using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class ApplicationEmployee
{
    public int Id { get; set; }

    public int ApplicationId { get; set; }

    public int EmployeeId { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
