namespace Althaus_Warehouse.Models.DTO.EmployeeDTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime HireDate { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
    }
}
