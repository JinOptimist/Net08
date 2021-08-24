using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMazeMvc.Migrations
{
    public partial class AddNewsUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "News");
        }
    }
}
