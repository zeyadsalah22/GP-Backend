using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class ResumeTest
{
    public int TestId { get; set; }

    public int ResumeId { get; set; }

    public double AtsScore { get; set; }

    public DateTime TestDate { get; set; }

    public string? JobDescription { get; set; }

    public virtual Resume Resume { get; set; } = null!;

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
