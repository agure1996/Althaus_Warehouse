using Althaus_Warehouse.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Althaus_Warehouse.Models.DTO.EmployeeDTOs
{
    /// <summary>
    /// DTO for creating a new employee
    /// </summary>
    public class CreateEmployeeDTO
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public EmployeeType EmployeeType { get; set; }
    }

}
