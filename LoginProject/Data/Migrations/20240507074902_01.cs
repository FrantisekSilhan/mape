using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoginProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class _01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    EditedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AuthorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParentPostId = table.Column<Guid>(type: "TEXT", nullable: true),
                    RootPostId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Posts_ParentPostId",
                        column: x => x.ParentPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Posts_RootPostId",
                        column: x => x.RootPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, "admin", "ADMIN" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, "moderator", "MODERATOR" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0, "b21b4e8f-c3e0-4852-bd4a-b915deaebf3d", "admin@local.slhn.cz", true, "Administrator User", false, null, "ADMIN@LOCAL.SLHN.CZ", "ADMIN", "AQAAAAIAAYagAAAAEJXpl+3X8Ig+rdEdNj7anTNS+MIeyzqyS6lU7IFB2tvcrgkAqj9yoaMNtSOHF9mw2g==", null, false, "Asdfiasjfisda", false, "admin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 0, "463005b3-09b3-4565-9bf4-9f057bb32be2", "moderator@local.slhn.cz", true, "Moderator User", false, null, "moderator@LOCAL.SLHN.CZ", "MODERATOR", "AQAAAAIAAYagAAAAEHjqibWGPdMSlURbGEwG+rBP9DCHyhQTG27Gp7BqdeOS0dCU205vClLclirAGX6IKw==", null, false, "ioosdgodof", false, "moderator" },
                    { new Guid("7cb67635-0e88-447c-9997-3bec8323b902"), 0, "fc279626-a718-46da-8b46-c5bf5bf5c8be", "user@local.slhn.cz", true, "Basic User", false, null, "USER@LOCAL.SLHN.CZ", "USER", "AQAAAAIAAYagAAAAEIFoZkFB8FauwroCcqu1u6pRBhM6qLrmRBQZ9hyIQUBxU7rdNvi5vpj5WXljLOoDQQ==", null, false, "kjsdgjdgsg", false, "user" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "AuthorId", "Content", "CreatedAt", "EditedAt", "ParentPostId", "RootPostId" },
                values: new object[,]
                {
                    { new Guid("014292c4-1df5-4c50-9835-047475fd7d50"), new Guid("11111111-1111-1111-1111-111111111111"), "This is the first post by an admin", new DateTime(2024, 5, 7, 9, 49, 0, 851, DateTimeKind.Local).AddTicks(9902), null, null, null },
                    { new Guid("1027f597-5302-41b2-b756-52fa91ca6918"), new Guid("22222222-2222-2222-2222-222222222222"), "This is the first post by a moderator", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(3), null, null, null },
                    { new Guid("44a9c2d8-3473-4612-ba0a-0cccb9832430"), new Guid("11111111-1111-1111-1111-111111111111"), "Já jsem teď trochu v ráži, ale může mi někdo vysvětlit ten obrovskej rozdíl mezi čistou a hrubou mzdou? Jako za co reálně odvádim desetitisíce? Já mám pocit, že za to dostávám úplný h***.", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(496), null, null, null },
                    { new Guid("2db924b5-790f-4267-a5fe-1b46ff3de37a"), new Guid("7cb67635-0e88-447c-9997-3bec8323b902"), "This is a second reply to the first post by a moderator", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(297), null, new Guid("1027f597-5302-41b2-b756-52fa91ca6918"), new Guid("1027f597-5302-41b2-b756-52fa91ca6918") },
                    { new Guid("3be07143-ec74-4073-91c1-abc418f77866"), new Guid("22222222-2222-2222-2222-222222222222"), "Použij google.", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(822), null, new Guid("44a9c2d8-3473-4612-ba0a-0cccb9832430"), new Guid("44a9c2d8-3473-4612-ba0a-0cccb9832430") },
                    { new Guid("4dedceb0-2506-423a-9c76-bc098adfcab5"), new Guid("7cb67635-0e88-447c-9997-3bec8323b902"), "Zajímavé jak takovýto příspěvek vyvolává další a další negativní reakce.\r\nJe to pochopitelné. Ideál, který by navíc vyhovoval všem, neexistuje.😉\r\nVždy je co zlepšovat.\r\nNicméně všem stěžovatelům bych vždy doporučil aby se alespoň porozhlédli a srovnali si stav věcí u nás a v jiných zemích.\r\nZřejmě by nakonec byli docela překvapení, jaká může být realita.\r\nTím neříkám, že mi nic nevadí, ale planě nadávat nikam prostě nevede.\r\nA neštěstí je, že většina stěžovatelů potom navíc věří populistům.🤷", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(530), null, new Guid("44a9c2d8-3473-4612-ba0a-0cccb9832430"), new Guid("44a9c2d8-3473-4612-ba0a-0cccb9832430") },
                    { new Guid("60aa66bc-eb2f-4414-a79f-18910924ca87"), new Guid("7cb67635-0e88-447c-9997-3bec8323b902"), "This is a first reply to the first post by an admin", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(84), null, new Guid("014292c4-1df5-4c50-9835-047475fd7d50"), new Guid("014292c4-1df5-4c50-9835-047475fd7d50") },
                    { new Guid("6da98fcb-cadf-40c0-b074-fdb0925c3520"), new Guid("7cb67635-0e88-447c-9997-3bec8323b902"), "This is a second reply to the first post by an admin", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(128), null, new Guid("014292c4-1df5-4c50-9835-047475fd7d50"), new Guid("014292c4-1df5-4c50-9835-047475fd7d50") },
                    { new Guid("db83e26f-5b9d-4a8a-9df5-c8dd196b6097"), new Guid("7cb67635-0e88-447c-9997-3bec8323b902"), "This is a first reply to the first post by a moderator", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(192), null, new Guid("1027f597-5302-41b2-b756-52fa91ca6918"), new Guid("1027f597-5302-41b2-b756-52fa91ca6918") },
                    { new Guid("9348323b-890c-4eec-86f5-f0b153ca4cd3"), new Guid("11111111-1111-1111-1111-111111111111"), "Já určitě nejsem za na všechno nadávat, na druhou stranu, tohle je “můj” prostor, kde si s dávkou nadsázky můžu ulevit a nevidim důvod proč ne.☺️", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(565), null, new Guid("4dedceb0-2506-423a-9c76-bc098adfcab5"), new Guid("44a9c2d8-3473-4612-ba0a-0cccb9832430") },
                    { new Guid("b62725ad-68a7-4629-ab2f-e2cd3902389a"), new Guid("11111111-1111-1111-1111-111111111111"), "Zkusila jsem, nepomohlo", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(861), null, new Guid("3be07143-ec74-4073-91c1-abc418f77866"), new Guid("44a9c2d8-3473-4612-ba0a-0cccb9832430") },
                    { new Guid("df5473dd-f1d7-438d-a55f-780d30789165"), new Guid("7cb67635-0e88-447c-9997-3bec8323b902"), "This is a reply to the first reply to the first post by an admin", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(412), null, new Guid("60aa66bc-eb2f-4414-a79f-18910924ca87"), new Guid("014292c4-1df5-4c50-9835-047475fd7d50") },
                    { new Guid("21f4548e-ce50-4f88-b04e-df954232b2ee"), new Guid("7cb67635-0e88-447c-9997-3bec8323b902"), "Nic proti.\r\nMá poznámka byla k tomu, jaké reakce to následně vyvolává.\r\nNic míň, nic víc.\r\nAť se ti daří.😉", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(603), null, new Guid("9348323b-890c-4eec-86f5-f0b153ca4cd3"), new Guid("44a9c2d8-3473-4612-ba0a-0cccb9832430") },
                    { new Guid("3bee7ecf-bf4a-4dcf-ab88-5df3f7b878da"), new Guid("22222222-2222-2222-2222-222222222222"), "T*l píšou ti tu boti 🤣🤣🤣🤣", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(782), null, new Guid("9348323b-890c-4eec-86f5-f0b153ca4cd3"), new Guid("44a9c2d8-3473-4612-ba0a-0cccb9832430") },
                    { new Guid("abc7cfd3-9bb7-44ed-b2eb-559b33e4243e"), new Guid("11111111-1111-1111-1111-111111111111"), "Já to ani neberu a nemyslim nijak zle. Ale nemyslím si, že těch 5 lidí, kteří reagovali je nějaké šíření negativity. ☺️", new DateTime(2024, 5, 7, 9, 49, 0, 852, DateTimeKind.Local).AddTicks(642), null, new Guid("21f4548e-ce50-4f88-b04e-df954232b2ee"), new Guid("44a9c2d8-3473-4612-ba0a-0cccb9832430") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ParentPostId",
                table: "Posts",
                column: "ParentPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_RootPostId",
                table: "Posts",
                column: "RootPostId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
