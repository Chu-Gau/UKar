using Microsoft.EntityFrameworkCore.Migrations;

namespace UKAR.Migrations
{
    public partial class changeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarFileType",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarFileType",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
