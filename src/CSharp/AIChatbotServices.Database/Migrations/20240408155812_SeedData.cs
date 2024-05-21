using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIChatbotServices.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserChatBotEntity");

            migrationBuilder.CreateTable(
                name: "UserWidgetEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ChatBotId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWidgetEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWidgetEntity_ChatBotEntity_ChatBotId",
                        column: x => x.ChatBotId,
                        principalTable: "ChatBotEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWidgetEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1L, "desc", "Ali" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Description", "Email", "Name", "Password", "Phone", "TenantId", "UserName" },
                values: new object[] { 1L, null, null, null, null, "40bd001563085fc35165329ea1ff5c5ecbdbbeef", null, 1L, "ali" });

            migrationBuilder.CreateIndex(
                name: "IX_UserWidgetEntity_ChatBotId",
                table: "UserWidgetEntity",
                column: "ChatBotId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWidgetEntity_UserId",
                table: "UserWidgetEntity",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWidgetEntity");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.CreateTable(
                name: "UserChatBotEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatBotId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatBotEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserChatBotEntity_ChatBotEntity_ChatBotId",
                        column: x => x.ChatBotId,
                        principalTable: "ChatBotEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChatBotEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserChatBotEntity_ChatBotId",
                table: "UserChatBotEntity",
                column: "ChatBotId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatBotEntity_UserId",
                table: "UserChatBotEntity",
                column: "UserId");
        }
    }
}
