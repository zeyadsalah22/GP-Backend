using GPBackend.Models;
using GPBackend.DTOs.Employee;
using GPBackend.DTOs.Common;

namespace GPBackend.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<PagedResult<Employee>> GetFilteredAsync(EmployeeQueryDto queryDto);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> CreateAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
    }
} 
