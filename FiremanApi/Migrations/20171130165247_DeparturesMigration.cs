using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FiremanApi.Migrations
{
    public partial class DeparturesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartureId",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartureId",
                table: "History",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartureId",
                table: "firecars",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departures",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    active = table.Column<bool>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    comments = table.Column<string>(nullable: true),
                    date_time = table.Column<DateTime>(nullable: false),
                    GpsPointId = table.Column<int>(nullable: true),
                    intent = table.Column<string>(nullable: true),
                    manager = table.Column<string>(nullable: true),
                    OperatorId = table.Column<int>(nullable: true),
                    receiver = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departures", x => x.id);
                    table.ForeignKey(
                        name: "FK_Departures_gps_points_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "gps_points",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departures_operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "operators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_DepartureId",
                table: "Images",
                column: "DepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_History_DepartureId",
                table: "History",
                column: "DepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_firecars_DepartureId",
                table: "firecars",
                column: "DepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_Departures_GpsPointId",
                table: "Departures",
                column: "GpsPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Departures_OperatorId",
                table: "Departures",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_firecars_Departures_DepartureId",
                table: "firecars",
                column: "DepartureId",
                principalTable: "Departures",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Departures_DepartureId",
                table: "History",
                column: "DepartureId",
                principalTable: "Departures",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Departures_DepartureId",
                table: "Images",
                column: "DepartureId",
                principalTable: "Departures",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_firecars_Departures_DepartureId",
                table: "firecars");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Departures_DepartureId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Departures_DepartureId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Departures");

            migrationBuilder.DropIndex(
                name: "IX_Images_DepartureId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_History_DepartureId",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_firecars_DepartureId",
                table: "firecars");

            migrationBuilder.DropColumn(
                name: "DepartureId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "DepartureId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "DepartureId",
                table: "firecars");
        }
    }
}
