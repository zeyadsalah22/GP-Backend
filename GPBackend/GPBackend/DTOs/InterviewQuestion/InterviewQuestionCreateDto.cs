using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.InterviewQuestion
{
    public class InterviewQuestionCreateDto
    {
        [Required(ErrorMessage = "InterviewId is required.")]
        public int InterviewId { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        public string Question { get; set; } = string.Empty;
        public string? Answer { get; set; } = string.Empty;
    }
}
