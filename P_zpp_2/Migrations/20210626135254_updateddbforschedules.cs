using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P_zpp_2.Migrations
{
    public partial class updateddbforschedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "scheduleName",
                table: "schedules",
                newName: "ScheduleName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "schedules",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "jsonfilewithschedule_staff_locaton",
                table: "schedules",
                newName: "ScheduleInstructions");

            migrationBuilder.RenameColumn(
                name: "jsonfilewithschedule_locaton",
                table: "schedules",
                newName: "ScheduleInJSON");

            migrationBuilder.AddColumn<string>(
                name: "CoordinatorId",
                table: "schedules",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HangingDaysInJSON",
                table: "schedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastScheduleDay",
                table: "schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LeaveDayRange",
                table: "leaves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialInfo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScheduleInstructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListOfShistsInJSON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordinatorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleInstructions_AspNetUsers_CoordinatorId",
                        column: x => x.CoordinatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_schedules_CoordinatorId",
                table: "schedules",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleInstructions_CoordinatorId",
                table: "ScheduleInstructions",
                column: "CoordinatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_AspNetUsers_CoordinatorId",
                table: "schedules",
                column: "CoordinatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedules_AspNetUsers_CoordinatorId",
                table: "schedules");

            migrationBuilder.DropTable(
                name: "ScheduleInstructions");

            migrationBuilder.DropIndex(
                name: "IX_schedules_CoordinatorId",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "CoordinatorId",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "HangingDaysInJSON",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "LastScheduleDay",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "LeaveDayRange",
                table: "leaves");

            migrationBuilder.DropColumn(
                name: "SpecialInfo",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ScheduleName",
                table: "schedules",
                newName: "scheduleName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "schedules",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ScheduleInstructions",
                table: "schedules",
                newName: "jsonfilewithschedule_staff_locaton");

            migrationBuilder.RenameColumn(
                name: "ScheduleInJSON",
                table: "schedules",
                newName: "jsonfilewithschedule_locaton");
        }
    }
}
