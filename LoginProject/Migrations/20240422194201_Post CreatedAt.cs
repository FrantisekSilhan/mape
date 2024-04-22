using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginProject.Migrations
{
    /// <inheritdoc />
    public partial class PostCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("98cbe636-2487-4a92-9a0c-58cff2153a3d"));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "Posts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("63fd0f58-233c-46c4-80ba-b488a7ae0fa6"), null, "moderator", "MODERATOR" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0fc83a65-b125-4566-81af-7b7e35fda6a8", "AQAAAAIAAYagAAAAEOcT9FEDVOxSRy53Bp5OfIs7JX0i1LtiuOo8SFdr0Kuzylt0EWvF7JJTx3gErXmDQg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63fd0f58-233c-46c4-80ba-b488a7ae0fa6"));

            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "Posts");

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
    }
}
