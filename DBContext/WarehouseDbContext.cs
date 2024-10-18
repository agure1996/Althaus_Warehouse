using Althaus_Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Althaus_Warehouse.DBContext
{
    public class WarehouseDbContext : DbContext
    {
        /// <summary>
        /// This is the constructor for my database context.
        /// It sets up the options (like the connection string) that Entity Framework Core needs to use.
        /// </summary>
        /// <param name="options">Options passed by dependency injection, contains things like the connection string</param>
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) { }

        // This represents the Items table in the database.
        public DbSet<Item> Items { get; set; }

        // This represents the Employees table in the database.
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Override OnModelCreating to seed dummy data into the database
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder to configure entities</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some Employees into the data
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Jimmy",
                    LastName = "Jackson",
                    Email = "jim.jack@altwarehouse.com",
                    EmployeeType = EmployeeType.Manager,  // Using the enum value for employee types
                    DateHired = new DateTime(2020, 01, 15),
                    IsActive = true
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@altwarehouse.com",
                    EmployeeType = EmployeeType.HR,
                    DateHired = new DateTime(2021, 05, 20),
                    IsActive = true
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Emily",
                    LastName = "Johnson",
                    Email = "emily.johnson@altwarehouse.com",
                    EmployeeType = EmployeeType.Sales,
                    DateHired = new DateTime(2022, 07, 25),
                    IsActive = true
                },
                new Employee
                {
                    Id = 4,
                    FirstName = "Hamza",
                    LastName = "Adam",
                    Email = "hamza.adam@altwarehouse.com",
                    EmployeeType = EmployeeType.Employee,
                    DateHired = new DateTime(2021, 11, 02),
                    IsActive = true
                }
            );

            modelBuilder.Entity<Item>().HasData(
                    new Item
                    {
                        Id = 1,
                        Name = "Laptop",
                        Description = "15-inch laptop with 8GB RAM",
                        Quantity = 50,
                        Price = 599.99,
                        CreatedById = 1  // Created by John Doe (Manager)
                    },
                    new Item
                    {
                        Id = 2,
                        Name = "Monitor",
                        Description = "24-inch full HD monitor",
                        Quantity = 75,
                        Price = 149.99,
                        CreatedById = 2  // Created by Jane Smith (HR)
                    },
                    new Item
                    {
                        Id = 3,
                        Name = "Mouse",
                        Description = "Wireless mouse",
                        Quantity = 200,
                        Price = 19.99,
                        CreatedById = 3  // Created by Emily Johnson (Sales)
                    },
                    new Item
                    {
                        Id = 4,
                        Name = "Keyboard",
                        Description = "Mechanical keyboard with RGB lighting",
                        Quantity = 150,
                        Price = 89.99,
                        CreatedById = 3
                    },
                    new Item
                    {
                        Id = 5,
                        Name = "External Hard Drive",
                        Description = "1TB external hard drive with USB 3.0",
                        Quantity = 80,
                        Price = 79.99,
                        CreatedById = 1
                    },
                    new Item
                    {
                        Id = 6,
                        Name = "Webcam",
                        Description = "1080p HD webcam",
                        Quantity = 100,
                        Price = 39.99,
                        CreatedById = 2
                    },
                    new Item
                    {
                        Id = 7,
                        Name = "Printer",
                        Description = "Wireless all-in-one printer",
                        Quantity = 40,
                        Price = 129.99,
                        CreatedById = 1
                    },
                    new Item
                    {
                        Id = 8,
                        Name = "Smartphone",
                        Description = "5G smartphone with 128GB storage",
                        Quantity = 30,
                        Price = 799.99,
                        CreatedById = 3
                    },
                    new Item
                    {
                        Id = 9,
                        Name = "Headphones",
                        Description = "Noise-cancelling wireless headphones",
                        Quantity = 120,
                        Price = 199.99,
                        CreatedById = 2
                    },
                    new Item
                    {
                        Id = 10,
                        Name = "Office Chair",
                        Description = "Ergonomic office chair with lumbar support",
                        Quantity = 60,
                        Price = 249.99,
                        CreatedById = 1
                    }
                );

        }
    }
}
