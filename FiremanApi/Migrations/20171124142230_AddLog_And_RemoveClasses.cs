using System;

using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FiremanApi.Migrations
{
    public partial class AddLog_And_RemoveClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_service_zones_ServiceZoneId",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_firecars_video_translations_VideoId",
                table: "firecars");

            migrationBuilder.DropForeignKey(
                name: "FK_gps_points_service_zones_ServiceZoneId",
                table: "gps_points");

            migrationBuilder.DropTable(
                name: "MapCaches");

            migrationBuilder.DropTable(
                name: "service_zones");

            migrationBuilder.DropTable(
                name: "video_translations");

            migrationBuilder.DropIndex(
                name: "IX_gps_points_ServiceZoneId",
                table: "gps_points");

            migrationBuilder.DropIndex(
                name: "IX_departments_ServiceZoneId",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "ServiceZoneId",
                table: "gps_points");

            migrationBuilder.DropColumn(
                name: "ServiceZoneId",
                table: "departments");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "firecars",
                newName: "BroadcastId");

            migrationBuilder.RenameIndex(
                name: "IX_firecars_VideoId",
                table: "firecars",
                newName: "IX_firecars_BroadcastId");

            migrationBuilder.AddColumn<string>(
                name: "descr",
                table: "hydrants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "spec",
                table: "firecars",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "video_broadcast",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_video_broadcast", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    dateTime = table.Column<DateTime>(nullable: false),
                    FireId = table.Column<int>(nullable: true),
                    record = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.id);
                    table.ForeignKey(
                        name: "FK_History_fires_FireId",
                        column: x => x.FireId,
                        principalTable: "fires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_History_FireId",
                table: "History",
                column: "FireId");

            migrationBuilder.AddForeignKey(
                name: "FK_firecars_video_broadcast_BroadcastId",
                table: "firecars",
                column: "BroadcastId",
                principalTable: "video_broadcast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_firecars_video_broadcast_BroadcastId",
                table: "firecars");

            migrationBuilder.DropTable(
                name: "video_broadcast");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropColumn(
                name: "descr",
                table: "hydrants");

            migrationBuilder.DropColumn(
                name: "spec",
                table: "firecars");

            migrationBuilder.RenameColumn(
                name: "BroadcastId",
                table: "firecars",
                newName: "VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_firecars_BroadcastId",
                table: "firecars",
                newName: "IX_firecars_VideoId");

            migrationBuilder.AddColumn<int>(
                name: "ServiceZoneId",
                table: "gps_points",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceZoneId",
                table: "departments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MapCaches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapCaches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "service_zones",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_zones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "video_translations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_video_translations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gps_points_ServiceZoneId",
                table: "gps_points",
                column: "ServiceZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_departments_ServiceZoneId",
                table: "departments",
                column: "ServiceZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_service_zones_ServiceZoneId",
                table: "departments",
                column: "ServiceZoneId",
                principalTable: "service_zones",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_firecars_video_translations_VideoId",
                table: "firecars",
                column: "VideoId",
                principalTable: "video_translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_gps_points_service_zones_ServiceZoneId",
                table: "gps_points",
                column: "ServiceZoneId",
                principalTable: "service_zones",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
