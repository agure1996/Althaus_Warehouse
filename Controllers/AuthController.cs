using Althaus_Warehouse.Services;
using Althaus_Warehouse.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Asp.Versioning.ApiVersion(1.0)]
public class AuthController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IAuthService _authService;

    public AuthController(IEmployeeService employeeService, IAuthService authService)
    {
        _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService)) ;
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request) 
    {
        if (!await _employeeService.ValidateEmployeeAsync(request.UserName, request.Password)) 
            return Unauthorized();

        var token = _authService.GenerateToken(request.UserName, "Admin");
        return Ok(new { Token = token });
    }


}

public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
