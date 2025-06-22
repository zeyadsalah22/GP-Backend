using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Question
{
    public class QuestionCreateDto {
        [Required(ErrorMessage = "Question is Required!!")]
        [StringLength(255, ErrorMessage ="Question length cannot exceed 255 character")]
        public string Question1 { get; set; } = null!;

        [StringLength(4000, ErrorMessage = "Answer Length cannot exceed 4000 characters")]
        public string? Answer { get; set; }

        [Required(ErrorMessage = "Application field is required")]
        public int ApplicationId { get; set; }
    }
}