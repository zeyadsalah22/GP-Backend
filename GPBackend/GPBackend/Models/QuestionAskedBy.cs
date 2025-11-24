using System;

namespace GPBackend.Models;

public partial class QuestionAskedBy
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual CommunityInterviewQuestion Question { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

