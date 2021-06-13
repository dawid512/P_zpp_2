using Microsoft.EntityFrameworkCore.Migrations;

namespace P_zpp_2.Migrations
{
    public partial class MigrationNew2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departures_AspNetUsers_User_idId",
                table: "departures");

            migrationBuilder.DropIndex(
                name: "IX_departures_User_idId",
                table: "departures");

            migrationBuilder.DropColumn(
                name: "User_idId",
                table: "departures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_departures_AspNetUsers_User_idId",
                table: "departures",
                column: "User_idId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
