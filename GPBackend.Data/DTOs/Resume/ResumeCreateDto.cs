using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Resume
{
    public class ResumeCreateDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Resume file is required")]
        public byte[] ResumeFile { get; set; } = null!;
    }
}
