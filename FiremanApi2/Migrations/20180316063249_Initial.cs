using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FiremanApi2.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GpsPoints",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    lat = table.Column<double>(nullable: false),
                    lon = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpsPoints", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "VideoStructs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoStructs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
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
                    table.PrimaryKey("PK_Departments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Departments_GpsPoints_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "GpsPoints",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hydrants",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    active = table.Column<bool>(nullable: false),
                    descr = table.Column<string>(nullable: true),
                    fault_problem = table.Column<string>(nullable: true),
                    GpsPointId = table.Column<int>(nullable: true),
                    place = table.Column<string>(nullable: true),
                    responsible = table.Column<string>(nullable: true),
                    revision_date = table.Column<DateTime>(nullable: false),
                    water_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hydrants", x => x.id);
                    table.ForeignKey(
                        name: "FK_Hydrants_GpsPoints_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "GpsPoints",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
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
                    table.PrimaryKey("PK_Operators", x => x.id);
                    table.ForeignKey(
                        name: "FK_Operators_GpsPoints_GeoZoneId",
                        column: x => x.GeoZoneId,
                        principalTable: "GpsPoints",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Category = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GpsPointId = table.Column<int>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    Rank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_GpsPoints_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "GpsPoints",
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
                        name: "FK_Departures_GpsPoints_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "GpsPoints",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departures_Operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fires",
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
                    table.PrimaryKey("PK_Fires", x => x.id);
                    table.ForeignKey(
                        name: "FK_Fires_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fires_GpsPoints_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "GpsPoints",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fires_Operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FireCars",
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
                    table.PrimaryKey("PK_FireCars", x => x.id);
                    table.ForeignKey(
                        name: "FK_FireCars_VideoStructs_BroadcastId",
                        column: x => x.BroadcastId,
                        principalTable: "VideoStructs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FireCars_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FireCars_Departures_DepartureId",
                        column: x => x.DepartureId,
                        principalTable: "Departures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FireCars_Fires_FireId",
                        column: x => x.FireId,
                        principalTable: "Fires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FireCars_GpsPoints_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "GpsPoints",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryRecord",
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
                    table.PrimaryKey("PK_HistoryRecord", x => x.id);
                    table.ForeignKey(
                        name: "FK_HistoryRecord_Departures_DepartureId",
                        column: x => x.DepartureId,
                        principalTable: "Departures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryRecord_Fires_FireId",
                        column: x => x.FireId,
                        principalTable: "Fires",
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
                        name: "FK_Images_Fires_FireId",
                        column: x => x.FireId,
                        principalTable: "Fires",
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
                        name: "FK_GpsLog_FireCars_FireCarId",
                        column: x => x.FireCarId,
                        principalTable: "FireCars",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GpsLog_Fires_FireId",
                        column: x => x.FireId,
                        principalTable: "Fires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GpsLog_GpsPoints_GpsPointId",
                        column: x => x.GpsPointId,
                        principalTable: "GpsPoints",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_DepartmentId",
                table: "Addresses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_GpsPointId",
                table: "Addresses",
                column: "GpsPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_GpsPointId",
                table: "Departments",
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
                name: "IX_FireCars_BroadcastId",
                table: "FireCars",
                column: "BroadcastId");

            migrationBuilder.CreateIndex(
                name: "IX_FireCars_DepartmentId",
                table: "FireCars",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FireCars_DepartureId",
                table: "FireCars",
                column: "DepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_FireCars_FireId",
                table: "FireCars",
                column: "FireId");

            migrationBuilder.CreateIndex(
                name: "IX_FireCars_GpsPointId",
                table: "FireCars",
                column: "GpsPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Fires_DepartmentId",
                table: "Fires",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Fires_GpsPointId",
                table: "Fires",
                column: "GpsPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Fires_OperatorId",
                table: "Fires",
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
                name: "IX_HistoryRecord_DepartureId",
                table: "HistoryRecord",
                column: "DepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryRecord_FireId",
                table: "HistoryRecord",
                column: "FireId");

            migrationBuilder.CreateIndex(
                name: "IX_Hydrants_GpsPointId",
                table: "Hydrants",
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
                name: "IX_Operators_GeoZoneId",
                table: "Operators",
                column: "GeoZoneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "GpsLog");

            migrationBuilder.DropTable(
                name: "HistoryRecord");

            migrationBuilder.DropTable(
                name: "Hydrants");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "FireCars");

            migrationBuilder.DropTable(
                name: "VideoStructs");

            migrationBuilder.DropTable(
                name: "Departures");

            migrationBuilder.DropTable(
                name: "Fires");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropTable(
                name: "GpsPoints");
        }
    }
}
