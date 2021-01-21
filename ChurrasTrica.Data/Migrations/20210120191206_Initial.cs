using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChurrasTrica.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Churras",
                columns: table => new
                {
                    ChurrasID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Observations = table.Column<string>(type: "TEXT", nullable: true, defaultValue: "Sem observações"),
                    WithDrink = table.Column<decimal>(type: "money", nullable: false),
                    WithoutDrink = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Churras", x => x.ChurrasID);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ContributionValue = table.Column<decimal>(type: "money", nullable: false),
                    IsPaid = table.Column<bool>(type: "INTEGER", nullable: false),
                    WithDrink = table.Column<bool>(type: "money", nullable: false),
                    WithoutDrink = table.Column<bool>(type: "money", nullable: false),
                    ChurrasID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Churras_ChurrasID",
                        column: x => x.ChurrasID,
                        principalTable: "Churras",
                        principalColumn: "ChurrasID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Login",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[] { 1, "queroChurras@trinca.com", "Thomaz", "trinca123" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChurrasID",
                table: "Users",
                column: "ChurrasID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Churras");
        }
    }
}
