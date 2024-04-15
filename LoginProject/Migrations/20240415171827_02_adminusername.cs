using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginProject.Migrations
{
    /// <inheritdoc />
    public partial class _02_adminusername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "0cc7ff0e-dc24-4732-9692-edbd783be8b0", "ADMIN", "AQAAAAIAAYagAAAAEDRNsSxCBnsquPYFiTWY/GzCg3AS9YQmGNu3ZTv2LPvHVdY64H+RbqMHunTA3G31eg==", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "b4465148-ca25-4983-a75f-60dcdc3e489c", "ADMIN@LOCAL.SLHN.CZ", "AQAAAAIAAYagAAAAEOPp6SS6vjuinWJPTPz3KPYwWXK7H+iX0NGhFCTqfILPJPCiPmwykVsNlEE3TpCo7A==", "admin@local.slhn.cz" });
        }
    }
}
