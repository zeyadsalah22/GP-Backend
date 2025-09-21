using GPBackend.DTOs.Employee;
using GPBackend.DTOs.Common;

namespace GPBackend.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagedResult<EmployeeDto>> GetFilteredEmployeesAsync(int userId, EmployeeQueryDto queryDto);
        Task<EmployeeDto> GetEmployeeByIdAsync(int id, int userId);
        Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreationDto employeeDto);
        Task<EmployeeDto> UpdateEmployeeAsync(int id, int userId, EmployeeUpdateDto employeeDto);
        Task<bool> DeleteEmployeeAsync(int id, int userId);
        Task<int> BulkDeleteEmployeesAsync(IEnumerable<int> ids, int userId);
    }
} 
