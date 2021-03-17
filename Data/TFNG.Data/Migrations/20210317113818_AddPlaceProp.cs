using Microsoft.EntityFrameworkCore.Migrations;

namespace TFNG.Data.Migrations
{
    public partial class AddPlaceProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Awards");

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Awards",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place",
                table: "Awards");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Awards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
