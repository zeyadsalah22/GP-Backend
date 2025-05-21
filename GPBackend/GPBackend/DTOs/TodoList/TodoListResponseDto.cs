using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.TodoList
{
    public class TodoListResponseDto
    {
        public int TodoId { get; set; }

        public int UserId { get; set; }

        public string ApplicationTitle { get; set; } = null!;

        public string? ApplicationLink { get; set; }

        public DateTime? Deadline { get; set; }

        public bool Completed { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}