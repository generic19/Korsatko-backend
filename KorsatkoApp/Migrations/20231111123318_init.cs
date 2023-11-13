using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KorsatkoApp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 123,
                column: "AddedOn",
                value: new DateTime(2023, 11, 11, 14, 33, 18, 405, DateTimeKind.Local).AddTicks(5615));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 124,
                column: "AddedOn",
                value: new DateTime(2023, 11, 11, 14, 33, 18, 405, DateTimeKind.Local).AddTicks(5619));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 123,
                column: "AddedOn",
                value: new DateTime(2023, 11, 11, 14, 32, 23, 412, DateTimeKind.Local).AddTicks(2530));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 124,
                column: "AddedOn",
                value: new DateTime(2023, 11, 11, 14, 32, 23, 412, DateTimeKind.Local).AddTicks(2534));
        }
    }
}
