using Microsoft.EntityFrameworkCore.Migrations;

namespace FiremanApi.Migrations
{
    public partial class AddresRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Addresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_DepartmentId",
                table: "Addresses",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_departments_DepartmentId",
                table: "Addresses",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_departments_DepartmentId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_DepartmentId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Addresses");
        }
    }
}
