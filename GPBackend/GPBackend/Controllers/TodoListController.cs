using Microsoft.AspNetCore.Mvc;
using GPBackend.Models;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.TodoList;
using GPBackend.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/todos")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _todoListService;

        public TodoListController(ITodoListService todoListService)
        {
            this._todoListService = todoListService;
        }

        // Helper method to get the authenticated user's ID
        private int GetAuthenticatedUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated properly");
            }
            return userId;
        }

        [HttpGet]
        public async Task<ActionResult<TodoListResponseDto>> GetAllTodoItems()
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var todoList = await _todoListService.GetAllTodoItemsAsync(userId);

                return Ok(todoList);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoListResponseDto>> GetTodoItemById(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var todoItem = await _todoListService.GetTodoItemByIdAsync(id, userId);
                if (todoItem == null)
                {
                    return NotFound();
                }

                return Ok(todoItem);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        // TODO: Check for nullibility of `createdTodoList`, Is it a case? should we bother?
        // check it at `TodoListService`

        [HttpPost]
        public async Task<ActionResult<TodoListResponseDto>> CreateTodoItem(TodoListCreateDto todoListCreateDto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                try
                {
                    var createdTodoList = await _todoListService.CreateTodoItemAsync(userId, todoListCreateDto)
                                                ?? throw new Exception("An unknown error ocurred while creating todoList");
                    return CreatedAtAction(
                        nameof(GetTodoItemById),
                        new { id = createdTodoList.TodoId },
                        createdTodoList
                    );
                }catch (InvalidOperationException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoListById(int id, TodoListUpdateDto todoListUpdateDto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _todoListService.UpdateTodoListByIdAsync(id, userId, todoListUpdateDto);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItemById(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var result = await _todoListService.DeleteTodoItemByIdAsync(id, userId);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }   
}