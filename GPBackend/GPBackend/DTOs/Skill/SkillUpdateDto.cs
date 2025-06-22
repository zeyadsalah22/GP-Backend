using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Skill
{
    public class SkillUpdateDto
    {
        [Required(ErrorMessage = "Skill name is required")]
        [MinLength(2, ErrorMessage = "Skill name must be at least 2 characters long")]
        [MaxLength(100, ErrorMessage = "Skill name cannot exceed 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_\.]+$", ErrorMessage = "Skill name can only contain letters, numbers, spaces, hyphens, underscores, and dots")]
        public string SkillName { get; set; } = null!;
    }
} 