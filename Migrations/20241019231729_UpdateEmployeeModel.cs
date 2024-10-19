using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Althaus_Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$kp9SPqZ/2yMQ.Q54pT.wBOWyC/C7Wib3sel1WCZ67Pbo1aei87PRy");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$Sbtr0q6cEAbTx55gv674SOSsBoTaNiSN4IqjhzUH/YL1/7slo7G7u");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$qaIOPqZLFZ0bYJY65BV88e2wAiFBQ/s4EF/OL16OK9cxSbgytMLMi");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$dtNpTK3h//WNUlx6wNAOG.Jw6pk8bbJsu.6Fg.NQ34PhcuNXs4EXG");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 20, 0, 17, 28, 797, DateTimeKind.Local).AddTicks(745));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 20, 0, 17, 28, 797, DateTimeKind.Local).AddTicks(869));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 20, 0, 17, 28, 797, DateTimeKind.Local).AddTicks(884));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 20, 0, 17, 28, 797, DateTimeKind.Local).AddTicks(911));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 20, 0, 17, 28, 797, DateTimeKind.Local).AddTicks(918));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 20, 0, 17, 28, 797, DateTimeKind.Local).AddTicks(931));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 20, 0, 17, 28, 797, DateTimeKind.Local).AddTicks(954));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 20, 0, 17, 28, 797, DateTimeKind.Local).AddTicks(961));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 20, 0, 17, 28, 797, DateTimeKind.Local).AddTicks(969));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 20, 0, 17, 28, 797, DateTimeKind.Local).AddTicks(977));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$o5uxf.x38RTqMJH.YYRw3O/JJclTJNYbFuYyZrgEomFFIthntNTcW");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$kDhZCpgwBGi/vdB55lW3n.BIK1aUJqjXcsEhAFwCAMJDiNagYDh9m");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$OsjN0/d92hxQWtojCYfmI.7GK.W/Cmh2iq2Nmwg7oNSr5TF7SvSaW");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$nlzD/zV4NOfxgdZREiQ/RO/LjxtyhYFMhCshJPab7s8B1bAJI0T2S");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4247));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4326));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4336));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4363));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4369));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4378));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4393));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4398));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4404));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 19, 23, 33, 22, 969, DateTimeKind.Local).AddTicks(4409));
        }
    }
}
