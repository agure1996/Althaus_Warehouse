using System.ComponentModel.DataAnnotations;

namespace Althaus_Warehouse.Models.DTO.EmployeeDTOs
{
    /// <summary>
    /// DTO for creating a new employee
    /// </summary>
    public class CreateEmployeeDTO
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
        /// Plain text password to be hashed and saved for the employee
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
