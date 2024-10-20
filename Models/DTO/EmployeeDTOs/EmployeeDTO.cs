namespace Althaus_Warehouse.Models.DTO.EmployeeDTOs
{
    /// <summary>
    /// DTO for reading employee information
    /// </summary>
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Ensure this property exists
        public DateTime HireDate { get; set; }
        public string? Role { get; set; } // This property will hold the string representation of EmployeeType
        public bool IsActive { get; set; }
        public string? Email { get; set; }
    }

}
