using Microsoft.EntityFrameworkCore.Migrations;

namespace PaliCanon.Data.SqlServer.Migrations
{
    public partial class Version3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Citation",
                table: "Chapter",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Citation",
                table: "Chapter");
        }
    }
}
