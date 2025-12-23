using System.ComponentModel.DataAnnotations;
using GPBackend.Models.Enums;

namespace GPBackend.DTOs.CommunityInterviewQuestion;

public class CommunityInterviewQuestionCreateDto
{
    [Required(ErrorMessage = "Question text is required")]
    [MaxLength(3000, ErrorMessage = "Question text cannot exceed 3000 characters")]
    public string QuestionText { get; set; } = null!;

    public int? CompanyId { get; set; }

    [MaxLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
    public string? CompanyName { get; set; }

    [MaxLength(500, ErrorMessage = "Company logo URL cannot exceed 500 characters")]
    public string? CompanyLogo { get; set; }

    [Required(ErrorMessage = "Role type is required")]
    public RoleType RoleType { get; set; }

    [MaxLength(100, ErrorMessage = "Added role type cannot exceed 100 characters")]
    public string? AddedRoleType { get; set; }

    [Required(ErrorMessage = "Question type is required")]
    public CommunityQuestionType QuestionType { get; set; }

    [MaxLength(100, ErrorMessage = "Added question type cannot exceed 100 characters")]
    public string? AddedQuestionType { get; set; }

    [Required(ErrorMessage = "Difficulty is required")]
    public Difficulty Difficulty { get; set; }
}

