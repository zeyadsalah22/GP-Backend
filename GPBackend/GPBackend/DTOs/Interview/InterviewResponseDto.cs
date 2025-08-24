using System.ComponentModel.DataAnnotations;
using GPBackend.Models;
using GPBackend.DTOs.InterviewQuestion;

namespace GPBackend.DTOs.Interview
{
    public class InterviewResponseDto
    {
        public int InterviewId { get; set; }
        public int ApplicationId { get; set; }

        public int CompanyId { get; set; }
        public int UserId { get; set; }

        public string? Position { get; set; }
        public string? Feedback { get; set; }
        public string? Notes { get; set; }

        public string? JobDescription { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }

        // interview questions
        public List<InterviewQuestionResponseDto> InterviewQuestions { get; set; } = new List<InterviewQuestionResponseDto>();
    }
}