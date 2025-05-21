using Microsoft.EntityFrameworkCore;
using GPBackend.Models;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Employee;
using GPBackend.DTOs.Common;
using System.Linq.Dynamic.Core;

namespace GPBackend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly GPDBContext _context;

        public EmployeeService(GPDBContext context)
        {
            _context = context;
        }

        public async Task<PagedList<EmployeeDto>> GetFilteredEmployeesAsync(EmployeeQueryDto queryDto)
        {
            var query = _context.Employees
                .Where(e => !e.IsDeleted)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(queryDto.Search))
            {
                query = query.Where(e => 
                    e.Name.Contains(queryDto.Search) ||
                    e.JobTitle.Contains(queryDto.Search) ||
                    e.Contacted.Contains(queryDto.Search));
            }

            // Apply company filter
            if (queryDto.CompanyId.HasValue)
            {
                query = query.Where(e => e.CompanyId == queryDto.CompanyId.Value);
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(queryDto.SortBy))
            {
                // Convert the sortBy parameter to match property names (e.g., "name" -> "Name")
                string sortBy = char.ToUpper(queryDto.SortBy[0]) + queryDto.SortBy.Substring(1).ToLower();
                query = queryDto.SortDescending
                    ? query.OrderByDescending(sortBy)
                    : query.OrderBy(sortBy);
            }
            else
            {
                query = query.OrderByDescending(e => e.CreatedAt);
            }

            var employees = await PagedList<Employee>.CreateAsync(
                query,
                queryDto.PageNumber,
                queryDto.PageSize);

            // Map to DTOs
            var employeeDtos = employees.Select(e => MapToDto(e));
            return new PagedList<EmployeeDto>
            {
                Items = employeeDtos.ToList(),
                PageNumber = employees.PageNumber,
                PageSize = employees.PageSize,
                TotalCount = employees.TotalCount
            };
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == id && !e.IsDeleted);

            return employee != null ? MapToDto(employee) : null;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreationDto employeeDto)
        {
            var employee = new Employee
            {
                UserId = employeeDto.UserId,
                CompanyId = employeeDto.CompanyId,
                Name = employeeDto.Name,
                LinkedinLink = employeeDto.LinkedinLink,
                Email = employeeDto.Email,
                JobTitle = employeeDto.JobTitle,
                Contacted = employeeDto.Contacted,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return MapToDto(employee);
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeUpdateDto employeeDto)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == id && !e.IsDeleted);

            if (employee == null)
                return null;

            employee.Name = employeeDto.Name;
            employee.LinkedinLink = employeeDto.LinkedinLink;
            employee.Email = employeeDto.Email;
            employee.JobTitle = employeeDto.JobTitle;
            employee.Contacted = employeeDto.Contacted;
            employee.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToDto(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == id && !e.IsDeleted);

            if (employee != null)
            {
                employee.IsDeleted = true;
                employee.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        private static EmployeeDto MapToDto(Employee employee)
        {
            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                UserId = employee.UserId,
                CompanyId = employee.CompanyId,
                Name = employee.Name,
                LinkedinLink = employee.LinkedinLink,
                Email = employee.Email,
                JobTitle = employee.JobTitle,
                Contacted = employee.Contacted,
                CreatedAt = employee.CreatedAt,
                UpdatedAt = employee.UpdatedAt,
                IsDeleted = employee.IsDeleted
            };
        }
    }
} 
