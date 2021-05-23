using Microsoft.EntityFrameworkCore.Migrations;

namespace P_zpp_2.Migrations
{
    public partial class Initial_v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
             table: "AspNetRoles",
             columns: new[] { "Id", "Name", "NormalizedName" },
             values: new object[] { "1", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName" },
            values: new object[] { "2", "Koordynator", "KOORDYNATOR" });

            migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName" },
            values: new object[] { "3", "Pracownik", "PRACOWNIK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
