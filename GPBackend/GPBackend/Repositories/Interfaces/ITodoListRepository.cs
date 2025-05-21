using GPBackend.DTOs.TodoList;
using GPBackend.DTOs.Common;
using GPBackend.Models;

namespace GPBackend.Repositories.Interfaces
{
    public interface ITodoListRepository
    {
        Task<IEnumerable<TodoList>> GetAllAsync(int userId);
        Task<TodoList?> GetByIdAsync(int id, int userId);
        Task<TodoList> CreateAsync(TodoList TodoList);
        Task<bool> UpdateAsync(TodoList TodoList);
        Task<bool> DeleteAsync(int id, int userId);
        // Task<bool> TodoListExistsAsync(int id);
    }
}