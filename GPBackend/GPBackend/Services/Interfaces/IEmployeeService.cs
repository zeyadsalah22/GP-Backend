using GPBackend.DTOs.Employee;
using GPBackend.DTOs.Common;

namespace GPBackend.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagedList<EmployeeDto>> GetFilteredEmployeesAsync(EmployeeQueryDto queryDto);
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreationDto employeeDto);
        Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeUpdateDto employeeDto);
        Task DeleteEmployeeAsync(int id);
    }
} 
