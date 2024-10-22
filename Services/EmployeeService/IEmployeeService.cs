using Althaus_Warehouse.Models.DTO.EmployeeDTOs;
using Althaus_Warehouse.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Althaus_Warehouse.Services
{
    public interface IEmployeeService
    {
        Task<Employee?> GetEmployeeByIdAsync(int employeeId);
        Task<Employee?> GetEmployeeByEmailAsync(string email);
        Task<bool> ValidateEmployeeAsync(string email, string password);
        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(int employeeId, EditEmployeeDTO employeeDTO);
        Task DeleteEmployeeAsync(int employeeId);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(); // Method to retrieve all employees
    }
}
