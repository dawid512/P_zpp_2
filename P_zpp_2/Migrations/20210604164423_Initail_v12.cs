using Microsoft.EntityFrameworkCore.Migrations;

namespace P_zpp_2.Migrations
{
    public partial class Initail_v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_company_AspNetUsers_UserId",
                table: "company");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "company",
                newName: "BossIdId");

            migrationBuilder.RenameIndex(
                name: "IX_company_UserId",
                table: "company",
                newName: "IX_company_BossIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_company_AspNetUsers_BossIdId",
                table: "company",
                column: "BossIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_company_AspNetUsers_BossIdId",
                table: "company");

            migrationBuilder.RenameColumn(
                name: "BossIdId",
                table: "company",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_company_BossIdId",
                table: "company",
                newName: "IX_company_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_company_AspNetUsers_UserId",
                table: "company",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
