using Microsoft.EntityFrameworkCore.Migrations;

namespace UKAR.Migrations
{
    public partial class modifyLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasTrip",
                table: "UserLocations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnTrip",
                table: "UserLocations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasTrip",
                table: "UserLocations");

            migrationBuilder.DropColumn(
                name: "OnTrip",
                table: "UserLocations");
        }
    }
}
