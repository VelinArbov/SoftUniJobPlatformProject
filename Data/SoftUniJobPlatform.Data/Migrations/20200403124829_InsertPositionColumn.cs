using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftUniJobPlatform.Data.Migrations
{
    public partial class InsertPositionColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Jobs");
        }
    }
}
