using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Employee;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GPBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

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
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees([FromQuery] EmployeeQueryDto queryDto)
        {
            var result = await _employeeService.GetFilteredEmployeesAsync(queryDto);

            // Add pagination headers
            Response.Headers.Add("X-Pagination-TotalCount", result.TotalCount.ToString());
            Response.Headers.Add("X-Pagination-PageSize", result.PageSize.ToString());
            Response.Headers.Add("X-Pagination-CurrentPage", result.PageNumber.ToString());
            Response.Headers.Add("X-Pagination-TotalPages", result.TotalPages.ToString());
            Response.Headers.Add("X-Pagination-HasNext", result.HasNext.ToString());
            Response.Headers.Add("X-Pagination-HasPrevious", result.HasPrevious.ToString());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(EmployeeCreationDto employeeDto)
        {
            // Set the authenticated user's ID
            employeeDto.UserId = GetAuthenticatedUserId();

            var createdEmployee = await _employeeService.CreateEmployeeAsync(employeeDto);
            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = createdEmployee.EmployeeId },
                createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee(int id, EmployeeUpdateDto employeeDto)
        {
            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
            if (updatedEmployee == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
} 