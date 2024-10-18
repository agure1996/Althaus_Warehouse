﻿using Althaus_Warehouse.DBContext;
using Althaus_Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Althaus_Warehouse.Services.Repositories
{

    /// <summary>
    /// Repository class for managing employee data in the warehouse.
    /// Implements <see cref="IEmployeeRepository"/>.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly WarehouseDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to use.</param>
        public EmployeeRepository(WarehouseDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            // Retrieve all employees from the database
            return await _context.Employees.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            // Retrieve a specific employee by its ID
            return await _context.Employees.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task AddEmployeeAsync(Employee employee)
        {
            // Add a new employee to the database
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            // Update an existing employee in the database
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteEmployeeAsync(int id)
        {
            // Find and delete an employee by its ID
            var employee = await GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
