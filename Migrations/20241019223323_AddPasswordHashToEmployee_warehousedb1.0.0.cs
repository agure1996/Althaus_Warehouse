using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Althaus_Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashToEmployee_warehousedb100 : Migration
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
                    PasswordHash = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
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
                name: "ItemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
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
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ItemTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Employees_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateHired", "Email", "EmployeeType", "FirstName", "IsActive", "LastName", "PasswordHash" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "jim.jack@altwarehouse.com", 1, "Jimmy", true, "Jackson", "$2a$11$o5uxf.x38RTqMJH.YYRw3O/JJclTJNYbFuYyZrgEomFFIthntNTcW" },
                    { 2, new DateTime(2021, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@altwarehouse.com", 2, "Jane", true, "Smith", "$2a$11$kDhZCpgwBGi/vdB55lW3n.BIK1aUJqjXcsEhAFwCAMJDiNagYDh9m" },
                    { 3, new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.johnson@altwarehouse.com", 3, "Emily", true, "Johnson", "$2a$11$OsjN0/d92hxQWtojCYfmI.7GK.W/Cmh2iq2Nmwg7oNSr5TF7SvSaW" },
                    { 4, new DateTime(2021, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "hamza.adam@altwarehouse.com", 4, "Hamza", true, "Adam", "$2a$11$nlzD/zV4NOfxgdZREiQ/RO/LjxtyhYFMhCshJPab7s8B1bAJI0T2S" }
                });

            migrationBuilder.InsertData(
                table: "ItemTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Perishable dairy products.", "Dairy" },
                    { 2, "Fresh meat products.", "Meat" },
                    { 3, "Various seafood items.", "Seafood" },
                    { 4, "Fresh fruits.", "Fruits" },
                    { 5, "Fresh vegetables.", "Vegetables" },
                    { 6, "Various drinks.", "Beverages" },
                    { 7, "Electronic devices.", "Electronics" },
                    { 8, "Furniture items.", "Furniture" },
                    { 9, "Clothing and apparel.", "Clothing" },
                    { 10, "Children's toys.", "Toys" },
                    { 11, "Office supplies.", "Stationery" },
                    { 12, "Various books.", "Books" },
                    { 13, "Hand tools and equipment.", "Tools" },
                    { 14, "Cleaning products.", "Cleaning Supplies" },
                    { 15, "Personal care items.", "Personal Care" },
                    { 16, "Appliances for home use.", "Household Appliances" },
                    { 17, "Beauty and cosmetic products.", "Cosmetics" },
                    { 18, "Grocery items.", "Grocery" },
                    { 19, "Snack foods.", "Snacks" },
                    { 20, "Ingredients for baking.", "Baking Supplies" },
                    { 21, "Cooking spices.", "Spices" },
                    { 22, "Various grains.", "Grains" },
                    { 23, "Supplies for office use.", "Office Supplies" },
                    { 24, "Computers and accessories.", "Computers" },
                    { 25, "Monitors and screens.", "Monitors" },
                    { 26, "Equipment for sports.", "Sports Equipment" },
                    { 27, "Automotive parts and supplies.", "Automotive" },
                    { 28, "Health products.", "Health and Wellness" },
                    { 29, "Jewelry items.", "Jewelry" },
                    { 30, "Shoes and footwear.", "Footwear" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedById", "DateCreated", "Description", "ItemTypeId", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4247), "15-inch laptop with 8GB RAM", 24, "Laptop", 599.99000000000001, 50 },
                    { 2, 2, new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4326), "Ergonomic office chair with lumbar support", 8, "Office Chair", 249.99000000000001, 60 },
                    { 3, 3, new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4336), "Wireless mouse", 11, "Mouse", 19.989999999999998, 200 },
                    { 4, 3, new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4363), "Mechanical keyboard with RGB lighting", 11, "Keyboard", 89.989999999999995, 150 },
                    { 5, 1, new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4369), "Fresh dairy milk", 1, "Milk", 1.99, 80 },
                    { 6, 2, new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4378), "Organic chicken breast", 2, "Chicken Breast", 5.9900000000000002, 100 },
                    { 7, 1, new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4393), "5G smartphone with 128GB storage", 7, "Smartphone", 799.99000000000001, 30 },
                    { 8, 3, new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4398), "Lightweight running shoes", 30, "Running Shoes", 59.990000000000002, 50 },
                    { 9, 2, new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4404), "Wireless all-in-one printer", 11, "Printer", 129.99000000000001, 40 },
                    { 10, 1, new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4409), "A comprehensive guide to C# programming", 12, "Book: C# Programming", 39.990000000000002, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatedById",
                table: "Items",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items",
                column: "ItemTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ItemTypes");
        }
    }
}
