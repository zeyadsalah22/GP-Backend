using System.ComponentModel.DataAnnotations;
using GPBackend.Models;

namespace GPBackend.DTOs.Interview
{
    public class InterviewResponseDto
    {
        public int InterviewId { get; set; }
        public int ApplicationId { get; set; }
        public int UserId { get; set; }

        public string? Position { get; set; }
        public string? Feedback { get; set; }

        // public string? jobDescription { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }

        

    }
}