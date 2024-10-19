using Althaus_Warehouse.DBContext;
using Althaus_Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Althaus_Warehouse.Services.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly WarehouseDbContext _context;

        public EmployeeRepository(WarehouseDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// Retrieves all employees in the warehouse.
        /// </summary>
        /// <returns>A list of <see cref="Employee"/> objects.</returns>
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync() =>
            await _context.Employees.ToListAsync();

        /// <summary>
        /// Retrieves a specific employee by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>The <see cref="Employee"/> object if found; otherwise, null.</returns>
        public async Task<Employee?> GetEmployeeByIdAsync(int id) =>
            await _context.Employees.FindAsync(id);

        /// <summary>
        /// Adds a new employee to the warehouse.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> object to add.</param>
        public async Task AddEmployeeAsync(Employee employee)
        {
            // Validate input
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            await _context.Employees.AddAsync(employee);
            await SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing employee in the warehouse.
        /// </summary>
        /// <param name="employee">The <see cref="Employee"/> object with updated values.</param>
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            // Validate input
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            _context.Employees.Update(employee);
            await SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an employee from the warehouse by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await SaveChangesAsync();
            }
        }

        /// <summary>
        /// Checks if an employee exists in the warehouse.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>True if the employee exists; otherwise, false.</returns>
        public async Task<bool> EmployeeExistsAsync(int id) =>
            await _context.Employees.AnyAsync(e => e.Id == id);

        /// <summary>
        /// Saves changes made to the database asynchronously.
        /// </summary>
        /// <returns>True if the save operation succeeded; otherwise, false.</returns>
        public async Task<bool> SaveChangesAsync() =>
            (await _context.SaveChangesAsync()) > 0;
    }
}
