using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using GPBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(CompanyMetaData))]
    public partial class Company
    {
    }

    public class CompanyMetaData
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        public string? Location { get; set; }

        [StringLength(255)]
        public string? CareersLink { get; set; }

        [StringLength(255)]
        public string? LinkedinLink { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public byte[]? Rowversion { get; set; }

        public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

        public virtual ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>();
    }
}