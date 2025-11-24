using System;
using System.Collections.Generic;

namespace GPBackend.Models;

public partial class InterviewAnswer
{
    public int AnswerId { get; set; }

    public int QuestionId { get; set; }

    public int UserId { get; set; }

    public string AnswerText { get; set; } = null!;

    public bool GotOffer { get; set; }

    public int HelpfulCount { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public virtual CommunityInterviewQuestion Question { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<InterviewAnswerHelpful> HelpfulVotes { get; set; } = new List<InterviewAnswerHelpful>();
}

