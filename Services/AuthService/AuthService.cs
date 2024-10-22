using Althaus_Warehouse.Models.DTO.EmployeeDTOs; // Import the EmployeeDTO namespace
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Althaus_Warehouse.Services.AuthService;
using Althaus_Warehouse.DBContext;

public class AuthService : IAuthService
{
    private readonly string? _secretKey;
    private readonly IConfiguration _configuration; // Added IConfiguration
    private readonly WarehouseDbContext _context;

    public AuthService(IConfiguration configuration, WarehouseDbContext context)
    {
        _configuration = configuration; // Initialize IConfiguration
        _secretKey = configuration["Authentication:SecretKey"];
        _context = context; // Initialize your DbContext
    }

    public string GenerateToken(string email, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email), 
            new Claim(ClaimTypes.Role, role)   
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Read issuer and audience from the configuration
        var issuer = _configuration["Authentication:Issuer"];
        var audience = _configuration["Authentication:Audience"];

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<(bool isValid, EmployeeDTO employeeDto)> ValidateUser(string email, string password)
    {
        // Fetch the employee from the database
        var employee = await _context.Employees
                                     .FirstOrDefaultAsync(e => e.Email == email && e.IsActive);

        if (employee == null)
        {
            Console.WriteLine($"User with email {email} not found or is not active.");
            return (false, null); // Invalid credentials
        }

        // Check if the password is valid
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, employee.PasswordHash);
        if (!isPasswordValid)
        {
            Console.WriteLine("Invalid password attempt.");
            return (false, null); // Invalid credentials
        }

        // Map the employee to EmployeeDTO
        var employeeDto = new EmployeeDTO
        {
            Id = employee.Id,
            Name = employee.GetFullName(),
            HireDate = employee.DateHired,
            Role = employee.EmployeeType.ToString(),
            IsActive = employee.IsActive,
            Email = employee.Email
        };

        return (true, employeeDto);
    }
}
