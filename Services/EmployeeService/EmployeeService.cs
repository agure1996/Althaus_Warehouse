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

        public async Task<bool> ValidateEmployeeAsync(string email, string password) // Updated to Async
        {
            var employee = await GetEmployeeByEmailAsync(email);
            return employee != null && BCrypt.Net.BCrypt.Verify(password, employee.PasswordHash);
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            await _employeeRepository.AddEmployeeAsync(employee);
        }
    }
}
