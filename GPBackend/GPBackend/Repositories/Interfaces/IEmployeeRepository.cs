using GPBackend.Models;
using GPBackend.DTOs.Employee;
using GPBackend.DTOs.Common;

namespace GPBackend.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<PagedResult<Employee>> GetFilteredAsync(int userId, EmployeeQueryDto queryDto);
        Task<IEnumerable<Employee>> GetAllAsync(int userId);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> CreateAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task<int> BulkSoftDeleteAsync(IEnumerable<int> ids, int userId);
        
        // New methods for ApplicationEmployee relationship management
        Task<bool> ValidateEmployeeIdsAsync(List<int> employeeIds, int userId, int companyId);
        Task<bool> AddApplicationEmployeesAsync(int applicationId, List<int> employeeIds);
        Task<bool> UpdateApplicationEmployeesAsync(int applicationId, List<int> employeeIds);
        Task<bool> RemoveApplicationEmployeesAsync(int applicationId);
    }
} 
