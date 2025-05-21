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
        private readonly IUserCompanyService _userCompanyService;

        public EmployeeController(
            IEmployeeService employeeService,
            IUserCompanyService userCompanyService)
        {
            _employeeService = employeeService;
            _userCompanyService = userCompanyService;
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

        private async Task EnsureUserHasAccessToCompany(int companyId)
        {
            var userId = GetAuthenticatedUserId();
            var hasAccess = await _userCompanyService.UserHasAccessToCompany(userId, companyId);
            if (!hasAccess)
            {
                throw new UnauthorizedAccessException("User does not have access to this company");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees([FromQuery] EmployeeQueryDto queryDto)
        {
            // If company ID is provided, ensure user has access to it
            if (queryDto.CompanyId.HasValue)
            {
                await EnsureUserHasAccessToCompany(queryDto.CompanyId.Value);
            }

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

            // Ensure user has access to the company this employee belongs to
            await EnsureUserHasAccessToCompany(employee.CompanyId);

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(EmployeeCreationDto employeeDto)
        {
            // Set the authenticated user's ID
            employeeDto.UserId = GetAuthenticatedUserId();

            // Ensure user has access to the company
            await EnsureUserHasAccessToCompany(employeeDto.CompanyId);

            var createdEmployee = await _employeeService.CreateEmployeeAsync(employeeDto);
            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = createdEmployee.EmployeeId },
                createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee(int id, EmployeeUpdateDto employeeDto)
        {
            // First get the existing employee to check company access
            var existingEmployee = await _employeeService.GetEmployeeByIdAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            // Ensure user has access to the company
            await EnsureUserHasAccessToCompany(existingEmployee.CompanyId);

            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            // First get the existing employee to check company access
            var existingEmployee = await _employeeService.GetEmployeeByIdAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            // Ensure user has access to the company
            await EnsureUserHasAccessToCompany(existingEmployee.CompanyId);

            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
} 
