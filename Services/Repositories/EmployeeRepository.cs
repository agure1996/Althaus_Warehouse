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

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync() =>
            await _context.Employees.ToListAsync();

        public async Task<Employee?> GetEmployeeByIdAsync(int id) =>
            await _context.Employees.FindAsync(id);

        public async Task<Employee?> GetEmployeeByEmailAsync(string email) =>
            await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);

        public async Task AddEmployeeAsync(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            await _context.Employees.AddAsync(employee);
            await SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            _context.Employees.Update(employee);
            await SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> EmployeeExistsAsync(int id) =>
            await _context.Employees.AnyAsync(e => e.Id == id);

        public async Task<bool> ValidateEmployeeCredentialsAsync(string email, string password)
        {
            var employee = await GetEmployeeByEmailAsync(email);
            if (employee == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, employee.PasswordHash); // Verify the password using BCrypt
        }

        public async Task<bool> SaveChangesAsync() =>
            (await _context.SaveChangesAsync()) >= 0;
    }
}
