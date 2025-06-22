using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.ResumeTest
{
    public class ResumeTestCreateDto
    {
        [Required(ErrorMessage = "Resume ID is required")]
        public int ResumeId { get; set; }

        [Required(ErrorMessage = "Job description is required")]
        public string JobDescription { get; set; } = null!;
    }
} 