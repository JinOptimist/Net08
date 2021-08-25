using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMazeMvc.Migrations
{
    public partial class AddEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfEvent = table.Column<int>(type: "int", nullable: false),
                    TypeOfMonth = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    DayOfWeekForMonthEvent = table.Column<int>(type: "int", nullable: false),
                    DayOfMonth = table.Column<int>(type: "int", nullable: false),
                    NumberOfWeekOfMonth = table.Column<int>(type: "int", nullable: false),
                    NumberOfMonth = table.Column<int>(type: "int", nullable: false),
                    DayOfQuartal = table.Column<int>(type: "int", nullable: false),
                    EventText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfQuarter = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    PeriodOfDays = table.Column<int>(type: "int", nullable: false),
                    DateTimeOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfMonthForQuartal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_UserId",
                table: "Event",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");
        }
    }
}
