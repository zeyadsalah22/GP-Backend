using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(UserCompanyMetaData))]
    public partial class UserCompany
    {
    }

    public class UserCompanyMetaData
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Key]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [StringLength(2000)]
        public string? PersonalNotes { get; set; }

        [Required]
        public Models.Enums.InterestLevel InterestLevel { get; set; }

        public bool Favorite { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        public virtual Company Company { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public virtual User User { get; set; } = null!;
    }
} 