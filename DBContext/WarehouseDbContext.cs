using Althaus_Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Althaus_Warehouse.DBContext
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding Employees into the database
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Jimmy",
                    LastName = "Jackson",
                    Email = "jim.jack@altwarehouse.com",
                    EmployeeType = EmployeeType.Manager,
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

            // Seeding ItemTypes into the database
            modelBuilder.Entity<ItemType>().HasData(
                new ItemType { Id = 1, Name = "Dairy", Description = "Perishable dairy products." },
                new ItemType { Id = 2, Name = "Meat", Description = "Fresh meat products." },
                new ItemType { Id = 3, Name = "Seafood", Description = "Various seafood items." },
                new ItemType { Id = 4, Name = "Fruits", Description = "Fresh fruits." },
                new ItemType { Id = 5, Name = "Vegetables", Description = "Fresh vegetables." },
                new ItemType { Id = 6, Name = "Beverages", Description = "Various drinks." },
                new ItemType { Id = 7, Name = "Electronics", Description = "Electronic devices." },
                new ItemType { Id = 8, Name = "Furniture", Description = "Furniture items." },
                new ItemType { Id = 9, Name = "Clothing", Description = "Clothing and apparel." },
                new ItemType { Id = 10, Name = "Toys", Description = "Children's toys." },
                new ItemType { Id = 11, Name = "Stationery", Description = "Office supplies." },
                new ItemType { Id = 12, Name = "Books", Description = "Various books." },
                new ItemType { Id = 13, Name = "Tools", Description = "Hand tools and equipment." },
                new ItemType { Id = 14, Name = "Cleaning Supplies", Description = "Cleaning products." },
                new ItemType { Id = 15, Name = "Personal Care", Description = "Personal care items." },
                new ItemType { Id = 16, Name = "Household Appliances", Description = "Appliances for home use." },
                new ItemType { Id = 17, Name = "Cosmetics", Description = "Beauty and cosmetic products." },
                new ItemType { Id = 18, Name = "Grocery", Description = "Grocery items." },
                new ItemType { Id = 19, Name = "Snacks", Description = "Snack foods." },
                new ItemType { Id = 20, Name = "Baking Supplies", Description = "Ingredients for baking." },
                new ItemType { Id = 21, Name = "Spices", Description = "Cooking spices." },
                new ItemType { Id = 22, Name = "Grains", Description = "Various grains." },
                new ItemType { Id = 23, Name = "Office Supplies", Description = "Supplies for office use." },
                new ItemType { Id = 24, Name = "Computers", Description = "Computers and accessories." },
                new ItemType { Id = 25, Name = "Monitors", Description = "Monitors and screens." },
                new ItemType { Id = 26, Name = "Sports Equipment", Description = "Equipment for sports." },
                new ItemType { Id = 27, Name = "Automotive", Description = "Automotive parts and supplies." },
                new ItemType { Id = 28, Name = "Health and Wellness", Description = "Health products." },
                new ItemType { Id = 29, Name = "Jewelry", Description = "Jewelry items." },
                new ItemType { Id = 30, Name = "Footwear", Description = "Shoes and footwear." }
            );

            // Seed some Items into the database with the new ItemType foreign key and DateCreated
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    Name = "Laptop",
                    Description = "15-inch laptop with 8GB RAM",
                    Quantity = 50,
                    Price = 599.99,
                    CreatedById = 1,
                    ItemTypeId = 24,
                    DateCreated = DateTime.Now // Store as DateTime
                },
                new Item
                {
                    Id = 2,
                    Name = "Office Chair",
                    Description = "Ergonomic office chair with lumbar support",
                    Quantity = 60,
                    Price = 249.99,
                    CreatedById = 2,
                    ItemTypeId = 8,
                    DateCreated = DateTime.Now
                },
                new Item
                {
                    Id = 3,
                    Name = "Mouse",
                    Description = "Wireless mouse",
                    Quantity = 200,
                    Price = 19.99,
                    CreatedById = 3,
                    ItemTypeId = 11,
                    DateCreated = DateTime.Now
                },
                new Item
                {
                    Id = 4,
                    Name = "Keyboard",
                    Description = "Mechanical keyboard with RGB lighting",
                    Quantity = 150,
                    Price = 89.99,
                    CreatedById = 3,
                    ItemTypeId = 11,
                    DateCreated = DateTime.Now
                },
                new Item
                {
                    Id = 5,
                    Name = "Milk",
                    Description = "Fresh dairy milk",
                    Quantity = 80,
                    Price = 1.99,
                    CreatedById = 1,
                    ItemTypeId = 1,
                    DateCreated = DateTime.Now
                },
                new Item
                {
                    Id = 6,
                    Name = "Chicken Breast",
                    Description = "Organic chicken breast",
                    Quantity = 100,
                    Price = 5.99,
                    CreatedById = 2,
                    ItemTypeId = 2,
                    DateCreated = DateTime.Now
                },
                new Item
                {
                    Id = 7,
                    Name = "Smartphone",
                    Description = "5G smartphone with 128GB storage",
                    Quantity = 30,
                    Price = 799.99,
                    CreatedById = 1,
                    ItemTypeId = 7,
                    DateCreated = DateTime.Now
                },
                new Item
                {
                    Id = 8,
                    Name = "Running Shoes",
                    Description = "Lightweight running shoes",
                    Quantity = 50,
                    Price = 59.99,
                    CreatedById = 3,
                    ItemTypeId = 30,
                    DateCreated = DateTime.Now
                },
                new Item
                {
                    Id = 9,
                    Name = "Printer",
                    Description = "Wireless all-in-one printer",
                    Quantity = 40,
                    Price = 129.99,
                    CreatedById = 2,
                    ItemTypeId = 11,
                    DateCreated = DateTime.Now
                },
                new Item
                {
                    Id = 10,
                    Name = "Book: C# Programming",
                    Description = "A comprehensive guide to C# programming",
                    Quantity = 100,
                    Price = 39.99,
                    CreatedById = 1,
                    ItemTypeId = 12,
                    DateCreated = DateTime.Now
                }
            );
        }
    }
}
