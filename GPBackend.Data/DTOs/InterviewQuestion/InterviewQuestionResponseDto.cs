using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.InterviewQuestion
{
    public class InterviewQuestionResponseDto
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public int InterviewId { get; set; }
    }
}
