using Microsoft.AspNetCore.Mvc;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Employee;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GPBackend.DTOs.Common;
using System.ComponentModel.DataAnnotations;

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
            try
            {
                var userId = GetAuthenticatedUserId();

                var result = await _employeeService.GetFilteredEmployeesAsync(userId, queryDto);

                // Add pagination headers
                Response.Headers.Add("X-Pagination-TotalCount", result.TotalCount.ToString());
                Response.Headers.Add("X-Pagination-PageSize", result.PageSize.ToString());
                Response.Headers.Add("X-Pagination-CurrentPage", result.PageNumber.ToString());
                Response.Headers.Add("X-Pagination-TotalPages", result.TotalPages.ToString());
                Response.Headers.Add("X-Pagination-HasNext", result.HasNext.ToString());
                Response.Headers.Add("X-Pagination-HasPrevious", result.HasPrevious.ToString());

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var userId = GetAuthenticatedUserId();
            var employee = await _employeeService.GetEmployeeByIdAsync(id, userId);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody][Required] EmployeeCreationDto employeeDto)
        {
            // Set the authenticated user's ID
            var UserId = GetAuthenticatedUserId();

            if (employeeDto == null)
            {
                return BadRequest(new { message = "Request body is required" });
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            if (employeeDto.UserId != UserId)
            {
                return Forbid();
            }

            var createdEmployee = await _employeeService.CreateEmployeeAsync(employeeDto);
            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = createdEmployee.EmployeeId },
                createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee(int id, [FromBody][Required] EmployeeUpdateDto employeeDto)
        {
            try
            {
                if (employeeDto == null)
                {
                    return BadRequest(new { message = "Request body is required" });
                }
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }
                var userId = GetAuthenticatedUserId();

                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, userId, employeeDto);
                if (updatedEmployee == null)
                {
                    return NotFound();
                }
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var userId = GetAuthenticatedUserId();
                var isDeleted = await _employeeService.DeleteEmployeeAsync(id, userId);
                if (!isDeleted)
                {
                    return NotFound();
                }
                return NoContent();
            }   
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("bulk-delete")]
        public async Task<IActionResult> BulkDeleteEmployees([FromBody][Required] BulkDeleteRequestDto request)
        {
            var userId = GetAuthenticatedUserId();
            if (request == null || request.Ids == null || request.Ids.Count == 0)
            {
                return BadRequest(new { message = "Ids list is required" });
            }
            var deleted = await _employeeService.BulkDeleteEmployeesAsync(request.Ids, userId);
            return Ok(new { deletedCount = deleted });
        }
    }
} 
