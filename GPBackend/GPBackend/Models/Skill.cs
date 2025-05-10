using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class Skill
{
    public int SkillId { get; set; }

    public int TestId { get; set; }

    public string Skill1 { get; set; } = null!;

    public virtual ResumeTest Test { get; set; } = null!;
}
