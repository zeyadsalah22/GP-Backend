using GPBackend.Models;
using GPBackend.DTOs.TodoList;

namespace GPBackend.Services.Interfaces
{
    public interface ITodoListService
    {
        Task<IEnumerable<TodoListResponseDto>> GetAllTodoItemsAsync(int userId);
        Task<TodoListResponseDto?> GetTodoItemByIdAsync(int id, int userId);

        Task<TodoListResponseDto?> CreateTodoItemAsync(int user_id, TodoListCreateDto todoListCreateDto);

        Task<bool> UpdateTodoListByIdAsync(int id, int userId, TodoListUpdateDto todoListUpdateDto);

        Task<bool> DeleteTodoItemByIdAsync(int id, int userId);

    }
}