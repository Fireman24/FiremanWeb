using Microsoft.EntityFrameworkCore.Migrations;

namespace FiremanApi.Migrations
{
    public partial class OperatorStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "operators",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "firecars",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "operators");

            migrationBuilder.DropColumn(
                name: "active",
                table: "firecars");
        }
    }
}
