using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.ResumeTest
{
    public class ResumeTestCreateDto
    {
        [Required(ErrorMessage = "Resume ID is required")]
        public int ResumeId { get; set; }

        [Required(ErrorMessage = "Job description is required")]
        [StringLength(4000, ErrorMessage = "Job description cannot exceed 4000 characters")]
        public string JobDescription { get; set; } = null!;
    }
} 