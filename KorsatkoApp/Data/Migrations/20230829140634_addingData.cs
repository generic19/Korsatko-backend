using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KorsatkoApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "AddedOn", "Description", "Name", "Picture", "Prerequisites", "Price" },
                values: new object[,]
                {
                    { 123, new DateTime(2023, 8, 29, 17, 6, 34, 84, DateTimeKind.Local).AddTicks(1778), "C++ Programming Language for Advanced Programmers", "Advanced C++", null, "C Course", 1500.0 },
                    { 124, new DateTime(2023, 8, 29, 17, 6, 34, 84, DateTimeKind.Local).AddTicks(1784), "C Programming Language", "C For Beginners", null, "None", 1200.0 }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "AddedOn", "Email", "ExperienceYears", "FullName", "Gender", "NationalId", "PhoneNumber", "Qualifications" },
                values: new object[] { 567, new DateTime(2023, 8, 29, 17, 6, 34, 84, DateTimeKind.Local).AddTicks(2045), "Aba@gmail.com", 8, "Abanob", "M", "321857012", "0123456789", "Doctor" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AddedOn", "BirthOfDate", "Email", "FullName", "Gender", "NationalId", "PhoneNumber", "UserName", "UserPassword" },
                values: new object[] { 789, new DateTime(2023, 8, 29, 17, 6, 34, 84, DateTimeKind.Local).AddTicks(2071), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adele@gmail.com", "Adele", "F", "32185776582", "012987654", "CoolAdele", "a123" });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "AddedOn", "CourseId", "EndDate", "EndTime", "InstructorId", "IsAvailable", "Limit", "Location", "PriceRate", "StartDate", "startTime" },
                values: new object[] { 456, new DateTime(2023, 8, 29, 17, 6, 34, 84, DateTimeKind.Local).AddTicks(2091), 123, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 567, true, 30, "Online", 1f, new DateTime(2023, 8, 29, 14, 6, 34, 84, DateTimeKind.Utc).AddTicks(2094), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 456);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 789);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 567);
        }
    }
}
