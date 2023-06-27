﻿// <auto-generated />
using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230626080825_Migrations")]
    partial class Migrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.City", b =>
                {
                    b.Property<int>("CityCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityCode"));

                    b.Property<string>("Cityname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateCode")
                        .HasColumnType("int");

                    b.HasKey("CityCode");

                    b.HasIndex("StateCode");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Domain.Models.Country", b =>
                {
                    b.Property<int>("CountryCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryCode"));

                    b.Property<string>("Countryname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryCode");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Domain.Models.Employee", b =>
                {
                    b.Property<int>("EmpID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmpID"));

                    b.Property<string>("Address")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CityCode")
                        .HasColumnType("int");

                    b.Property<int>("CountryCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("StateCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("EmpID");

                    b.HasIndex("CityCode");

                    b.HasIndex("CountryCode");

                    b.HasIndex("StateCode");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Domain.Models.State", b =>
                {
                    b.Property<int>("StateCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StateCode"));

                    b.Property<int>("CountryCode")
                        .HasColumnType("int");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StateCode");

                    b.HasIndex("CountryCode");

                    b.ToTable("State");
                });

            modelBuilder.Entity("Domain.Models.City", b =>
                {
                    b.HasOne("Domain.Models.State", "State")
                        .WithMany()
                        .HasForeignKey("StateCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("Domain.Models.Employee", b =>
                {
                    b.HasOne("Domain.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.State", "State")
                        .WithMany()
                        .HasForeignKey("StateCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("State");
                });

            modelBuilder.Entity("Domain.Models.State", b =>
                {
                    b.HasOne("Domain.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });
#pragma warning restore 612, 618
        }
    }
}
