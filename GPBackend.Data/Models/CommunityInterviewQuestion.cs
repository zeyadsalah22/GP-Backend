using System;
using System.Collections.Generic;
using GPBackend.Models.Enums;

namespace GPBackend.Models;

public partial class CommunityInterviewQuestion
{
    public int QuestionId { get; set; }

    public int UserId { get; set; }

    public string QuestionText { get; set; } = null!;

    public int? CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanyLogo { get; set; }

    public RoleType RoleType { get; set; }

    public string? AddedRoleType { get; set; }

    public CommunityQuestionType QuestionType { get; set; }

    public string? AddedQuestionType { get; set; }

    public Difficulty Difficulty { get; set; }

    public int AskedCount { get; set; } = 0;

    public int AnswerCount { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual Company? Company { get; set; }

    public virtual ICollection<InterviewAnswer> Answers { get; set; } = new List<InterviewAnswer>();

    public virtual ICollection<QuestionAskedBy> AskedByUsers { get; set; } = new List<QuestionAskedBy>();
}

