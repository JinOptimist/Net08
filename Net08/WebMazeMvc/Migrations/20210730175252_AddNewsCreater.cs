using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMazeMvc.Migrations
{
    public partial class AddNewsCreater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreaaterId",
                table: "News",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_CreaaterId",
                table: "News",
                column: "CreaaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Users_CreaaterId",
                table: "News",
                column: "CreaaterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Users_CreaaterId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_CreaaterId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CreaaterId",
                table: "News");
        }
    }
}
