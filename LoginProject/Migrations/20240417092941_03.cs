using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginProject.Migrations
{
    /// <inheritdoc />
    public partial class _03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RootPostId",
                table: "Posts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4a6f3533-4c1f-4e48-8e39-61b28c486229", "AQAAAAIAAYagAAAAEBwFIJrQBmPYOLMXOYDZVpU9Xy02ex6Y1y3WVY/0Db28QNGrJmFivmf2bIntYpYnzA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_RootPostId",
                table: "Posts",
                column: "RootPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_RootPostId",
                table: "Posts",
                column: "RootPostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_RootPostId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_RootPostId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "RootPostId",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "49e90ed5-edce-41fc-b2a3-449b08d740f9", "AQAAAAIAAYagAAAAEHgH4PfNCtw6neBAe8oMZ0Sk29+gOmXlF62lAWABGNSDL2sn/DFkpkpK8DberxYncA==" });
        }
    }
}
