using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Althaus_Warehouse.Models.DTO.EmployeeDTOs; // Adjust the namespace as necessary
using Althaus_Warehouse.Services.AuthService;
using Althaus_Warehouse.Models.DTO;
using Althaus_Warehouse.Models.Entities; // Ensure this namespace is included

namespace Althaus_Warehouse.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Asp.Versioning.ApiVersion(1.0)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous] // Allow access without authentication
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Validate input
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest("Email and password are required.");
            }

            // Validate user credentials
            var (isValid, employeeDto) = await _authService.ValidateUser(loginDto.Email, loginDto.Password);

            if (!isValid)
            {
                return Unauthorized("Invalid credentials.");
            }

            // Check if the employee is a Manager , HR , Sales
            if (employeeDto.Role != EmployeeType.Employee.ToString() || employeeDto.Role == EmployeeType.HR.ToString() || employeeDto.Role == EmployeeType.Sales.ToString())
            {
                // Generate token if the employee is a Manager
#pragma warning disable CS8604 // Possible null reference argument.
                string token = _authService.GenerateToken(loginDto.Email, employeeDto.Role);
#pragma warning restore CS8604 // Possible null reference argument.
                return Ok(new { Token = token, Employee = employeeDto });
            }
            else
            {
                // Instead of Forbid, return Unauthorized with a message
                return Unauthorized("You do not have access.");
            }
        }
    }
}
