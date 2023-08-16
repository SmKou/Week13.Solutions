using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MessageBoard.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MessageText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SentAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "Name" },
                values: new object[,]
                {
                    { 1, "Testing 1.2.3" },
                    { 2, "Starters" },
                    { 3, "Builders" },
                    { 4, "FAQ" }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageId", "GroupId", "MessageText", "SentAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Test tomorrow guys, gear up!!", new DateTime(2023, 8, 14, 20, 59, 14, 584, DateTimeKind.Local).AddTicks(2353), 2 },
                    { 2, 3, "DIY wood-cardboard shelves complete.", new DateTime(2023, 8, 14, 20, 59, 14, 584, DateTimeKind.Local).AddTicks(2410), 1 },
                    { 3, 3, "Awesome. How did the shelves turn out?", new DateTime(2023, 8, 14, 20, 59, 14, 584, DateTimeKind.Local).AddTicks(2412), 2 },
                    { 4, 1, "Why do we have so many tests!!", new DateTime(2023, 8, 15, 20, 59, 14, 584, DateTimeKind.Local).AddTicks(2415), 1 },
                    { 5, 2, "I've just started a wellness diet.", new DateTime(2023, 8, 15, 20, 59, 14, 584, DateTimeKind.Local).AddTicks(2417), 2 },
                    { 6, 3, "Hey Hey all! It's my new lego megatower!", new DateTime(2023, 8, 15, 20, 59, 14, 584, DateTimeKind.Local).AddTicks(2419), 3 },
                    { 7, 4, "What do I ask so I don't look dumb?", new DateTime(2023, 8, 15, 20, 59, 14, 584, DateTimeKind.Local).AddTicks(2422), 4 },
                    { 8, 4, "Questions like What do I ask, How would I...if I were to..., To what extent does... Before asking, identify the kind of answer you're looking for, what are you assuming, is it right, and google it. Then ask.", new DateTime(2023, 8, 15, 20, 59, 14, 584, DateTimeKind.Local).AddTicks(2424), 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name", "NormalizedUserName", "UserName" },
                values: new object[,]
                {
                    { 1, "Adam Zapel", "bob", "Bob" },
                    { 2, "Anna Conda", "long", "Long" },
                    { 3, "Crystal Ball", "crall", "Crall" },
                    { 4, "Dee Zaster", "deedeeha", "DeeDeeHa" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
