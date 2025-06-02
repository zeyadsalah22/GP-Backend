using System.ComponentModel.DataAnnotations;
using GPBackend.Models;

namespace GPBackend.DTOs.Interview
{
    public class InterviewCreateDto
    {
        public int ApplicationId { get; set; }
    
        [Required (ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string? Position { get; set; }
        public string? JobDescription { get; set; }

    }
}