using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Question
{
    public class QuestionBatchCreateDto
    {
        [Required(ErrorMessage = "Questions list is required")]
        [MinLength(1, ErrorMessage = "At least one question is required")]
        public List<QuestionCreateDto> Questions { get; set; } = new List<QuestionCreateDto>();
    }
}
