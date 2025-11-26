namespace GPBackend.DTOs.InterviewAnswer;

public class InterviewAnswerResponseDto
{
    public int AnswerId { get; set; }

    public int QuestionId { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? UserProfilePictureUrl { get; set; }

    public string AnswerText { get; set; } = null!;

    public bool GotOffer { get; set; }

    public int HelpfulCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool CurrentUserMarkedHelpful { get; set; }
}

