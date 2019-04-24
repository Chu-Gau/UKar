using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UKAR.Migrations
{
    public partial class nullableOnTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeOffset",
                table: "Trips",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<double>(
                name: "Distance",
                table: "Trips",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeOffset",
                table: "ActiveTrips",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "ActiveTrips",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishTime",
                table: "ActiveTrips",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<double>(
                name: "Distance",
                table: "ActiveTrips",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeOffset",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Distance",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeOffset",
                table: "ActiveTrips",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "ActiveTrips",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishTime",
                table: "ActiveTrips",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Distance",
                table: "ActiveTrips",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
