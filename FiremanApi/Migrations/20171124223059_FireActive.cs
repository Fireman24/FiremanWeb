using Microsoft.EntityFrameworkCore.Migrations;

namespace FiremanApi.Migrations
{
    public partial class FireActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "fires",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "manager",
                table: "fires",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "receiver",
                table: "fires",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "fires");

            migrationBuilder.DropColumn(
                name: "manager",
                table: "fires");

            migrationBuilder.DropColumn(
                name: "receiver",
                table: "fires");
        }
    }
}
