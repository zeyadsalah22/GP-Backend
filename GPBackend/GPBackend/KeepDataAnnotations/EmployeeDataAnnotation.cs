using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    {
    }

    public class EmployeeMetaData
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [ForeignKey("UserCompany")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("UserCompany")]
        public int UserCompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        public string? LinkedinLink { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? JobTitle { get; set; }

        [StringLength(255)]
        public string? Contacted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<ApplicationEmployee> ApplicationEmployees { get; set; } = new List<ApplicationEmployee>();

        public virtual UserCompany UserCompany { get; set; } = null!;

    }
} 