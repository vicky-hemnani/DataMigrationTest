using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationTest2.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "migrationModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<int>(type: "int", nullable: false),
                    End = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeTaken = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_migrationModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sourceModels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstNumner = table.Column<int>(type: "int", nullable: false),
                    SecondNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sourceModels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "destinationModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_destinationModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_destinationModels_sourceModels_SourceId",
                        column: x => x.SourceId,
                        principalTable: "sourceModels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_destinationModels_SourceId",
                table: "destinationModels",
                column: "SourceId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "destinationModels");

            migrationBuilder.DropTable(
                name: "migrationModels");

            migrationBuilder.DropTable(
                name: "sourceModels");
        }
    }
}
