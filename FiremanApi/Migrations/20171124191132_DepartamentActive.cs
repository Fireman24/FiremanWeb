using Microsoft.EntityFrameworkCore.Migrations;

namespace FiremanApi.Migrations
{
    public partial class DepartamentActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "departments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "departments");
        }
    }
}
