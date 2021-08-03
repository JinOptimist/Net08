using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMazeMvc.Migrations
{
    public partial class AddNewsForumCommentLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NewsId",
                table: "Forums",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Forums_NewsId",
                table: "Forums",
                column: "NewsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Forums_News_NewsId",
                table: "Forums",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forums_News_NewsId",
                table: "Forums");

            migrationBuilder.DropIndex(
                name: "IX_Forums_NewsId",
                table: "Forums");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "Forums");
        }
    }
}
