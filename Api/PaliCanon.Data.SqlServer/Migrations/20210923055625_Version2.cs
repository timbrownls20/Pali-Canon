using Microsoft.EntityFrameworkCore.Migrations;

namespace PaliCanon.Data.SqlServer.Migrations
{
    public partial class Version2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nikaya",
                table: "Chapter");

            migrationBuilder.AddColumn<string>(
                name: "Nikaya",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nikaya",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "Nikaya",
                table: "Chapter",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
