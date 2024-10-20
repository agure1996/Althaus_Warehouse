namespace Althaus_Warehouse.Services.AuthService
{
    public interface IAuthService
    {
        string GenerateToken(string userName, string role);
        bool ValidateUser(string userName, string password);
    }

}
