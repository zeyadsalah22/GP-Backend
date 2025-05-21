using Microsoft.EntityFrameworkCore;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.DTOs.Employee;
using GPBackend.DTOs.Common;
using System.Linq.Expressions;

namespace GPBackend.Repositories.Implements
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly GPDBContext _context;

        public EmployeeRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Employee>> GetFilteredAsync(int userId, EmployeeQueryDto queryDto)
        {
            var query = _context.Employees
                .Include(e => e.UserCompany)
                .Include(e => e.UserCompany.Company) // Include Company data
                .Where(e => !e.IsDeleted && e.UserCompany.UserId == userId)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(queryDto.Search))
            {
                query = query.Where(e => 
                    e.Name.Contains(queryDto.Search) ||
                    (e.JobTitle != null && e.JobTitle.Contains(queryDto.Search)) ||
                    (e.Contacted != null && e.Contacted.Contains(queryDto.Search)));
            }

            // Apply company filter
            if (queryDto.CompanyId.HasValue)
            {
                query = query.Where(e => e.CompanyId == queryDto.CompanyId.Value);
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(queryDto.SortBy))
            {
                // Normalize the sort field name
                string sortBy = char.ToUpper(queryDto.SortBy[0]) + queryDto.SortBy.Substring(1).ToLower();
                
                // Apply appropriate sorting based on property name
                switch (sortBy)
                {
                    case "Name":
                        query = queryDto.SortDescending 
                            ? query.OrderByDescending(e => e.Name)
                            : query.OrderBy(e => e.Name);
                        break;
                    case "JobTitle":
                        query = queryDto.SortDescending 
                            ? query.OrderByDescending(e => e.JobTitle)
                            : query.OrderBy(e => e.JobTitle);
                        break;
                    case "CreatedAt":
                        query = queryDto.SortDescending 
                            ? query.OrderByDescending(e => e.CreatedAt)
                            : query.OrderBy(e => e.CreatedAt);
                        break;
                    case "Email":
                        query = queryDto.SortDescending 
                            ? query.OrderByDescending(e => e.Email)
                            : query.OrderBy(e => e.Email);
                        break;
                    default:
                        query = query.OrderByDescending(e => e.CreatedAt);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(e => e.CreatedAt);
            }

            // Get total count
            var totalCount = await query.CountAsync();

            // Apply pagination
            var items = await query
                .Skip((queryDto.PageNumber - 1) * queryDto.PageSize)
                .Take(queryDto.PageSize)
                .ToListAsync();

            return new PagedResult<Employee>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = queryDto.PageNumber,
                PageSize = queryDto.PageSize
            };
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(int userId)
        {
            return await _context.Employees
                .Include(e => e.UserCompany.Company) // Include related company data
                .Where(e => !e.IsDeleted && e.UserCompany.UserId == userId)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.UserCompany.Company) // Include related company data
                .FirstOrDefaultAsync(e => e.EmployeeId == id && !e.IsDeleted);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            
            // Reload the employee with UserCompany and Company data
            return await _context.Employees
                .Include(e => e.UserCompany)
                .Include(e => e.UserCompany.Company)
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EmployeeExistsAsync(employee.EmployeeId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await GetByIdAsync(id);
            if (employee == null)
            {
                return false;
            }

            employee.IsDeleted = true;
            employee.UpdatedAt = DateTime.UtcNow;
            return await UpdateAsync(employee);
        }

        private async Task<bool> EmployeeExistsAsync(int id)
        {
            return await _context.Employees.AnyAsync(e => e.EmployeeId == id);
        }
    }
} 
