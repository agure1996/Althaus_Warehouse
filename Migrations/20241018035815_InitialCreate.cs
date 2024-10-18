using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Althaus_Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmployeeType = table.Column<int>(type: "int", nullable: false),
                    DateHired = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Employees_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Employees",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateHired", "Email", "EmployeeType", "FirstName", "IsActive", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "jim.jack@altwarehouse.com", 1, "Jimmy", true, "Jackson" },
                    { 2, new DateTime(2021, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@altwarehouse.com", 2, "Jane", true, "Smith" },
                    { 3, new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.johnson@altwarehouse.com", 3, "Emily", true, "Johnson" },
                    { 4, new DateTime(2021, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "hamza.adam@altwarehouse.com", 4, "Hamza", true, "Adam" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedById", "DateCreated", "Description", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(1, 1, 1), "15-inch laptop with 8GB RAM", "Laptop", 599.99000000000001, 50 },
                    { 2, 2, new DateOnly(1, 1, 1), "24-inch full HD monitor", "Monitor", 149.99000000000001, 75 },
                    { 3, 3, new DateOnly(1, 1, 1), "Wireless mouse", "Mouse", 19.989999999999998, 200 },
                    { 4, 3, new DateOnly(1, 1, 1), "Mechanical keyboard with RGB lighting", "Keyboard", 89.989999999999995, 150 },
                    { 5, 1, new DateOnly(1, 1, 1), "1TB external hard drive with USB 3.0", "External Hard Drive", 79.989999999999995, 80 },
                    { 6, 2, new DateOnly(1, 1, 1), "1080p HD webcam", "Webcam", 39.990000000000002, 100 },
                    { 7, 1, new DateOnly(1, 1, 1), "Wireless all-in-one printer", "Printer", 129.99000000000001, 40 },
                    { 8, 3, new DateOnly(1, 1, 1), "5G smartphone with 128GB storage", "Smartphone", 799.99000000000001, 30 },
                    { 9, 2, new DateOnly(1, 1, 1), "Noise-cancelling wireless headphones", "Headphones", 199.99000000000001, 120 },
                    { 10, 1, new DateOnly(1, 1, 1), "Ergonomic office chair with lumbar support", "Office Chair", 249.99000000000001, 60 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatedById",
                table: "Items",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
