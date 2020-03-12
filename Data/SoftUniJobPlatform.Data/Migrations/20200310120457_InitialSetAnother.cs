using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftUniJobPlatform.Data.Migrations
{
    public partial class InitialSetAnother : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Categories_CategoryId1",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CategoryId1",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "TotalЕvaluation",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Jobs");

            migrationBuilder.AddColumn<double>(
                name: "Raiting",
                table: "Students",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegistrationNumber",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tittle",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CategoryId",
                table: "Jobs",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Categories_CategoryId",
                table: "Jobs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Categories_CategoryId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CategoryId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Raiting",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegistrationNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Tittle",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<double>(
                name: "TotalЕvaluation",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CategoryId1",
                table: "Jobs",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Categories_CategoryId1",
                table: "Jobs",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
