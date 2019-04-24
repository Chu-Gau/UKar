using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UKAR.Migrations
{
    public partial class addCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    CarImage = table.Column<string>(nullable: true),
                    CarImageFileType = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    PlateNumber = table.Column<string>(nullable: true),
                    RegistrationImage = table.Column<string>(nullable: true),
                    RegistrationImageFileType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CarId",
                table: "AspNetUsers",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cars_CarId",
                table: "AspNetUsers",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cars_CarId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CarId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "AspNetUsers");
        }
    }
}
