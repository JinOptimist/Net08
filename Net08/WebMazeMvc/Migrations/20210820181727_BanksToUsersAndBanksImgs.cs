using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMazeMvc.Migrations
{
    public partial class BanksToUsersAndBanksImgs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BankUser",
                columns: table => new
                {
                    ClientsId = table.Column<long>(type: "bigint", nullable: false),
                    MyBanksId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankUser", x => new { x.ClientsId, x.MyBanksId });
                    table.ForeignKey(
                        name: "FK_BankUser_Banks_MyBanksId",
                        column: x => x.MyBanksId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankUser_Users_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankUser_MyBanksId",
                table: "BankUser",
                column: "MyBanksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankUser");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Banks");
        }
    }
}
