using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class Resume
{
    public int ResumeId { get; set; }

    public int UserId { get; set; }

    public byte[] ResumeFile { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<ResumeTest> ResumeTests { get; set; } = new List<ResumeTest>();

    public virtual User User { get; set; } = null!;
}
