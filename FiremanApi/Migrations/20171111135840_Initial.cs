using System;

using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FiremanApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "gps_points",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    lat = table.Column<double>(nullable: false),
                    lon = table.Column<double>(nullable: false),
                    ServiceZoneId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gps_points", x => x.id);
                    table.ForeignKey(
                        name: "FK_gps_points_service_zones_ServiceZoneId",
                        column: x => x.ServiceZoneId,
                        principalTable: "service_zones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    address = table.Column<string>(nullable: true),
                    GpsPointId = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    ServiceZoneId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.id);
                    table.ForeignKey(
                        name: "FK_departments_gps_points_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "gps_points",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_departments_service_zones_ServiceZoneId",
                        column: x => x.ServiceZoneId,
                        principalTable: "service_zones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "hydrants",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    active = table.Column<bool>(nullable: false),
                    GpsPointId = table.Column<int>(nullable: true),
                    responsible = table.Column<string>(nullable: true),
                    revision_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hydrants", x => x.id);
                    table.ForeignKey(
                        name: "FK_hydrants_gps_points_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "gps_points",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "operators",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    GeoZoneId = table.Column<int>(nullable: true),
                    key = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operators", x => x.id);
                    table.ForeignKey(
                        name: "FK_operators_gps_points_GeoZoneId",
                        column: x => x.GeoZoneId,
                        principalTable: "gps_points",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fires",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    address = table.Column<string>(nullable: true),
                    comments = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    finish_time = table.Column<DateTime>(nullable: true),
                    GpsPointId = table.Column<int>(nullable: true),
                    OperatorId = table.Column<int>(nullable: true),
                    rank = table.Column<int>(nullable: false),
                    start_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fires", x => x.id);
                    table.ForeignKey(
                        name: "FK_fires_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fires_gps_points_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "gps_points",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fires_operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "operators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "firecars",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DepartmentId = table.Column<int>(nullable: true),
                    FireId = table.Column<int>(nullable: true),
                    GpsPointId = table.Column<int>(nullable: true),
                    lastUpdate = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    num = table.Column<string>(nullable: true),
                    VideoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_firecars", x => x.id);
                    table.ForeignKey(
                        name: "FK_firecars_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_firecars_fires_FireId",
                        column: x => x.FireId,
                        principalTable: "fires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_firecars_gps_points_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "gps_points",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_firecars_video_translations_VideoId",
                        column: x => x.VideoId,
                        principalTable: "video_translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FireId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_fires_FireId",
                        column: x => x.FireId,
                        principalTable: "fires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GpsLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateTime = table.Column<DateTime>(nullable: false),
                    FireCarId = table.Column<int>(nullable: true),
                    FireId = table.Column<int>(nullable: true),
                    GpsPointId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpsLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GpsLog_firecars_FireCarId",
                        column: x => x.FireCarId,
                        principalTable: "firecars",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GpsLog_fires_FireId",
                        column: x => x.FireId,
                        principalTable: "fires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GpsLog_gps_points_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "gps_points",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_departments_GpsPointId",
                table: "departments",
                column: "GpsPointId");

            migrationBuilder.CreateIndex(
                name: "IX_departments_ServiceZoneId",
                table: "departments",
                column: "ServiceZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_fires_DepartmentId",
                table: "fires",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_fires_GpsPointId",
                table: "fires",
                column: "GpsPointId");

            migrationBuilder.CreateIndex(
                name: "IX_fires_OperatorId",
                table: "fires",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_firecars_DepartmentId",
                table: "firecars",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_firecars_FireId",
                table: "firecars",
                column: "FireId");

            migrationBuilder.CreateIndex(
                name: "IX_firecars_GpsPointId",
                table: "firecars",
                column: "GpsPointId");

            migrationBuilder.CreateIndex(
                name: "IX_firecars_VideoId",
                table: "firecars",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_GpsLog_FireCarId",
                table: "GpsLog",
                column: "FireCarId");

            migrationBuilder.CreateIndex(
                name: "IX_GpsLog_FireId",
                table: "GpsLog",
                column: "FireId");

            migrationBuilder.CreateIndex(
                name: "IX_GpsLog_GpsPointId",
                table: "GpsLog",
                column: "GpsPointId");

            migrationBuilder.CreateIndex(
                name: "IX_gps_points_ServiceZoneId",
                table: "gps_points",
                column: "ServiceZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_hydrants_GpsPointId",
                table: "hydrants",
                column: "GpsPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_FireId",
                table: "Images",
                column: "FireId");

            migrationBuilder.CreateIndex(
                name: "IX_operators_GeoZoneId",
                table: "operators",
                column: "GeoZoneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GpsLog");

            migrationBuilder.DropTable(
                name: "hydrants");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "firecars");

            migrationBuilder.DropTable(
                name: "fires");

            migrationBuilder.DropTable(
                name: "video_translations");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "operators");

            migrationBuilder.DropTable(
                name: "gps_points");

            migrationBuilder.DropTable(
                name: "service_zones");
        }
    }
}
