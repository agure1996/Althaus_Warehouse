using Althaus_Warehouse.Models.Entities;

namespace Althaus_Warehouse.Services
{
    public interface IEmployeeService
    {
        Task<Employee?> GetEmployeeByIdAsync(int employeeId);
        Task<Employee?> GetEmployeeByEmailAsync(string email);
        Task<bool> ValidateEmployeeAsync(string email, string password);
        Task CreateEmployeeAsync(Employee employee);
    }
}


