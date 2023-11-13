using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KorsatkoApp.Migrations
{
    /// <inheritdoc />
    public partial class nullableenrollnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 123,
                column: "AddedOn",
                value: new DateTime(2023, 11, 11, 14, 40, 51, 400, DateTimeKind.Local).AddTicks(6587));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 124,
                column: "AddedOn",
                value: new DateTime(2023, 11, 11, 14, 40, 51, 400, DateTimeKind.Local).AddTicks(6591));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 123,
                column: "AddedOn",
                value: new DateTime(2023, 11, 11, 14, 40, 12, 594, DateTimeKind.Local).AddTicks(6092));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 124,
                column: "AddedOn",
                value: new DateTime(2023, 11, 11, 14, 40, 12, 594, DateTimeKind.Local).AddTicks(6096));
        }
    }
}
