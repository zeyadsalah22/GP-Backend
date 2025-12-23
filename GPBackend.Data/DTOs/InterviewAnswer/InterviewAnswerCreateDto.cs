using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.InterviewAnswer;

public class InterviewAnswerCreateDto
{
    [Required(ErrorMessage = "Answer text is required")]
    [MaxLength(5000, ErrorMessage = "Answer text cannot exceed 5000 characters")]
    public string AnswerText { get; set; } = null!;

    public bool GotOffer { get; set; }
}

