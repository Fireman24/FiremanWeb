using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FiremanApi.DataBase;

namespace FiremanApi.Migrations
{
    [DbContext(typeof(FireContext))]
    [Migration("20171120210148_AddressMigration")]
    partial class AddressMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("FiremanModel.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label");

                    b.Property<double>("Lat");

                    b.Property<double>("Lon");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("FiremanModel.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnName("address");

                    b.Property<int?>("GpsPointId");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<int?>("ServiceZoneId");

                    b.HasKey("Id");

                    b.HasIndex("GpsPointId");

                    b.HasIndex("ServiceZoneId");

                    b.ToTable("departments");
                });

            modelBuilder.Entity("FiremanModel.Fire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnName("address");

                    b.Property<string>("Comments")
                        .HasColumnName("comments");

                    b.Property<int?>("DepartmentId");

                    b.Property<DateTime?>("FinishDateTime")
                        .HasColumnName("finish_time");

                    b.Property<int?>("GpsPointId");

                    b.Property<int?>("OperatorId");

                    b.Property<int>("Rank")
                        .HasColumnName("rank");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnName("start_time");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("GpsPointId");

                    b.HasIndex("OperatorId");

                    b.ToTable("fires");
                });

            modelBuilder.Entity("FiremanModel.FireCar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("DepartmentId");

                    b.Property<int?>("FireId");

                    b.Property<int?>("GpsPointId");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnName("lastUpdate");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("Num")
                        .HasColumnName("num");

                    b.Property<int?>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FireId");

                    b.HasIndex("GpsPointId");

                    b.HasIndex("VideoId");

                    b.ToTable("firecars");
                });

            modelBuilder.Entity("FiremanModel.GpsLogRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<int?>("FireCarId");

                    b.Property<int?>("FireId");

                    b.Property<int?>("GpsPointId");

                    b.HasKey("Id");

                    b.HasIndex("FireCarId");

                    b.HasIndex("FireId");

                    b.HasIndex("GpsPointId");

                    b.ToTable("GpsLog");
                });

            modelBuilder.Entity("FiremanModel.GpsPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<double>("Lat")
                        .HasColumnName("lat");

                    b.Property<double>("Lon")
                        .HasColumnName("lon");

                    b.Property<int?>("ServiceZoneId");

                    b.HasKey("Id");

                    b.HasIndex("ServiceZoneId");

                    b.ToTable("gps_points");
                });

            modelBuilder.Entity("FiremanModel.Hydrant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnName("active");

                    b.Property<int?>("GpsPointId");

                    b.Property<string>("Responsible")
                        .HasColumnName("responsible");

                    b.Property<DateTime>("RevisionDate")
                        .HasColumnName("revision_date");

                    b.HasKey("Id");

                    b.HasIndex("GpsPointId");

                    b.ToTable("hydrants");
                });

            modelBuilder.Entity("FiremanModel.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FireId");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("FireId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("FiremanModel.MapCache", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("MapCaches");
                });

            modelBuilder.Entity("FiremanModel.Operator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("GeoZoneId");

                    b.Property<string>("Key")
                        .HasColumnName("key");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("GeoZoneId");

                    b.ToTable("operators");
                });

            modelBuilder.Entity("FiremanModel.ServiceZone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("service_zones");
                });

            modelBuilder.Entity("FiremanModel.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("video_translations");
                });

            modelBuilder.Entity("FiremanModel.Department", b =>
                {
                    b.HasOne("FiremanModel.GpsPoint", "GpsPoint")
                        .WithMany()
                        .HasForeignKey("GpsPointId");

                    b.HasOne("FiremanModel.ServiceZone", "ServiceZone")
                        .WithMany()
                        .HasForeignKey("ServiceZoneId");
                });

            modelBuilder.Entity("FiremanModel.Fire", b =>
                {
                    b.HasOne("FiremanModel.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("FiremanModel.GpsPoint", "GpsPoint")
                        .WithMany()
                        .HasForeignKey("GpsPointId");

                    b.HasOne("FiremanModel.Operator", "Operator")
                        .WithMany("Fires")
                        .HasForeignKey("OperatorId");
                });

            modelBuilder.Entity("FiremanModel.FireCar", b =>
                {
                    b.HasOne("FiremanModel.Department", "Department")
                        .WithMany("FireCars")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("FiremanModel.Fire", "Fire")
                        .WithMany("FireCars")
                        .HasForeignKey("FireId");

                    b.HasOne("FiremanModel.GpsPoint", "GpsPoint")
                        .WithMany()
                        .HasForeignKey("GpsPointId");

                    b.HasOne("FiremanModel.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId");
                });

            modelBuilder.Entity("FiremanModel.GpsLogRecord", b =>
                {
                    b.HasOne("FiremanModel.FireCar", "FireCar")
                        .WithMany()
                        .HasForeignKey("FireCarId");

                    b.HasOne("FiremanModel.Fire", "Fire")
                        .WithMany()
                        .HasForeignKey("FireId");

                    b.HasOne("FiremanModel.GpsPoint", "GpsPoint")
                        .WithMany()
                        .HasForeignKey("GpsPointId");
                });

            modelBuilder.Entity("FiremanModel.GpsPoint", b =>
                {
                    b.HasOne("FiremanModel.ServiceZone")
                        .WithMany("Points")
                        .HasForeignKey("ServiceZoneId");
                });

            modelBuilder.Entity("FiremanModel.Hydrant", b =>
                {
                    b.HasOne("FiremanModel.GpsPoint", "GpsPoint")
                        .WithMany()
                        .HasForeignKey("GpsPointId");
                });

            modelBuilder.Entity("FiremanModel.Image", b =>
                {
                    b.HasOne("FiremanModel.Fire")
                        .WithMany("Images")
                        .HasForeignKey("FireId");
                });

            modelBuilder.Entity("FiremanModel.Operator", b =>
                {
                    b.HasOne("FiremanModel.GpsPoint", "GeoZone")
                        .WithMany()
                        .HasForeignKey("GeoZoneId");
                });
        }
    }
}
