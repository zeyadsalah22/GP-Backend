using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(ResumeTestMetaData))]
    public partial class ResumeTest
    {
    }

    public class ResumeTestMetaData
    {
        [Key]
        public int TestId { get; set; }

        [Required]
        [ForeignKey("Resume")]
        public int ResumeId { get; set; }

        [Required]
        public int AtsScore { get; set; }

        public DateTime TestDate { get; set; }

        [StringLength(4000)]
        public string? JobDescription { get; set; }

        public virtual Resume Resume { get; set; } = null!;

        public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
} 