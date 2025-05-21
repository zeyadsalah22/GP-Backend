using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Question
{
    public class QuestionUpdateDto
    {
        [StringLength(255, ErrorMessage ="Question length cannot exceed 255 character")]
        public string? Question1 { get; set; } = null!;

        [StringLength(255, ErrorMessage = "Answer Length cannot exceed 255 characters")]
        public string? Answer { get; set; }
    }
}