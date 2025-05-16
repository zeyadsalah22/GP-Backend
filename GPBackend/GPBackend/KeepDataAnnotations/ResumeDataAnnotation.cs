using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(ResumeMetaData))]
    public partial class Resume
    {
    }

    public class ResumeMetaData
    {
        [Key]
        public int ResumeId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public byte[] ResumeFile { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        public virtual ICollection<ResumeTest> ResumeTests { get; set; } = new List<ResumeTest>();

        public virtual User User { get; set; } = null!;
    }

}