using System;
using System.ComponentModel.DataAnnotations;

namespace Althaus_Warehouse.Models.Entities
{
    /// <summary>
    /// Entity class representing an employee in the warehouse
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Default no-args constructor for Employee
        /// </summary>
        public Employee() { }

        /// <summary>
        /// Constructor for creating an Employee with details
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <param name="firstName">First name of the employee</param>
        /// <param name="lastName">Last name of the employee</param>
        /// <param name="email">Employee email address</param>
        /// <param name="employeeType">Employee's type in the warehouse</param>
        public Employee(int id, string firstName, string lastName, string email, EmployeeType employeeType)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            EmployeeType = employeeType;
            DateHired = DateTime.Today;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the employee
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the employee
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Get or set the last name of the employee
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Get or set the email of the employee
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the employee's role current ones: (Manager, HR, Sales, Employee)
        /// </summary>
        [Required]
        public EmployeeType EmployeeType { get; set; }

        /// <summary>
        /// Gets or sets the date the employee was hired
        /// </summary>
        public DateTime DateHired { get; set; }

        /// <summary>
        /// Gets or sets the employee's status (active or inactive)
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Method to deactivate an employee
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }

        /// <summary>
        /// Method to activate an employee
        /// </summary>
        public void Activate()
        {
            IsActive = true;
        }

        /// <summary>
        /// Method to promote an employee to a new type
        /// </summary>
        /// <param name="updatedEmployeeType">The new type to assign to the employee</param>
        public void Promote(EmployeeType updatedEmployeeType)
        {
            EmployeeType = updatedEmployeeType;
        }

        /// <summary>
        /// Displays the full name of the employee
        /// </summary>
        /// <returns>Full name of the employee</returns>
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
