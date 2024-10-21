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
    private readonly WarehouseDbContext _context; 

    public AuthService(IConfiguration configuration, WarehouseDbContext context)
    {
        _secretKey = configuration["Authentication:SecretKey"];
        _context = context; // Initialize your DbContext
    }

    public string GenerateToken(string email, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, role)
        };

#pragma warning disable CS8604 // Possible null reference argument.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
#pragma warning restore CS8604 // Possible null reference argument.
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://localhost:5168/",
            audience: "althauswarehouse",
            claims: claims,
            expires: DateTime.Now.AddHours(12),
            signingCredentials: creds);

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
