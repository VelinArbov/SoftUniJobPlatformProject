using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftUniJobPlatform.Data.Migrations
{
    public partial class AddRelBetweenJobandCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
