using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Althaus_Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "Items",
                newName: "ItemTypeId");

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

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "ItemTypeId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "ItemTypeId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "ItemTypeId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "ItemTypeId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "ItemTypeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "ItemTypeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "ItemTypeId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "ItemTypeId",
                value: 30);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "ItemTypeId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "ItemTypeId",
                value: 12);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items",
                column: "ItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemTypes_ItemTypeId",
                table: "Items",
                column: "ItemTypeId",
                principalTable: "ItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemTypes_ItemTypeId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ItemTypeId",
                table: "Items",
                newName: "ItemType");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "ItemType",
                value: 23);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "ItemType",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "ItemType",
                value: 22);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "ItemType",
                value: 22);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "ItemType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "ItemType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "ItemType",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "ItemType",
                value: 29);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "ItemType",
                value: 22);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "ItemType",
                value: 11);
        }
    }
}
