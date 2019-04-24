using Microsoft.EntityFrameworkCore.Migrations;

namespace UKAR.Migrations
{
    public partial class changeActiveTripIDCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveTrips_AspNetUsers_DriverId",
                table: "ActiveTrips");

            migrationBuilder.DropForeignKey(
                name: "FK_ActiveTrips_AspNetUsers_EmployerId",
                table: "ActiveTrips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActiveTrips",
                table: "ActiveTrips");

            migrationBuilder.DropIndex(
                name: "IX_ActiveTrips_DriverId",
                table: "ActiveTrips");

            migrationBuilder.DropIndex(
                name: "IX_ActiveTrips_EmployerId",
                table: "ActiveTrips");

            migrationBuilder.AlterColumn<string>(
                name: "EmployerId",
                table: "ActiveTrips",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "ActiveTrips",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActiveTrips",
                table: "ActiveTrips",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTrips_DriverId",
                table: "ActiveTrips",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveTrips_AspNetUsers_DriverId",
                table: "ActiveTrips",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveTrips_AspNetUsers_EmployerId",
                table: "ActiveTrips",
                column: "EmployerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveTrips_AspNetUsers_DriverId",
                table: "ActiveTrips");

            migrationBuilder.DropForeignKey(
                name: "FK_ActiveTrips_AspNetUsers_EmployerId",
                table: "ActiveTrips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActiveTrips",
                table: "ActiveTrips");

            migrationBuilder.DropIndex(
                name: "IX_ActiveTrips_DriverId",
                table: "ActiveTrips");

            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "ActiveTrips",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployerId",
                table: "ActiveTrips",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActiveTrips",
                table: "ActiveTrips",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTrips_DriverId",
                table: "ActiveTrips",
                column: "DriverId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActiveTrips_EmployerId",
                table: "ActiveTrips",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveTrips_AspNetUsers_DriverId",
                table: "ActiveTrips",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveTrips_AspNetUsers_EmployerId",
                table: "ActiveTrips",
                column: "EmployerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
