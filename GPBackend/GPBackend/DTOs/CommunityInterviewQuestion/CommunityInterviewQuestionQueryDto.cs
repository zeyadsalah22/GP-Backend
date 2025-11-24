using GPBackend.Models.Enums;

namespace GPBackend.DTOs.CommunityInterviewQuestion;

public class CommunityInterviewQuestionQueryDto
{
    public string? SearchText { get; set; }

    public List<int>? CompanyIds { get; set; }

    public string? CompanyName { get; set; }

    public RoleType? RoleType { get; set; }

    public string? AddedRoleType { get; set; }

    public CommunityQuestionType? QuestionType { get; set; }

    public string? AddedQuestionType { get; set; }

    public Difficulty? Difficulty { get; set; }

    public bool? MostFrequentlyAsked { get; set; }

    public string? SortBy { get; set; } = "MostRecent"; // MostRecent, MostAsked, MostAnswered

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}

