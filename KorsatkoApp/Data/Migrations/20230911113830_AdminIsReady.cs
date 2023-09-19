using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KorsatkoApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdminIsReady : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("INSERT INTO AspNetUserRoles (UserId, RoleId) SELECT 'b109156e-6974-4206-a1d7-825325f5dcbc', Id FROM AspNetRoles");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DELETE FROM AspNetUserRoles WHERE UserId = 'b109156e-6974-4206-a1d7-825325f5dcbc'");
		}
    }
}
