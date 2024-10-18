using System;
using System.ComponentModel.DataAnnotations;

namespace Althaus_Warehouse.Models.DTO.EmployeeDTOs
{
    /// <summary>
    /// Data Transfer Object for Employee
    /// </summary>
    public class EmployeeDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the employee.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the role of the employee.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the hire date of the employee.
        /// </summary>
        public DateTime HireDate { get; set; }
    }
}
