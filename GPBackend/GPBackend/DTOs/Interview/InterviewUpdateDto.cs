using System.ComponentModel.DataAnnotations;
using GPBackend.Models;
using GPBackend.DTOs.InterviewQuestion;

namespace GPBackend.DTOs.Interview
{
    public class InterviewUpdateDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int ApplicationId { get; set; }
        public string? Position { get; set; }
        public string? JobDescription { get; set; }
        public string? Feedback { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }

        // public List<string> InterviewQuestionsAnswers { get; set; } = new List<string>();
        public List<InterviewQuestionUpdateDto> InterviewQuestions { get; set; } = new List<InterviewQuestionUpdateDto>();
    }
}
