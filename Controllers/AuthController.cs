using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Althaus_Warehouse.Services.AuthService;
using Althaus_Warehouse.Models.DTO;
using Althaus_Warehouse.Models.Entities;

namespace Althaus_Warehouse.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Asp.Versioning.ApiVersion(1.0)]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: /Auth/login
        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult GetLoginView()
        {
            // Return the login view
            return View("Index"); // Ensure this matches the name of your Razor view file
        }

        // API method for login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            // Validate input
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest(new { message = "Email and password are required." });
            }

            // Validate user credentials
            var (isValid, employeeDto) = await _authService.ValidateUser(loginDto.Email, loginDto.Password);

            if (!isValid)
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            // Check role and return token
            if (employeeDto.Role == EmployeeType.Manager.ToString() ||
                employeeDto.Role == EmployeeType.HR.ToString() ||
                employeeDto.Role == EmployeeType.Sales.ToString())
            {
                string token = _authService.GenerateToken(loginDto.Email, employeeDto.Role);
                return Ok(new { Token = token, Employee = employeeDto });
            }

            return Unauthorized(new { message = "You do not have access." });
        }
    }
}
