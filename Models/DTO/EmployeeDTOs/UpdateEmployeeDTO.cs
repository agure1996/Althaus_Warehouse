using Althaus_Warehouse.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Althaus_Warehouse.Models.DTO.EmployeeDTOs
{
    /// <summary>
    /// DTO for updating employee information
    /// </summary>
    public class UpdateEmployeeDTO
    {

        /// <summary>
        /// First name of the employee
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Last name of the employee
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }

        /// <summary>
        /// Role of the employee (e.g., Staff, Manager, Admin)
        /// </summary>
        [Required]
        public EmployeeType EmployeeType { get; set; }

        /// <summary>
        /// Determines if the employee is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Email address of the employee
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        /// <summary>
        /// Optional plain text password for updating the employee's password
        /// </summary>
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
