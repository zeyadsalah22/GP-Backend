using System;

namespace GPBackend.Models;

public partial class InterviewAnswerHelpful
{
    public int Id { get; set; }

    public int AnswerId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public virtual InterviewAnswer Answer { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

