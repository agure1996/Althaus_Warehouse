using Althaus_Warehouse.Models.Entities;

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
        Task<Employee?> GetEmployeeByIdAsync(int id);

        /// <summary>
        /// Retrieves an employee by email address for authentication purposes.
        /// </summary>
        /// <param name="email">The email address of the employee.</param>
        /// <returns>The <see cref="Employee"/> object if found; otherwise, null.</returns>
        Task<Employee?> GetEmployeeByEmailAsync(string email);

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

        /// <summary>
        /// Checks if an employee exists in the warehouse.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>True if the employee exists; otherwise, false.</returns>
        Task<bool> EmployeeExistsAsync(int id);

        /// <summary>
        /// Validates an employee's credentials for login.
        /// </summary>
        /// <param name="email">The employee's email.</param>
        /// <param name="password">The provided password to validate.</param>
        /// <returns>True if the password is valid; otherwise, false.</returns>
        Task<bool> ValidateEmployeeCredentialsAsync(string email, string password);

        /// <summary>
        /// Saves changes made to the database asynchronously.
        /// </summary>
        /// <returns>True if the save operation succeeded; otherwise, false.</returns>
        Task<bool> SaveChangesAsync();
    }
}
