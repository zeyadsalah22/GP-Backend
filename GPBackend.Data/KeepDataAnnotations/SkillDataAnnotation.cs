using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(SkillMetaData))]
    public partial class Skill
    {
    }

    public class SkillMetaData
    {
        [Key]
        public int SkillId { get; set; }

        [Required]
        [ForeignKey("ResumeTest")]
        public int TestId { get; set; }

        [Required]
        [StringLength(100)]
        public string Skill1 { get; set; } = null!;

        public virtual ResumeTest Test { get; set; } = null!;
    }
} 