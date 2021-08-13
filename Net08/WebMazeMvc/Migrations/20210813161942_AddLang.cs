using Microsoft.EntityFrameworkCore.Migrations;
using WebMazeMvc.EfStuff.Model;

namespace WebMazeMvc.Migrations
{
    public partial class AddLang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Lang",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: (int)Lang.Eng);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lang",
                table: "Users");
        }
    }
}
