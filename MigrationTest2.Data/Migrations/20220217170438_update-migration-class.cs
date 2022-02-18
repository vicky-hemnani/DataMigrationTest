using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationTest2.Data.Migrations
{
    public partial class updatemigrationclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeTaken",
                table: "migrationModels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeTaken",
                table: "migrationModels",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
