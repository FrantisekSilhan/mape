using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginProject.Migrations
{
    /// <inheritdoc />
    public partial class _04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("98cbe636-2487-4a92-9a0c-58cff2153a3d"), null, "moderator", "MODERATOR" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c3b5f9ae-e8a1-4cbe-8405-71473923ed1e", "AQAAAAIAAYagAAAAEDRHKCrmx1nrgZ/svGu9/Nr5jqJESEEPz7zUgnj9h0Pf4PiLwmkRicsAZylGKwmoFA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("98cbe636-2487-4a92-9a0c-58cff2153a3d"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c84ab07f-d876-4405-8132-6996b58e0e6d", "AQAAAAIAAYagAAAAENhM4iwcTQwYHzwPlp+6AH669C3VYmqtQLa2+8N1zcxmbPWi+CfHtvFhE2X0xHeROA==" });
        }
    }
}
