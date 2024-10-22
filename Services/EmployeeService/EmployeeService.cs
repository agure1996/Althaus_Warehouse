using Althaus_Warehouse.Models.DTO.EmployeeDTOs;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Services.Repositories;

namespace Althaus_Warehouse.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(employeeId);
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            return await _employeeRepository.GetEmployeeByEmailAsync(email);
        }

        public async Task<bool> ValidateEmployeeAsync(string email, string password)
        {
            return await _employeeRepository.ValidateEmployeeCredentialsAsync(email, password);
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            await _employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task UpdateEmployeeAsync(int employeeId, EditEmployeeDTO employeeDTO)
        {
            var employee = await GetEmployeeByIdAsync(employeeId);
            if (employee == null) throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");

            // Update properties based on the DTO
            employee.FirstName = employeeDTO.FirstName;
            employee.LastName = employeeDTO.LastName;
            employee.Email = employeeDTO.Email;
            employee.EmployeeType = Enum.Parse<EmployeeType>(employeeDTO.EmployeeType); // Ensure correct Enum conversion

            // Hash password only if provided
            if (!string.IsNullOrWhiteSpace(employeeDTO.Password))
            {
                employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(employeeDTO.Password); // Hash password before saving
            }

            await _employeeRepository.UpdateEmployeeAsync(employee);
        }




        public async Task DeleteEmployeeAsync(int employeeId)
        {
            await _employeeRepository.DeleteEmployeeAsync(employeeId);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
        }
    }
}
