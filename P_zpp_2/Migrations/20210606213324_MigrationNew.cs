using Microsoft.EntityFrameworkCore.Migrations;

namespace P_zpp_2.Migrations
{
    public partial class MigrationNew : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "User_idId",
                table: "departures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_departures_User_idId",
                table: "departures",
                column: "User_idId");

            migrationBuilder.AddForeignKey(
                name: "FK_company_AspNetUsers_BossIdId",
                table: "company",
                column: "BossIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_departures_AspNetUsers_User_idId",
                table: "departures",
                column: "User_idId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_company_AspNetUsers_BossIdId",
                table: "company");

            migrationBuilder.DropForeignKey(
                name: "FK_departures_AspNetUsers_User_idId",
                table: "departures");

            migrationBuilder.DropIndex(
                name: "IX_departures_User_idId",
                table: "departures");

            migrationBuilder.DropColumn(
                name: "User_idId",
                table: "departures");

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
