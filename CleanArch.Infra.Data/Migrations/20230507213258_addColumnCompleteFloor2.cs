using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Migrations
{
    public partial class addColumnCompleteFloor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteFloor",
                table: "Elevators");

            migrationBuilder.AddColumn<bool>(
                name: "CompleteFloor",
                table: "ElevatorCallSteps",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteFloor",
                table: "ElevatorCallSteps");

            migrationBuilder.AddColumn<bool>(
                name: "CompleteFloor",
                table: "Elevators",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
