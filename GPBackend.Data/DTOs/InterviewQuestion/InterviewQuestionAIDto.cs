using GPBackend.Models;

using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.InterviewQuestion
{
    public class InterviewQuestionAIDto
    {
        [Required]
        public List<string> Questions { get; set; }
    }
}