using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMazeMvc.Migrations
{
    public partial class addedrelationbankcardbank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BankIssuingId",
                table: "BankCards",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankCards_BankIssuingId",
                table: "BankCards",
                column: "BankIssuingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankCards_Banks_BankIssuingId",
                table: "BankCards",
                column: "BankIssuingId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankCards_Banks_BankIssuingId",
                table: "BankCards");

            migrationBuilder.DropIndex(
                name: "IX_BankCards_BankIssuingId",
                table: "BankCards");

            migrationBuilder.DropColumn(
                name: "BankIssuingId",
                table: "BankCards");
        }
    }
}
