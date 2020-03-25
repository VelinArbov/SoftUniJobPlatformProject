using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftUniJobPlatform.Data.Migrations
{
    public partial class AddJobProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Engagement",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Engagement",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Jobs");
        }
    }
}
