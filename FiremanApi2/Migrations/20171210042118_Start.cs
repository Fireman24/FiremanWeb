using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FiremanApi2.Migrations
{
    public partial class Start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gps_points",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    lat = table.Column<double>(nullable: false),
                    lon = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gps_points", x => x.id);
                });

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
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Active = table.Column<bool>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    GpsPointId = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "hydrants",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    active = table.Column<bool>(nullable: false),
                    descr = table.Column<string>(nullable: true),
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
                    active = table.Column<bool>(nullable: false),
                    GeoZoneId = table.Column<int>(nullable: true),
                    key = table.Column<string>(nullable: true),
                    login = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    role = table.Column<string>(nullable: true)
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
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DepartmentId = table.Column<int>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    Lat = table.Column<double>(nullable: false),
                    Lon = table.Column<double>(nullable: false),
                    Rank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "fires",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    active = table.Column<bool>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    comments = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    finish_time = table.Column<DateTime>(nullable: true),
                    GpsPointId = table.Column<int>(nullable: true),
                    manager = table.Column<string>(nullable: true),
                    OperatorId = table.Column<int>(nullable: true),
                    rank = table.Column<int>(nullable: false),
                    receiver = table.Column<string>(nullable: true),
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
                    active = table.Column<bool>(nullable: false),
                    BroadcastId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    DepartureId = table.Column<int>(nullable: true),
                    FireId = table.Column<int>(nullable: true),
                    GpsPointId = table.Column<int>(nullable: true),
                    lastUpdate = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    num = table.Column<string>(nullable: true),
                    spec = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_firecars", x => x.id);
                    table.ForeignKey(
                        name: "FK_firecars_video_broadcast_BroadcastId",
                        column: x => x.BroadcastId,
                        principalTable: "video_broadcast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_firecars_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_firecars_Departures_DepartureId",
                        column: x => x.DepartureId,
                        principalTable: "Departures",
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
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    dateTime = table.Column<DateTime>(nullable: false),
                    DepartureId = table.Column<int>(nullable: true),
                    FireId = table.Column<int>(nullable: true),
                    record = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.id);
                    table.ForeignKey(
                        name: "FK_History_Departures_DepartureId",
                        column: x => x.DepartureId,
                        principalTable: "Departures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_History_fires_FireId",
                        column: x => x.FireId,
                        principalTable: "fires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DepartureId = table.Column<int>(nullable: true),
                    FireId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Departures_DepartureId",
                        column: x => x.DepartureId,
                        principalTable: "Departures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_Addresses_DepartmentId",
                table: "Addresses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_departments_GpsPointId",
                table: "departments",
                column: "GpsPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Departures_GpsPointId",
                table: "Departures",
                column: "GpsPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Departures_OperatorId",
                table: "Departures",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_firecars_BroadcastId",
                table: "firecars",
                column: "BroadcastId");

            migrationBuilder.CreateIndex(
                name: "IX_firecars_DepartmentId",
                table: "firecars",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_firecars_DepartureId",
                table: "firecars",
                column: "DepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_firecars_FireId",
                table: "firecars",
                column: "FireId");

            migrationBuilder.CreateIndex(
                name: "IX_firecars_GpsPointId",
                table: "firecars",
                column: "GpsPointId");

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
                name: "IX_History_DepartureId",
                table: "History",
                column: "DepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_History_FireId",
                table: "History",
                column: "FireId");

            migrationBuilder.CreateIndex(
                name: "IX_hydrants_GpsPointId",
                table: "hydrants",
                column: "GpsPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_DepartureId",
                table: "Images",
                column: "DepartureId");

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
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "GpsLog");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "hydrants");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "firecars");

            migrationBuilder.DropTable(
                name: "video_broadcast");

            migrationBuilder.DropTable(
                name: "Departures");

            migrationBuilder.DropTable(
                name: "fires");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "operators");

            migrationBuilder.DropTable(
                name: "gps_points");
        }
    }
}
