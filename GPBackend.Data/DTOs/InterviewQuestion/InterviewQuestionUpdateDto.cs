using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.InterviewQuestion
{
    public class InterviewQuestionUpdateDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Answer is required.")]
        public string Answer { get; set; } = string.Empty;

        public int InterviewId { get; set; }
    }
}
