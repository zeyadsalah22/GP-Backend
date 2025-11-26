using GPBackend.Models.Enums;
using GPBackend.DTOs.InterviewAnswer;

namespace GPBackend.DTOs.CommunityInterviewQuestion;

public class CommunityInterviewQuestionDetailDto
{
    public int QuestionId { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? UserProfilePictureUrl { get; set; }

    public string QuestionText { get; set; } = null!;

    public int? CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanyLogo { get; set; }

    public RoleType RoleType { get; set; }

    public string? AddedRoleType { get; set; }

    public CommunityQuestionType QuestionType { get; set; }

    public string? AddedQuestionType { get; set; }

    public Difficulty Difficulty { get; set; }

    public int AskedCount { get; set; }

    public int AnswerCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool CurrentUserAskedThis { get; set; }

    public List<InterviewAnswerResponseDto> Answers { get; set; } = new List<InterviewAnswerResponseDto>();
}

    