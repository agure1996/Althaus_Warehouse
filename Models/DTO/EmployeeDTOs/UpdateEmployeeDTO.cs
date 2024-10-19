namespace Althaus_Warehouse.Models.DTO.EmployeeDTOs
{
    public class UpdateEmployeeDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeType { get; set; } 
        public bool IsActive { get; set; } 
        public string Email { get; set; } 
    }
}
