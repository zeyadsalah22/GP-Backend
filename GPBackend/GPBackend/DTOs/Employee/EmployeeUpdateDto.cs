using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Employee
{
    public class EmployeeUpdateDto
    {
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
    }
} 
