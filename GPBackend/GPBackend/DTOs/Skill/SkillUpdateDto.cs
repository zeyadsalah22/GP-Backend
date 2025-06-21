using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Skill
{
    public class SkillUpdateDto
    {
        [Required(ErrorMessage = "Skill name is required")]
        [StringLength(100, ErrorMessage = "Skill name cannot exceed 100 characters")]
        public string SkillName { get; set; } = null!;
    }
} 