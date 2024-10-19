using Althaus_Warehouse.Models.Entities;
using System;

namespace Althaus_Warehouse.Models.Interfaces
{
    /// <summary>
    /// Interface representing an employee
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// Unique identifier for the employee
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// First name of the employee
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Last name of the employee
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Email address of the employee
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Hashed password of the employee (used for authentication)
        /// </summary>
        string PasswordHash { get; set; }

        /// <summary>
        /// Role of the employee (e.g., Staff, Manager, Admin)
        /// </summary>
        EmployeeType EmployeeType { get; set; }

        /// <summary>
        /// Date when the employee was hired
        /// </summary>
        DateTime DateHired { get; set; }

        /// <summary>
        /// Determines if the employee is active or not
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Gets the full name of the employee
        /// </summary>
        /// <returns>The full name as a string</returns>
        string GetFullName();

        /// <summary>
        /// Promotes the employee to a new role
        /// </summary>
        /// <param name="newRole">The new role (EmployeeType) to promote the employee to</param>
        void Promote(EmployeeType newRole);

        /// <summary>
        /// Deactivates the employee, marking them as inactive
        /// </summary>
        void Deactivate();

        /// <summary>
        /// Activates the employee, marking them as active
        /// </summary>
        void Activate();
    }
}
