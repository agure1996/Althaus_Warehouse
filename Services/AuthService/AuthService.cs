using Althaus_Warehouse.Services.AuthService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


public class AuthService : IAuthService
{
    private readonly string _secretKey;

    public AuthService(IConfiguration configuration)
    {
        _secretKey = configuration["Authentication:SecretKey"];
    }

    public string GenerateToken(string userName, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://localhost:5168/",
            audience: "althauswarehouse",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateUser(string userName, string password)
    {
        // TODO: Implement your user validation logic, e.g., check against a database
        // For example, you might want to check if the user is an admin or regular user
        return userName == "admin" && password == "password"; // This is just for demonstration
    }
}
