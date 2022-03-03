using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookTracker.Server.Data.Migrations
{
    public partial class AddedListSubTypeDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AcquiredUtc",
                table: "ReadingLists",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ReadingLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Format",
                table: "ReadingLists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HowAcquired",
                table: "ReadingLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "ReadingLists",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Review",
                table: "ReadingLists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartedUtc",
                table: "ReadingLists",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcquiredUtc",
                table: "ReadingLists");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ReadingLists");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "ReadingLists");

            migrationBuilder.DropColumn(
                name: "HowAcquired",
                table: "ReadingLists");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "ReadingLists");

            migrationBuilder.DropColumn(
                name: "Review",
                table: "ReadingLists");

            migrationBuilder.DropColumn(
                name: "StartedUtc",
                table: "ReadingLists");
        }
    }
}
