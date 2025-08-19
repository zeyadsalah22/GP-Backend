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
                query = ApplySorting(query, queryDto.SortBy, queryDto.SortDescending);
            }
            else
            {
                // Default sorting by CreatedAt if no sortBy is specified
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

        public async Task<int> BulkSoftDeleteAsync(IEnumerable<int> ids, int userId)
        {
            if (ids == null) return 0;
            var idList = ids.Distinct().ToList();
            if (idList.Count == 0) return 0;

            var employees = await _context.Employees
                .Where(e => idList.Contains(e.EmployeeId) && !e.IsDeleted && e.UserCompany.UserId == userId)
                .ToListAsync();

            foreach (var e in employees)
            {
                e.IsDeleted = true;
                e.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return employees.Count;
        }

        private async Task<bool> EmployeeExistsAsync(int id)
        {
            return await _context.Employees.AnyAsync(e => e.EmployeeId == id);
        }

        public async Task<bool> ValidateEmployeeIdsAsync(List<int> employeeIds, int userId, int companyId)
        {
            if (employeeIds == null || !employeeIds.Any())
                return true;

            // Check if all employee IDs exist and belong to the same company and user
            var validEmployeeCount = await _context.Employees
                .CountAsync(e => employeeIds.Contains(e.EmployeeId) &&
                               e.UserId == userId &&
                               e.CompanyId == companyId &&
                               !e.IsDeleted);

            return validEmployeeCount == employeeIds.Count;
        }

        public async Task<bool> AddApplicationEmployeesAsync(int applicationId, List<int> employeeIds)
        {
            if (employeeIds == null || !employeeIds.Any())
                return true;

            try
            {
                var applicationEmployees = employeeIds.Select(employeeId => new ApplicationEmployee
                {
                    ApplicationId = applicationId,
                    EmployeeId = employeeId
                }).ToList();

                _context.Set<ApplicationEmployee>().AddRange(applicationEmployees);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateApplicationEmployeesAsync(int applicationId, List<int> employeeIds)
        {
            try
            {
                // Remove existing relationships
                await RemoveApplicationEmployeesAsync(applicationId);

                // Add new relationships if provided
                if (employeeIds != null && employeeIds.Any())
                {
                    return await AddApplicationEmployeesAsync(applicationId, employeeIds);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveApplicationEmployeesAsync(int applicationId)
        {
            try
            {
                var existingRelationships = await _context.Set<ApplicationEmployee>()
                    .Where(ae => ae.ApplicationId == applicationId)
                    .ToListAsync();

                if (existingRelationships.Any())
                {
                    _context.Set<ApplicationEmployee>().RemoveRange(existingRelationships);
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private IQueryable<Employee> ApplySorting(IQueryable<Employee> query, string sortBy, bool sortDescending)
        {
            Expression<Func<Employee, object>> keySelector = sortBy.ToLower() switch
            {
                "email" => e => e.Email,
                "name" => e => e.Name,
                "createdat" => e => e.CreatedAt,
                "updatedat" => e => e.UpdatedAt,
                "companyname" => e => e.UserCompany.Company.Name,
                "jobtitle" => e => e.JobTitle,
                _ => e => e.CreatedAt // Default sorting by created at
            };
            return sortDescending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
        }
    }
} 
