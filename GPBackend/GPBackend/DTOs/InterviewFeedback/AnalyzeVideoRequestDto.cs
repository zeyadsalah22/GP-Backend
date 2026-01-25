using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GPBackend.DTOs.InterviewFeedback
{
    // Backend API request DTO (multipart/form-data)
    public class AnalyzeVideoRequestDto
    {
        [Required]
        public IFormFile File { get; set; } = null!;
    }
}


