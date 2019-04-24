using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UKAR.Migrations
{
    public partial class addLicenseAndDrivingTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DriverTestPassed",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DrivingLicenses",
                columns: table => new
                {
                    DriverId = table.Column<string>(nullable: false),
                    LicenseNumber = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    ImageFileType = table.Column<string>(nullable: true),
                    ImageBack = table.Column<string>(nullable: true),
                    ImageBackFileType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrivingLicenses", x => x.DriverId);
                    table.ForeignKey(
                        name: "FK_DrivingLicenses_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrivingTests",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PracticeScore = table.Column<int>(nullable: false),
                    ExamScore = table.Column<int>(nullable: false),
                    Passed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrivingTests", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_DrivingTests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrivingLicenses");

            migrationBuilder.DropTable(
                name: "DrivingTests");

            migrationBuilder.DropColumn(
                name: "DriverTestPassed",
                table: "AspNetUsers");
        }
    }
}
