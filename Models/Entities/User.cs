namespace Althaus_Warehouse.Models.Entities
{
    public class User
    {
        public int UserId { get; set; } // Unique identifier for the user
        public string UserName { get; set; } // Username
        public string PasswordHash { get; set; } // Hashed password
        public string Role { get; set; } // User role (e.g., Admin, User)

    }

}
