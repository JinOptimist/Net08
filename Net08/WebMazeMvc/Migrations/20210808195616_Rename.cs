using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMazeMvc.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreUser_Genres_GenresId",
                table: "GenreUser");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "GenreUser",
                newName: "FavoriteGenresId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreUser_Genres_FavoriteGenresId",
                table: "GenreUser",
                column: "FavoriteGenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreUser_Genres_FavoriteGenresId",
                table: "GenreUser");

            migrationBuilder.RenameColumn(
                name: "FavoriteGenresId",
                table: "GenreUser",
                newName: "GenresId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreUser_Genres_GenresId",
                table: "GenreUser",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
