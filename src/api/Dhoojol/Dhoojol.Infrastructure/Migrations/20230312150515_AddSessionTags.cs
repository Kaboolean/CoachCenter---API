using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhoojol.Infrastructure.Migrations
{
    public partial class AddSessionTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Sessions");
        }
    }
}
