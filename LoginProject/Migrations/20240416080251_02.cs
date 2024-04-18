using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginProject.Migrations {
    /// <inheritdoc />
    public partial class _02 : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_ParentPostId",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "49e90ed5-edce-41fc-b2a3-449b08d740f9", "AQAAAAIAAYagAAAAEHgH4PfNCtw6neBAe8oMZ0Sk29+gOmXlF62lAWABGNSDL2sn/DFkpkpK8DberxYncA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_ParentPostId",
                table: "Posts",
                column: "ParentPostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_ParentPostId",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "13eb2a59-925b-4e57-a856-9bc78ed0ce00", "AQAAAAIAAYagAAAAEPJata0VDSRc0Sjio1rSc2n/RV5VWzZTotUyx6AWVKKiGvMv8SegwWmf6WJIXxkEcw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_ParentPostId",
                table: "Posts",
                column: "ParentPostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
