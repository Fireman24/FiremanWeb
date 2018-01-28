using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FiremanApi.DataBase;

namespace FiremanApi.Migrations
{
    [DbContext(typeof(FireContext))]
    [Migration("20171124153843_OperatorStatus")]
    partial class OperatorStatus
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

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("Label");

                    b.Property<double>("Lat");

                    b.Property<double>("Lon");

                    b.Property<int>("Rank");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("FiremanModel.Broadcast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("video_broadcast");
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

                    b.HasKey("Id");

                    b.HasIndex("GpsPointId");

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

                    b.Property<bool>("Active")
                        .HasColumnName("active");

                    b.Property<int?>("BroadcastId");

                    b.Property<int?>("DepartmentId");

                    b.Property<int?>("FireId");

                    b.Property<int?>("GpsPointId");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnName("lastUpdate");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("Num")
                        .HasColumnName("num");

                    b.Property<string>("Specialization")
                        .HasColumnName("spec");

                    b.HasKey("Id");

                    b.HasIndex("BroadcastId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FireId");

                    b.HasIndex("GpsPointId");

                    b.ToTable("firecars");
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

                    b.HasKey("Id");

                    b.ToTable("gps_points");
                });

            modelBuilder.Entity("FiremanModel.GpsRecord", b =>
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

            modelBuilder.Entity("FiremanModel.HistoryRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("DateTime")
                        .HasColumnName("dateTime");

                    b.Property<int?>("FireId");

                    b.Property<string>("Record")
                        .HasColumnName("record");

                    b.HasKey("Id");

                    b.HasIndex("FireId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("FiremanModel.Hydrant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnName("active");

                    b.Property<string>("Description")
                        .HasColumnName("descr");

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

            modelBuilder.Entity("FiremanModel.Operator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnName("active");

                    b.Property<int?>("GeoZoneId");

                    b.Property<string>("Key")
                        .HasColumnName("key");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("GeoZoneId");

                    b.ToTable("operators");
                });

            modelBuilder.Entity("FiremanModel.Address", b =>
                {
                    b.HasOne("FiremanModel.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("FiremanModel.Department", b =>
                {
                    b.HasOne("FiremanModel.GpsPoint", "GpsPoint")
                        .WithMany()
                        .HasForeignKey("GpsPointId");
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
                    b.HasOne("FiremanModel.Broadcast", "Broadcast")
                        .WithMany()
                        .HasForeignKey("BroadcastId");

                    b.HasOne("FiremanModel.Department", "Department")
                        .WithMany("FireCars")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("FiremanModel.Fire", "Fire")
                        .WithMany("FireCars")
                        .HasForeignKey("FireId");

                    b.HasOne("FiremanModel.GpsPoint", "GpsPoint")
                        .WithMany()
                        .HasForeignKey("GpsPointId");
                });

            modelBuilder.Entity("FiremanModel.GpsRecord", b =>
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

            modelBuilder.Entity("FiremanModel.HistoryRecord", b =>
                {
                    b.HasOne("FiremanModel.Fire")
                        .WithMany("HystoryRecords")
                        .HasForeignKey("FireId");
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
