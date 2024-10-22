using System.ComponentModel.DataAnnotations;

namespace Althaus_Warehouse.Models.DTO.EmployeeDTOs
{
    /// <summary>
    /// DTO for editing an existing employee
    /// </summary>
    public class EditEmployeeDTO
    {
        /// <summary>
        /// Unique identifier for the employee
        /// </summary>
        public int Id { get; set; } 

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
        /// Role of the employee (e.g., Staff, Manager, Admin) - Accepts string for Enum conversion
        /// </summary>
        [Required]
        public string? EmployeeType { get; set; }

        /// <summary>
        /// Email address of the employee
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        /// <summary>
        /// Optional plain text password for the employee (for updating password)
        /// </summary>
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
