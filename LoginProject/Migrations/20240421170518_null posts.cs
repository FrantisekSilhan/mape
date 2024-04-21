using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginProject.Migrations
{
    /// <inheritdoc />
    public partial class nullposts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c84ab07f-d876-4405-8132-6996b58e0e6d", "AQAAAAIAAYagAAAAENhM4iwcTQwYHzwPlp+6AH669C3VYmqtQLa2+8N1zcxmbPWi+CfHtvFhE2X0xHeROA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4a6f3533-4c1f-4e48-8e39-61b28c486229", "AQAAAAIAAYagAAAAEBwFIJrQBmPYOLMXOYDZVpU9Xy02ex6Y1y3WVY/0Db28QNGrJmFivmf2bIntYpYnzA==" });
        }
    }
}
