using Althaus_Warehouse.Models.DTO.EmployeeDTOs;

namespace Althaus_Warehouse.Services.AuthService
{
    public interface IAuthService
    {
        string GenerateToken(string userName, string role);

        // Updated this method to be asynchronous and return a tuple
        Task<(bool isValid, EmployeeDTO employeeDto)> ValidateUser(string email, string password);
    }
}
