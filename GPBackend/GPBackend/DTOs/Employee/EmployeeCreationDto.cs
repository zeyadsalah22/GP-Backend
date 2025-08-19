using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Employee
{
    public class EmployeeCreationDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? LinkedinLink { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? JobTitle { get; set; }

        [StringLength(255)]
        public string? Contacted { get; set; }

        [StringLength(50)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }
    }
} 
