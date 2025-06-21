using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Skill
{
    public class SkillCreateDto
    {
        [Required(ErrorMessage = "Test ID is required")]
        public int TestId { get; set; }

        [Required(ErrorMessage = "Skill name is required")]
        [StringLength(100, ErrorMessage = "Skill name cannot exceed 100 characters")]
        public string SkillName { get; set; } = null!;
    }
} 