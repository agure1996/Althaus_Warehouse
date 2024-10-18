using Althaus_Warehouse.Models.Entities;

namespace Althaus_Warehouse.Models.DTO.EmployeeDTOs
{
    /// <summary>
    /// DTO for reading employee information
    /// </summary>
    public class GetEmployeeDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public DateTime DateHired { get; set; }

        public bool IsActive { get; set; }
    }
}
