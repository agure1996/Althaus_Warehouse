using Althaus_Warehouse.Models.DTO.EmployeeDTOs;

namespace Althaus_Warehouse.Services.AuthService
{
    public interface IAuthService1
    {
        string GenerateToken(string userName, string role);
        Task<(bool isValid, EmployeeDTO employeeDto)> ValidateUser(string email, string password);
    }
}