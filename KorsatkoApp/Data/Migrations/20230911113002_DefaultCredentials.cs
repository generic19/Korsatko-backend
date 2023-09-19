using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KorsatkoApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class DefaultCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [AddedOn], [DateOfBirth], [FullName], [Gender], [NationalId]) VALUES (N'b109156e-6974-4206-a1d7-825325f5dcbc', N'admin@korsatko.com', N'ADMIN@KORSATKO.COM', N'admin@korsatko.com', N'ADMIN@KORSATKO.COM', 0, N'AQAAAAIAAYagAAAAECNKgtzAWVQV6iAmfBXJ7Ayr9Q182joB/Gdx/55z9L5B+DXhJhJNotirUuQO+OhH3w==', N'GNF7HKAXY3ZDIQ7CR46OI2S66QALF4RO', N'1f40957a-29b4-4404-8563-dbf57655a869', N'01012345678', 0, 0, NULL, 1, 0, N'2023-09-11 11:25:44', N'0001-01-01 00:00:00', N'Admin', N'm', N'01234567891122')");

		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetUsers WHERE Id = 'b109156e-6974-4206-a1d7-825325f5dcbc'");
        }

	}
}
