﻿using Althaus_Warehouse.Models.Entities;

namespace Althaus_Warehouse.Services.Repositories
{
    /// <summary>
    /// Interface for managing employee data in the warehouse.
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Retrieves all employees in the warehouse.
        /// </summary>
        /// <returns>A list of <see cref="Employee"/> objects.</returns>
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        /// <summary>
        /// Retrieves a specific employee by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>The <see cref="Employee"/> object if found; otherwise, null.</returns>
        Task<Employee?> GetEmployeeByIdAsync(int id); // Updated to return null if not found

        /// <summary>
        /// Adds a new employee to the warehouse.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> object to add.</param>
        Task AddEmployeeAsync(Employee employee);

        /// <summary>
        /// Updates an existing employee in the warehouse.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> object with updated values.</param>
        Task UpdateEmployeeAsync(Employee employee);

        /// <summary>
        /// Deletes an employee from the warehouse by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        Task DeleteEmployeeAsync(int id);
    }
}
