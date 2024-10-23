using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Althaus_Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class Updateitempricetags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$4UDvFzYCtE.rCebgzlCIZeEjLhvV43ASh.fgMsMsm.1hNuB14zEPa");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$ttlaiTI1O5uvOoQz/d4vXOmQdti9k4ido1Rzoj4Kqcj762keSWQem");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$uIUe3mxbugvk92fj5IYc5ekU.k3ho9e0C0Ug6QugjaWRSO7tm8nAa");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$mxKVEEsq8t77RlzkEa1s9.JJoNOD21zNn/W3.OhXFA5Tj6zGMtEjm");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 12, 15, 15, 769, DateTimeKind.Local).AddTicks(6540));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 12, 15, 15, 769, DateTimeKind.Local).AddTicks(6626));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 12, 15, 15, 769, DateTimeKind.Local).AddTicks(6642));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 12, 15, 15, 769, DateTimeKind.Local).AddTicks(6683));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 12, 15, 15, 769, DateTimeKind.Local).AddTicks(6692));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 12, 15, 15, 769, DateTimeKind.Local).AddTicks(6707));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 12, 15, 15, 769, DateTimeKind.Local).AddTicks(6726));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 12, 15, 15, 769, DateTimeKind.Local).AddTicks(6735));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 12, 15, 15, 769, DateTimeKind.Local).AddTicks(6744));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 12, 15, 15, 769, DateTimeKind.Local).AddTicks(6753));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$KHHKZYSHkS.uTvlP8R6iBOwakLgGldzGj5/WUhDbBser6jPFJghNW");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$AAoJ.zs/aYP/SfLG9wsuDuZaQuwZJqy7mysbYloVZt7rDvroLBQaW");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$iSoZ1UykYaHhpnH5PevsH.XOfyMEfBMiLE1jpp6ZnDrGW.WFGMDqO");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$8WO22lPkt8J/ABD2bbb72uEa/iw8tQXf.m1BnzuNLdPwPLrnPBkQC");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 11, 37, 33, 624, DateTimeKind.Local).AddTicks(5429));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 11, 37, 33, 624, DateTimeKind.Local).AddTicks(5558));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 11, 37, 33, 624, DateTimeKind.Local).AddTicks(5574));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 11, 37, 33, 624, DateTimeKind.Local).AddTicks(5600));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 11, 37, 33, 624, DateTimeKind.Local).AddTicks(5609));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 11, 37, 33, 624, DateTimeKind.Local).AddTicks(5624));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 11, 37, 33, 624, DateTimeKind.Local).AddTicks(5641));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 11, 37, 33, 624, DateTimeKind.Local).AddTicks(5649));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 11, 37, 33, 624, DateTimeKind.Local).AddTicks(5656));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 23, 11, 37, 33, 624, DateTimeKind.Local).AddTicks(5664));
        }
    }
}
