using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(TodoListMetaData))]
    public partial class TodoList
    {
    }

    public class TodoListMetaData
    {
        [Key]
        public int TodoId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string ApplicationTitle { get; set; } = null!;

        [StringLength(255)]
        public string? ApplicationLink { get; set; }

        public DateTime? Deadline { get; set; }

        public bool Completed { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public virtual User User { get; set; } = null!;
    }
} 