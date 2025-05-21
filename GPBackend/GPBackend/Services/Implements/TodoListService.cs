using AutoMapper;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.TodoList;

namespace GPBackend.Services.Implements
{
    public class TodoListService : ITodoListService
    {
        private ITodoListRepository _todoListRepository;

        private readonly IMapper _mapper;

        public TodoListService(
            ITodoListRepository _todoListRepository,
            IMapper _mapper
        )
        {
            this._todoListRepository = _todoListRepository;
            this._mapper = _mapper;
        }
        public async Task<IEnumerable<TodoListResponseDto>> GetAllTodoItemsAsync(int userId)
        {
            var todoItems = await _todoListRepository.GetAllAsync(userId);
            return _mapper.Map<IEnumerable<TodoListResponseDto>>(todoItems);
        }

        public async Task<TodoListResponseDto?> GetTodoItemByIdAsync(int id, int userId)
        {
            var todoList = await _todoListRepository.GetByIdAsync(id, userId);
            return todoList != null ? _mapper.Map<TodoListResponseDto>(todoList) : null;
        }

        public async Task<TodoListResponseDto?> CreateTodoItemAsync(int userId, TodoListCreateDto todoListCreateDto)
        {
            // check valid user
            if (userId != todoListCreateDto.UserId)
            {
                return null;
            }
            var todoList = _mapper.Map<TodoList>(todoListCreateDto);
            var createdTodoList = await _todoListRepository.CreateAsync(todoList);
            return _mapper.Map<TodoListResponseDto>(createdTodoList);
        }

        public async Task<bool> UpdateTodoListByIdAsync(int id, int userId, TodoListUpdateDto todoListUpdateDto)
        {
            var todoList = await _todoListRepository.GetByIdAsync(id, userId);
            if (todoList == null)
            {
                return false;
            }

            _mapper.Map(todoListUpdateDto, todoList);
            todoList.UpdatedAt = DateTime.UtcNow;

            return await _todoListRepository.UpdateAsync(todoList);

        }

        public async Task<bool> DeleteTodoItemByIdAsync(int id, int userId)
        {
            return await _todoListRepository.DeleteAsync(id, userId);
        }

    }
    
}