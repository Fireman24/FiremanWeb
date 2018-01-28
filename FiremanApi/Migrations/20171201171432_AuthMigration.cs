using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FiremanApi.Migrations
{
    public partial class AuthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "login",
                table: "operators",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "operators",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "login",
                table: "operators");

            migrationBuilder.DropColumn(
                name: "role",
                table: "operators");
        }
    }
}
