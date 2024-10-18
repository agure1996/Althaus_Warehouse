using Althaus_Warehouse.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Althaus_Warehouse.Models.DTO.EmployeeDTOs
{
    /// <summary>
    /// DTO for updating an existing employee
    /// </summary>
    public class UpdateEmployeeDTO
    {
        [Required]
        public int Id { get; set; }

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

        public bool IsActive { get; set; }
    }

}
