using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.TodoList
{
    public class TodoListUpdateDto
    {

        [Required]
        public int UserId { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string ApplicationTitle { get; set; } = null!;

        [StringLength(255)]
        public string? ApplicationLink { get; set; }

        public DateTime? Deadline { get; set; }

        [Required]
        public bool Completed { get; set; }
    }
}