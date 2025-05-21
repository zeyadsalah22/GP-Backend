using GPBackend.Models;
using GPBackend.DTOs.TodoList;
using GPBackend.DTOs.Common;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly GPDBContext _context;

        public TodoListRepository(GPDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TodoList>> GetAllAsync(int userId)
        {
            return await _context.TodoLists
                .Where(c => !c.IsDeleted && c.UserId == userId)
                .ToListAsync();
        }
        public async Task<TodoList?> GetByIdAsync(int id, int userId)
        {
            return await _context.TodoLists
                        .Where(c => !c.IsDeleted && c.TodoId == id && c.UserId == userId)
                        .FirstOrDefaultAsync();
        }
        public async Task<TodoList> CreateAsync(TodoList TodoList)
        {
            _context.TodoLists.Add(TodoList);
            await _context.SaveChangesAsync();
            return TodoList;
        }
        public async Task<bool> UpdateAsync(TodoList TodoList)
        {
            try
            {
                TodoList.UpdatedAt = DateTime.UtcNow;
                _context.TodoLists.Update(TodoList);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TodoListExistsAsync(TodoList.TodoId))
                {
                    return false;
                }
                throw;
            }
        }
        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var todoList = await GetByIdAsync(id, userId);
            if (todoList == null)
            {
                return false;
            }

            // Soft delete
            todoList.IsDeleted = true;
            todoList.UpdatedAt = DateTime.UtcNow;
            _context.TodoLists.Update(todoList);
            await _context.SaveChangesAsync();
            return true;
        }
        
        private async Task<bool> TodoListExistsAsync(int id)
        {
            return await _context.TodoLists.AnyAsync(c => c.TodoId == id);
        }
    }
}