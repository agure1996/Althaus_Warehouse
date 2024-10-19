using Althaus_Warehouse.Models.Entities;

namespace Althaus_Warehouse.Models.DTO.EmployeeDTOs
{
    /// <summary>
    /// DTO for reading employee information
    /// </summary>
    public class GetEmployeeDTO
    {
        /// <summary>
        /// Unique identifier for the employee
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name of the employee
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the employee
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email address of the employee
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Role of the employee (e.g., Staff, Manager, Admin)
        /// </summary>
        public EmployeeType EmployeeType { get; set; }

        /// <summary>
        /// Date when the employee was hired
        /// </summary>
        public DateTime DateHired { get; set; }

        /// <summary>
        /// Determines if the employee is currently active
        /// </summary>
        public bool IsActive { get; set; }
    }
}
