using AutoMapper;
using GPBackend.Models;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.Employee;
using GPBackend.DTOs.Common;
using GPBackend.Repositories.Interfaces;

namespace GPBackend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<EmployeeDto>> GetFilteredEmployeesAsync(EmployeeQueryDto queryDto)
        {
            var result = await _employeeRepository.GetFilteredAsync(queryDto);

            return new PagedResult<EmployeeDto>
            {
                Items = result.Items.Select(e => _mapper.Map<EmployeeDto>(e)).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return employee != null ? _mapper.Map<EmployeeDto>(employee) : null;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreationDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var createdEmployee = await _employeeRepository.CreateAsync(employee);
            return _mapper.Map<EmployeeDto>(createdEmployee);
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeUpdateDto employeeDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return null;

            _mapper.Map(employeeDto, employee);
            await _employeeRepository.UpdateAsync(employee);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }
    }
} 
